using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float baseVitesse;
    public Rigidbody2D ceci;
    bool inventaireIsOuvert;
    public GameObject inventaire;
    // Start is called before the first frame update
    void Start()
    {
    }

    public float accelerationDirection = 3F;

    public void OnDirection(InputAction.CallbackContext valeur)
    {

    }

    void Update()
    {
        if (!inventaire.activeInHierarchy)
        {
            Vector2 velocity = new Vector2(0.0F, 0.0F);
            if (Keybinds.Up.isPressed)
            {
                velocity += new Vector2(0, 1);
            }
            if (Keybinds.Down.isPressed)
            {
                velocity += new Vector2(0, -1);
            }

            if (Keybinds.Right.isPressed)
            {
                velocity += new Vector2(1, 0);
            }
            if (Keybinds.Left.isPressed)
            {
                velocity += new Vector2(-1, 0);
            }

            float speed = baseVitesse * (Keybinds.Courir.isPressed ? 2 : 1);
            if (velocity.magnitude > 1) velocity.Normalize();
            Vector2 targetVelocity = velocity * speed;
            Vector2 newVelocity = Vector2.MoveTowards(ceci.velocity, targetVelocity, Time.deltaTime * accelerationDirection);
            
            ceci.velocity = newVelocity;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    [SerializeField][Range(0.5F, 2)] private float arrowLength = 1.0F;
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        var position = transform.position;
        var velocity = ceci.velocity;

        if (velocity.magnitude < 0.1f) return;
        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }

        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(new Vector3(velocity.x, velocity.y, 0)), arrowLength, EventType.Repaint);
    }
}
