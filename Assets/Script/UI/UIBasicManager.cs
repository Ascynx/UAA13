using UnityEngine;
using UnityEngine.U2D;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIBasicManager : MonoBehaviour
{
    public UIControlGroupManager[] ctrlGroups;

    [SerializeField]
    private InputSystemIntegration inputIntegration;
    // Start is called before the first frame update

    [SerializeField] SpriteAtlas _controlAtlas;
    void Start()
    {
        for (int i = 0; i < ctrlGroups.Length; i++)
        {
            ctrlGroups[i].enabled = false;
        }
    }

    public void OnInputSystemIntegrationLoaded(InputSystemIntegration inputSystemIntegration)
    {
        inputIntegration = inputSystemIntegration;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        #endif
    }

    [InspectorButton("Test")]
    public bool RunTest = false;
    public void Test()
    {
        Debug.Log(inputIntegration.GetKeybindForAction("Sprint"));
        Debug.Log(inputIntegration.GetKeybindForAction("Direction"));
    }

    // Update is called once per frame
    void Update()
    {
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
        manager.enabled = true;
    }

    public Sprite GetSprite(string name)
    {
        return _controlAtlas.GetSprite(name);
    }
}
