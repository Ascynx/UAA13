using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CombatUIPlayerController : MonoBehaviour
{
    [SerializeField]
    private Shader shaderBasPV;
    [SerializeField]
    private List<ButtonFight> listeAttaques;

    [SerializeField]
    private List<EffectWidget> statusWidgets;

    public TextMeshProUGUI playerPv;
    public Image playerPvBase;
    public Image playerSprite;

    private int currPV = 0;
    private int maxPV = 0;


    public int AttaqueCount() { return listeAttaques.Count; }
    public ButtonFight GetButton(int index)
    {
        return listeAttaques[index];
    }
    public void SetMaxPV(int maxPV)
    {
        this.maxPV = maxPV;
        UpdateStatus();
    }

    public void DamageEffect(int mod)
    {
        if (mod > 0)
        {
            //damage
            Material mat = new(shaderBasPV)
            {
                mainTexture = playerSprite.mainTexture
            };
            mat.SetFloat("_rate", 15);
            playerSprite.material = mat;

        } else if (mod < 0)
        {
            //heal
        }
    }
    public void SetPV(int pv)
    {
        this.currPV = pv;
        UpdateStatus();
    }

    public int GetCurrPV()
    {
        return currPV;
    }

    public void RemoveMaterialIfMatch(Shader shader)
    {
        if (playerPvBase.material.shader == shader)
        {
            playerPvBase.material = null;
        }
    }

    public void ApplyEffectShader(Material mat)
    {
        if (mat == null)
        {
            playerPvBase.material = null;
            return;
        }
        mat.mainTexture = playerPv.mainTexture;
        playerPvBase.material = mat;
    }

    private IEnumerator LowHPTimerCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        LowHPTimerLogic();
    }

    private void LowHPTimerLogic()
    {

        if (playerSprite.material.shader == shaderBasPV)
        {
            playerSprite.material = null;
        }

        if (((float)currPV / (float)maxPV) < 0.15f)
        {
            //inférieur à 15% de vie restante
            Material mat = new(shaderBasPV)
            {
                mainTexture = playerSprite.mainTexture
            };

            playerSprite.material = mat;
        }
    }


    private Coroutine lowHPTimerCoroutine;
    private void UpdateStatus()
    {
        if (maxPV <= 0) return;
        playerPv.text = currPV.ToString() + "/" + maxPV.ToString() + " PV";

        if (lowHPTimerCoroutine != null)
        {
            StopCoroutine(lowHPTimerCoroutine);
        }
        lowHPTimerCoroutine = StartCoroutine(LowHPTimerCoroutine());
    }

    public void UpdateAppliedEffects()
    {
        PlayerProperties properties = Jeu.Instance.playerProperties;

        Dictionary<Typings.Effect, int> effects = properties.GetAccumulatedEffects();
        int i = 0;
        int max = statusWidgets.Count;
        foreach (KeyValuePair<Typings.Effect, int> effect in effects)
        {
            if (i >= max)
            {
                break;
            }

            if (Typings.GetEffectTypeOf(effect.Key) != Typings.EffectType.Status)
            {
                continue;
            }
            statusWidgets[i].UpdateAppliedEffect(effect.Key, effect.Value);
            i++;
        }
    }

    public void UnsetAllAppliedEffects()
    {
        foreach (EffectWidget widget in statusWidgets)
        {
            widget.UpdateAppliedEffect(Typings.Effect.None, 0);
        }
    }

    private void OnDisable()
    {
        playerSprite.material = null;
        playerPvBase.material = null;
        UnsetAllAppliedEffects();
    }

    private void Awake()
    {
    }
}
