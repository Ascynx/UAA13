using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Moving : MonoBehaviour
{
    public float baseVitesse;
    public Rigidbody2D ceci;

    public SpriteAtlas playerSpriteAtlas;
    public SpriteRenderer ActSprite;
    // Start is called before the first frame update

    private InputSystemIntegration integration;
    void Awake()
    {
        integration = gameObject.GetComponent<InputSystemIntegration>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
    }


    public float directionAcceleration = 10F;
    public Boolean estEnSprint = false;
    Vector2 movementVector = Vector2.zero;

    public void OnDirection(InputValue value)
    {
        Vector2 vecteurMouvement = value.Get<Vector2>();
        OnEditMovement(vecteurMouvement);
    }

    public void OnSprint(InputValue value)
    {
        var sprintPressedPercent = value.Get<float>();
        estEnSprint = sprintPressedPercent > 0.5F;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        #endif
    }

    private void OnEditMovement(Vector2 velocity)
    {
        movementVector = velocity.normalized;
    }

    void Update()
    {
        bool inGui = false; //TODO hook ça quand l'inventaire est ouvert.
        if (integration.SetActionState(inGui, "OpenWorld", out bool prev) && (prev && !inGui))
        {
            //l'état précédent est actif, l'état actuel est inactif, on reset donc le vecteur de mouvement.
            movementVector = Vector2.zero;
        }

        float speed = baseVitesse * (estEnSprint ? 2 : 1);
        Vector2 targetVelocity = movementVector * speed;
        ceci.velocity = Vector2.MoveTowards(ceci.velocity, targetVelocity, Time.deltaTime * directionAcceleration);

        if (ceci.velocity != Vector2.zero)
        {
            if (Math.Abs(ceci.velocity.x) > Math.Abs(ceci.velocity.y))
            {
                if (ceci.velocity.x > 0)
                {
                    ActSprite.sprite = playerSpriteAtlas.GetSprite("PlayerRight");
                }
                else
                {
                    ActSprite.sprite = playerSpriteAtlas.GetSprite("PlayerLeft");
                }
            }
            else
            {
                if (ceci.velocity.y > 0)
                {
                    ActSprite.sprite = playerSpriteAtlas.GetSprite("PlayerUp");
                }
                else
                {
                    ActSprite.sprite = playerSpriteAtlas.GetSprite("PlayerDown");
                }
            }
        }
        
        transform.eulerAngles = new Vector3(0, 0, 0);
    }


    #if UNITY_EDITOR
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
    #endif
}
