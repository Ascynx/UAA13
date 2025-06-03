using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;
using System.Linq;
using System;
using System.Security.Cryptography;

public class PlayerProperties : MonoBehaviour, Typings.ITyped, Typings.IAfflictable
{
    [SerializeField]
    public Inventory inventaire;
    [SerializeField]
    public int hp;
    [SerializeField]
    public SpriteAtlas playerSpriteAtlas;
    [SerializeField]
    public Moving movementManager;

    private bool alive = true; //devrait être false quand le "player" est mort/censé être inactif.

    public bool Alive
    {
        get => alive; set {
            alive = value;

            //change l'état de la caméra du joueur pour refléter l'état du joueur.
            if (Jeu.Instance.mainCamera.isActiveAndEnabled != alive)
            {
                Jeu.Instance.mainCamera.gameObject.SetActive(alive);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        SetAlive(alive, true);
    }

    public void SetAlive(bool alive, bool affectCamera)
    {
        if (affectCamera)
        {
            Alive = alive;
        }
        else
        {
            this.alive = alive;
        }
    }

    public Typings.Type Type1 => Typings.Type.None;

    public Typings.Type Type2 => Typings.Type.None;

    private Dictionary<string, Typings.Effect> effects = new();
    private Dictionary<string, int> effectTurns = new();

    public Dictionary<string, Typings.Effect> AppliedEffects => effects;

    public Dictionary<string, int> EffectTurns { get => effectTurns; set => effectTurns = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnOpenLivre(InputValue value)
    {
        float press = value.Get<float>();
        if (press > 0.5f)
        {
            if (Jeu.Instance.openedGUI is Livre)
            {
                Jeu.Instance.CloseGUI();
            } else Jeu.Instance.OpenGUI(Jeu.Instance.livre);
        }
    }

    public void OnExitGui(InputValue value)
    {
        float press = value.Get<float>();
        if (press > 0.5)
        {
            if (Jeu.Instance.openedGUI == null || !Jeu.Instance.openedGUI.CanBeEscaped())
            {
                return;
            }
            Jeu.Instance.CloseGUI();
        }
    }

    public bool resetSinceLastMovement = true;
    /// <summary>
    /// Mouvement en GUI
    /// </summary>
    /// <param name="value">La valeur donnée par l'input system, un vecteur 2D en ce cas ci.</param>
    public void OnGuiMove(InputValue value)
    {
        //TODO fix composite press release.
        Vector2 movement = value.Get<Vector2>();
        Vector2 dir = VectorUtility.SnapToDirection(movement);

        if (!resetSinceLastMovement)
        {
            //devrait éviter les mouvements trop rapides avec les sticks de gamepad.
            if (dir == Vector2.zero)
            {
                resetSinceLastMovement = true;
            }
            return;
        }

        //envoye vers le gui ouvert actuellement
        resetSinceLastMovement = false;
        Jeu.Instance.OnGuiMove(dir);
    }

    /// <summary>
    /// Appuie sur le bouton de sélection dans le GUI.
    /// </summary>
    /// <param name="value">La valeur donnée par l'input system, un float dans ce cas ci.</param>
    public void OnGuiSelect(InputValue value)
    {
        float press = value.Get<float>();
        if (press > 0.5f)
        {
            //envoye vers le gui ouvert actuellement
            Jeu.Instance.OnGuiSelect();
        }
    }

    public void OnOpenPauseScreen(InputValue value)
    {
        float press = value.Get<float>();
        if (press > 0.5f)
        {
            //ouvre le menu de pause si le joueur n'est pas déjà dans un menu.
            Jeu.Instance.OpenGUI(Jeu.Instance.pauseMenuGUI);
        }
    }

    public int GetDefense()
    {
        if (inventaire == null) return 0;
        return inventaire.equippedShield.def;
    }

    public void AddEffect(string source, Typings.Effect effect)
    {
        if (AppliedEffects.ContainsKey(source))
        {
            AppliedEffects[source] = effect;
        }
        else
        {
            AppliedEffects.Add(source, effect);
        }

        if (EffectTurns.ContainsKey(source))
        {
            EffectTurns.Remove(source);
        }
        EffectTurns.Add(source, 10);
    }

    public void TickDown()
    {
        IEnumerable<string> keys = new List<string>(EffectTurns.Keys);
        foreach (string source in keys)
        {
            if (!EffectTurns.Keys.Contains(source))
            {
                continue;
            }

            EffectTurns[source]--;
            if (EffectTurns[source] <= 0)
            {
                AppliedEffects.Remove(source);
                EffectTurns.Remove(source);
            }
        }
    }

    public Dictionary<Typings.Effect, int> GetAccumulatedEffects()
    {
        Dictionary<Typings.Effect, int> accumulatedEffects = new Dictionary<Typings.Effect, int>();
        foreach (KeyValuePair<string, Typings.Effect> effect in AppliedEffects)
        {
            if (Typings.GetEffectTypeOf(effect.Value) == Typings.EffectType.Unknown)
            {
                Debug.LogWarning("Type d'effet inconnu pour " + effect.Value);
                continue;
            }

            if (accumulatedEffects.ContainsKey(effect.Value))
            {
                if (accumulatedEffects[effect.Value] >= Typings.GetCappedEffectLevel(effect.Value))
                {
                    //already maxed out.
                    continue;
                }
                accumulatedEffects[effect.Value]++;
            }
            else
            {
                accumulatedEffects.Add(effect.Value, 1);
            }
        }
        return accumulatedEffects;
    }

    ///nullable enable
    public Typings.Effect? GetFirstEnvironmentalEffect()
    {
        foreach (KeyValuePair<string, Typings.Effect> effect in AppliedEffects)
        {
            if (Typings.GetEffectTypeOf(effect.Value) == Typings.EffectType.Environmental)
            {
                return effect.Value;
            }
        }
        return null;
    }

    public bool IsEffectSourceApplied(string source)
    {
        return AppliedEffects.ContainsKey(source);
    }

    public void ClearEffects()
    {
        AppliedEffects.Clear();
        EffectTurns.Clear();
    }
}
