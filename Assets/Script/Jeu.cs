using UnityEngine;

public class Jeu : MonoBehaviour
{
    //Attention Race conditions.

    [SerializeField]
    public InputSystemIntegration inputIntegration;

    [SerializeField]
    public Canvas UICanvas;

    [SerializeField]
    public Canvas GUICanvas;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public Camera mainCamera;

    [SerializeField]
    public Sauvegarde sauvegarde;

    private static Jeu instance;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
