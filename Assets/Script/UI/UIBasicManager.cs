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
            string key = jeu.inputIntegration.KeybindToSpriteKey(sprintBindings[i]);
            Sprite sprite = GetSprite(key);
            Debug.Log($"{(sprite == null ? "Did not find" : "Found")} Sprite for key '{key}'");
        }
        for (int j = 0; j < movementBindings.Count; j++)
        {
            string key = jeu.inputIntegration.KeybindToSpriteKey(movementBindings[j]);
            Sprite sprite = GetSprite(key);
            Debug.Log($"{(sprite == null ? "Did not find" : "Found")} Sprite for key '{key}'");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnLeaveGui()
    {
        Jeu jeu = Jeu.Instance;

        List<InputBinding> sprintBindings = jeu.inputIntegration.GetKeybindsForAction("Sprint");
        InputBinding binding = sprintBindings.First();
        string textSprint = $"Sprint";
        string spriteKeySprint = jeu.inputIntegration.KeybindToSpriteKey(binding);
        SetAideControle(0, spriteKeySprint, textSprint);

        List<InputBinding> openInventoryBinding = jeu.inputIntegration.GetKeybindsForAction("OpenInventory");
        InputBinding inventoryBinding = openInventoryBinding.First();
        string textInventory = $"Inventaire";
        string spriteKeyInventory = jeu.inputIntegration.KeybindToSpriteKey(inventoryBinding);
        SetAideControle(1, spriteKeyInventory, textInventory);
    }

    public void SetAideControle(int index, string nomSprite, string texte)
    {
        if (index >= ctrlGroups.Length)
        {
            throw new System.Exception("Peut pas modifier index " + index + " car supérieur à taille max.");
        }

        Sprite sprite = GetSprite(nomSprite);

        if (sprite == null)
        {
            throw new System.Exception("Peut pas modifier aide controle index: " + index + " car le sprite demandé n'existe pas");
        }

        UIControlGroupManager manager = ctrlGroups[index];

        manager.UpdateText(texte);
        manager.UpdateSprite(sprite);
        manager.enabled = (texte != null && sprite != null);
    }

    public Sprite GetSprite(string name)
    {
        return _controlAtlas.GetSprite(name);
    }
}
