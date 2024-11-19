using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float vitesse;
    public float baseVitesse;
    public Rigidbody2D ceci;
    Vector3 deplacement;
    bool inventaireIsOuvert;
    public GameObject inventaire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 startMarker;
    public Vector2 endMarker;

    private float movementSpeed = 1.0F;
    private readonly int framesForMovement = 45;
    private float movementStartedFramesAgo = -1;

    void Update()
    {
        if (!inventaire.activeInHierarchy)
        {
            bool inMovement = false;
            if (movementStartedFramesAgo != -1)
            {
                float ratioCovered = (movementStartedFramesAgo / (framesForMovement / movementSpeed));
                Vector2 lerpPosition = Vector2.Lerp(startMarker, endMarker, ratioCovered);
                ceci.position = lerpPosition;
                movementStartedFramesAgo += 1;
                if (ratioCovered >= 1)
                {
                    movementStartedFramesAgo = -1;
                }
                if (ratioCovered > -1)
                {
                    inMovement = true;
                }
            }



            if (!inMovement)
            {
                Vector2 velocity;
                vitesse = baseVitesse;
                if (Keybinds.Courir.isPressed)
                {
                    vitesse += baseVitesse;
                }
                if (Keybinds.Up.isPressed)
                {
                    deplacement = new Vector3(0, 1, 0);
                    velocity = deplacement;
                }
                else if (Keybinds.Down.isPressed)
                {
                    deplacement = new Vector3(0, -1, 0);
                    velocity = deplacement;
                }
                else if (Keybinds.Right.isPressed)
                {
                    deplacement = new Vector3(1, 0, 0);
                    velocity = deplacement;
                }
                else if (Keybinds.Left.isPressed)
                {
                    deplacement = new Vector3(-1, 0, 0);
                    velocity = deplacement;
                }
                else velocity = new Vector3(0, 0, 0);

                transform.eulerAngles = new Vector3(
                    0,
                    0,
                    0
                );

                
                if (velocity != new Vector2(0, 0))
                {
                    movementSpeed = vitesse / baseVitesse;
                    startMarker = ceci.position;
                    endMarker = startMarker + velocity;
                    movementStartedFramesAgo = 0;
                }
                
            }
        }
    }
}
