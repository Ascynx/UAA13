using System;
using Unity.VisualScripting;

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

    public SpriteRenderer ActSprite;
    // Start is called before the first frame update
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
    }

    public void OnPreSave(Sauvegarde save)
    {
        save.PlayerMap.LayerId = Jeu.Instance.player.layer;
        save.PlayerMap.PhysicalLayerId = (int) Jeu.Instance.player.transform.position.z;
        save.PlayerPositionState.Position = ceci.position;
    }

    public void OnPostLoad(Sauvegarde save)
    {
        GameObject player = Jeu.Instance.player;

        TeleportPlayer(player.transform, save.PlayerPositionState.Position, save.PlayerMap.LayerId, save.PlayerMap.PhysicalLayerId);
    }

    private void TeleportPlayer(Transform player, Vector3 newPos, int visualLayer, int physicalLayer)
    {
        if (player.gameObject.layer != visualLayer)
        {
            Physics2D.IgnoreLayerCollision(player.gameObject.layer, visualLayer, true);
            player.position = new Vector3(newPos.x, newPos.y, physicalLayer);
        } else
        {
            //change pas les layers (risque de desynchronisation).
            player.position = new Vector3(newPos.x, newPos.y, player.position.z);
        }

        
        player.gameObject.layer = visualLayer;
    }


    public float directionAcceleration = 10F;
    public Boolean estEnSprint = false;
    Vector2 movementVector = Vector2.zero;

    /// <summary>
    /// Sur mouvement du player controller
    /// </summary>
    /// <param name="value">Le vecteur de mouvement</param>
    public void OnDirection(InputValue value)
    {
        Vector2 vecteurMouvement = value.Get<Vector2>();
        if (vecteurMouvement.magnitude < 0.1F) //évite les mouvements parasites (stick drift)
        {
            vecteurMouvement = Vector2.zero;
        }
        OnEditMovement(vecteurMouvement);
    }

    /// <summary>
    /// Sur sprint du player controller
    /// </summary>
    /// <param name="value">Le no</param>
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

    public void OnGuiOpened()
    {
        movementVector = Vector2.zero;
    }

    void LateUpdate()
    {
        SpriteAtlas playerSpriteAtlas = Jeu.Instance.playerProperties.playerSpriteAtlas;

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
