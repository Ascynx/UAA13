using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveIconControl : MonoBehaviour
{
    [SerializeField]
    Image sprite;

    private void Awake()
    {
        sprite.enabled = false;
    }

    public void SetActive()
    {
        sprite.enabled = true;
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Active", true);
    }

    public void SetInactive()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Active", false);
    }

    public void HideSprite()
    {
        sprite.enabled = false;
    }

    public void PostRunDone()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("CanRunDone", false);
    }

    public void PostRunActive()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("CanRunDone", true);
    }
}
