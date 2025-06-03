using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.U2D;

public class Jeu : MonoBehaviour
{
    //Attention Race conditions.
    public EntityManager entityManager;

    public SpriteAtlas statusEffectSpriteAtlas;
    public SpriteAtlas itemAtlas;

    public InputSystemIntegration inputIntegration;

    public Canvas UICanvas;
    public UIBasicManager ControlUIManager;

    public Canvas GUICanvas;
    public MiniMapController miniMapController;

    public GameObject player;

    public PreferenceIntegration preferenceIntegration;
    public PlayerProperties playerProperties;

    public Camera mainCamera;

    public FicherSauvegarde fichierSauvegarde;
    public SaveIconControl saveIconControl;

    private Sauvegarde resetData;

    public Sauvegarde ResetData
    {
        get
        {
            if (resetData == null)
            {
                resetData = this.gameObject.AddComponent<Sauvegarde>();
                resetData.SetParent(fichierSauvegarde);
                resetData.Slot = "RESET";
                resetData.PrepareSave();
            }
            return resetData;
        }
    }

    public Shader gradientSpriteShader;
    public Shader basicShiftColoredShader;

    private static Jeu instance;

    public Livre livre;
    public ItemIndex ItemIndex;

    public MainMenuGUI mainMenuGUI;
    public PauseMenuGUI pauseMenuGUI;

    public AbstractGUI openedGUI = null;
    public static Jeu Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        OpenGUI(mainMenuGUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGUI(AbstractGUI gui)
    {
        if (gui == null)
        {
            Debug.LogError("GUI is null");
            return;
        }

        if (openedGUI != null)
        {
            //clear le gui ouvert.
            CloseGUI();
        }
        this.openedGUI = gui;
        openedGUI.OnOpenGui();
        inputIntegration.SwitchToGuiMap(out string _);
        Instance.playerProperties.movementManager.OnGuiOpened();
        HideControlHelp();
    }

    public void CloseGUI()
    {
        this.openedGUI?.OnCloseGui();
        this.openedGUI = null;
        inputIntegration.SwitchToOverworldMap(out string _);
        ShowControlHelp();
    }

    public void OnGuiMove(Vector2 dir)
    {
        if (openedGUI != null)
        {
            openedGUI.OnGuiMove(dir);
        }
    }

    public void OnGuiSelect()
    {
        if (openedGUI != null)
        {
            openedGUI.OnGuiSelect();
        }
    }

    public void HideControlHelp()
    {
        ControlUIManager.gameObject.SetActive(false);
    }

    public void ShowControlHelp()
    {
        ControlUIManager.gameObject.SetActive(true);
    }

    public void OnPreSave(Sauvegarde save)
    {
        Debug.Log("OnPreSave");
        playerProperties.inventaire.OnPreSave(save);
        entityManager.OnPreSave(save);
        playerProperties.movementManager.OnPreSave(save);
        Debug.Log("OnPreSave done");
    }

    public void OnPostLoad(Sauvegarde save)
    {
        Debug.Log("OnPostLoad");
        CloseGUI();
        playerProperties.inventaire.OnPostLoad(save);
        entityManager.OnPostLoad(save);
        playerProperties.movementManager.OnPostLoad(save);

        //Joueur peut maintenant être intéragit avec.
        playerProperties.Alive = true;
        Debug.Log("OnPostLoad done");
    }
}
