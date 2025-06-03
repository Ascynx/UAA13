using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIBasicManager : MonoBehaviour
{
    public UIControlGroupManager[] ctrlGroups;
    // Start is called before the first frame update

    [SerializeField] SpriteAtlas _controlAtlas;

    void Start()
    {
        for (int i = 0; i < ctrlGroups.Length; i++)
        {
            ctrlGroups[i].enabled = false;
        }

        //TODO déplacer ça vers l'évenement de chargement du monde aka quand le controlleur peut être utilisé.
        ChargeAideControle();
    }

    [InspectorButton("Test")]
    public bool RunTest = false;
    public void Test()
    {
        Jeu jeu = Jeu.Instance;

        List<InputBinding> sprintBindings = jeu.inputIntegration.GetKeybindsForAction("Sprint");
        List<InputBinding> movementBindings = jeu.inputIntegration.GetKeybindsForAction("Direction");

        for (int i = 0; i < sprintBindings.Count; i++)
        {
            Optional<string> key = jeu.inputIntegration.KeybindToSpriteKey(sprintBindings[i]);
            Optional<Sprite> sprite = GetSprite(key.Get());
            Debug.Log($"{(sprite.IsEmpty() ? "Did not find" : "Found")} Sprite for key '{key}'");
        }
        for (int j = 0; j < movementBindings.Count; j++)
        {
            Optional<string> key = jeu.inputIntegration.KeybindToSpriteKey(movementBindings[j]);
            Optional<Sprite> sprite = GetSprite(key.Get());
            Debug.Log($"{(sprite.IsEmpty() ? "Did not find" : "Found")} Sprite for key '{key}'");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChargeAideControle()
    {
        Jeu jeu = Jeu.Instance;

        List<InputBinding> sprintBindings = jeu.inputIntegration.GetKeybindsForAction("Sprint");
        InputBinding binding = sprintBindings.First();
        string textSprint = $"Sprint";
        Optional<string> spriteKeySprint = jeu.inputIntegration.KeybindToSpriteKey(binding);
        SetAideControle(0, spriteKeySprint.OrElse("Keyboard_Cursor"), textSprint);

        List<InputBinding> openInventoryBinding = jeu.inputIntegration.GetKeybindsForAction("OpenInventory");
        InputBinding inventoryBinding = openInventoryBinding.First();
        string textInventory = $"Inventaire";
        Optional<string> spriteKeyInventory = jeu.inputIntegration.KeybindToSpriteKey(inventoryBinding);
        SetAideControle(1, spriteKeyInventory.OrElse("Keyboard_Cursor"), textInventory);

        List<InputBinding> openLivreBindings = jeu.inputIntegration.GetKeybindsForAction("OpenLivre");
        InputBinding openLivreBinding = openLivreBindings.First();
        string textLivre = $"Livre";
        Optional<string> spriteKeyLivre = jeu.inputIntegration.KeybindToSpriteKey(openLivreBinding);
        if (spriteKeyLivre.IsPresent() && GetSprite(spriteKeyLivre.Get()).IsEmpty())
        {
            SetAideControle(2, "Keyboard_Cursor", textLivre);
        } else
        {
            SetAideControle(2, spriteKeyLivre.OrElse("Keyboard_Cursor"), textLivre);
        }
        
    }

    public void SetAideControle(int index, string nomSprite, string texte)
    {
        if (index >= ctrlGroups.Length)
        {
            throw new System.Exception("Peut pas modifier index " + index + " car supérieur à taille max.");
        }

        Optional<Sprite> sprite = GetSprite(nomSprite);

        if (sprite.IsEmpty())
        {
            throw new System.Exception("Peut pas modifier aide controle index: " + index + " car le sprite demandé n'existe pas");
        }

        UIControlGroupManager manager = ctrlGroups[index];

        manager.UpdateText(texte);
        manager.UpdateSprite(sprite.Get());
        manager.enabled = (texte != null && sprite != null);
    }

    public Optional<Sprite> GetSprite(string name)
    {
        return Optional<Sprite>.OfNullable(_controlAtlas.GetSprite(name));
    }
}
