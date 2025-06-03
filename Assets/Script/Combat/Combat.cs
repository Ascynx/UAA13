using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Combat : AbstractGUI
{
    public Transform FightBackground;
    Mob attacking;
    Mob.MobCombatCompanion attackingCompat;
    Transform tattacking;

    public TextMeshProUGUI textPlayerPV, textMobPV;
    public Image playerSprite, mobSprite;
    public CombatUIPlayerController playerUIController;
    public CombatUIMobController mobUIController;
    public Transform Player;
    public GameObject GameOverCamera;

    int PlayerPV {
        set
        {
            if (playerUIController != null)
            {
                playerUIController.SetPV(value);
            }
        }
        get
        {
            if (playerUIController != null)
            {
                return playerUIController.GetCurrPV();
            }
            return -1; //�tat bugg�, le CombatUIPlayerController devrait exister dans tout contexte.
        }
    }

    public Attaque[] attack = new Attaque[4];

    public override void OnCloseGui()
    {
    }
    public override void OnOpenGui()
    {
        currentIndex = 0;
        UpdateSelection();
    }

    public sbyte currentIndex = 0;
    public override void OnGuiMove(Vector2 dir)
    {
        if (dir == Vector2.left)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        } else if (dir == Vector2.right)
        {
            if (currentIndex < playerUIController.AttaqueCount() - 1)
            {
                currentIndex++;
            }
        }
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        for (int i = 0; i < playerUIController.AttaqueCount(); i++)
        {
            ButtonFight button = playerUIController.GetButton(i);
            if (i == currentIndex)
            {
                button.selected = true;
            }
            else
            {
                button.selected = false;
            }
        }
    }

    public override void OnGuiSelect()
    {
        for (int i = 0; i < playerUIController.AttaqueCount(); i++)
        {
            ButtonFight button = playerUIController.GetButton(i);
            if (i == currentIndex)
            {
                button.OnClick();
            }
        }
    }

    public override bool CanBeEscaped()
    {
        return false;
    }
    public void Fight(Mob attacking, Transform tattacking)
    {
        Jeu.Instance.HideControlHelp();

        PlayerProperties playerProperties = Jeu.Instance.playerProperties;
        Inventory inventaire = playerProperties.inventaire;

        attackingCompat = attacking.CreateCompanion();

        playerSprite.sprite = playerProperties.playerSpriteAtlas.GetSprite("PlayerUp");
        mobSprite.sprite = attacking.sprite;

        mobUIController.SetGradient(attacking.GetColor1(), attacking.GetColor2());

        playerProperties.ClearEffects();
        attackingCompat.ClearEffects();
        this.attacking = attacking;
        this.tattacking = tattacking;

        if (inventaire.equippedSword != null)
        {
            initAtt(inventaire.equippedSword, out attack[0]);
        }
        else
        {
            attack[0] = Attaque.Lutte;
        }
        if (inventaire.equippedParchemins.Count >=1)
        {
            if (inventaire.equippedParchemins[0] != null)
            {
                initAtt(inventaire.equippedParchemins[0], out attack[1]);
            }
            else
            {
                attack[1] = Attaque.Lutte;
            }
        }
        else
        {
            attack[1] = Attaque.Lutte;
        }
        if (inventaire.equippedParchemins.Count >= 2)
        {
            if (inventaire.equippedParchemins[1] != null)
            {
                initAtt(inventaire.equippedParchemins[1], out attack[2]);
            }
            else
            {
                attack[2] = Attaque.Lutte;
            }
        }
        else
        {
            attack[2] = Attaque.Lutte;
        }
        if (inventaire.equippedParchemins.Count >= 3)
        {
            if (inventaire.equippedParchemins[2] != null)
            {
                initAtt(inventaire.equippedParchemins[2], out attack[3]);
            }
            else
            {
                attack[3] = Attaque.Lutte;
            }
        }
        else
        {
            attack[3] = Attaque.Lutte;
        }
        FightBackground.gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            ButtonFight button = playerUIController.GetButton(i);
            UpdateButton(button, attack[i]);
        }
        PlayerPV = playerProperties.hp;
        playerUIController.SetMaxPV(PlayerPV);
        attacking.pvactuel = attacking.pvmax;

        textMobPV.text = attacking.pvactuel.ToString() + "/" + attacking.pvmax.ToString() + " PV";
        attacking.ResetPP();

        RunLoadInAnimation();
    }


    private bool inAnim = false;
    private float animLoadedPercent = 0.0f;
    private readonly float rate = 0.025f;
    private readonly int LOAD_APPLIED_RELATIVE_POSITION = 1000;
    public void RunLoadInAnimation()
    {
        inAnim = true;
        for (int i = 0; i < 4; i++)
        {
            ButtonFight buttonFight = playerUIController.GetButton(i);
            buttonFight.canClick = false;
            Transform buttonTransform = buttonFight.transform;
            buttonTransform.SetPositionAndRotation(buttonTransform.position - new Vector3(LOAD_APPLIED_RELATIVE_POSITION, 0, 0), buttonTransform.rotation);
            animLoadedPercent = 0.0f;
        }
    }


    private void Update()
    {
        bool wasInAnim = inAnim;
        if (inAnim && animLoadedPercent < 1f)
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonFight button = playerUIController.GetButton(i);
                Transform transform = button.transform;
                transform.SetPositionAndRotation(transform.position + new Vector3(rate * LOAD_APPLIED_RELATIVE_POSITION, 0, 0), transform.rotation);
            }
            animLoadedPercent += rate;
            if (animLoadedPercent >= 1f) inAnim = false;
        }

        if (wasInAnim && !inAnim)
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonFight buttonFight = playerUIController.GetButton(i);
                buttonFight.ResetPosition();
                buttonFight.canClick = true;
            }
            animLoadedPercent = 0.0f;
        }
    }

    public void UpdateButton(ButtonFight button, Attaque atq)
    {
        UpdateButton(button, atq.nom, atq.ppact, atq.ppmax, atq.sprite);
    }

    public void UpdateButton(ButtonFight button, string nom, int pp, int ppmax, Sprite sprite)
    {
        button.NomAttaque.text = nom;
        if (pp == 1 && ppmax == 1)
        {
            button.PPAttaque.text = "MAX";
        } else
        {
            button.PPAttaque.text = pp.ToString() + "/" + ppmax.ToString();
        }
        button.SetEquippedSprite(sprite);
    }

    public void ResetButton(ButtonFight button)
    {
        button.NomAttaque.text = "";
        button.PPAttaque.text = "";
        button.SetEquippedSprite(null);
    }

    public void FightAdvence(int att)
    {
        PlayerProperties properties = Jeu.Instance.playerProperties;
        attack[att].ppact--;
        initTurn(attack[att]);

        for (int i = 0; i < 4; i++)
        {
            if (attack[i].ppact == 0)
            {
                attack[i] = Attaque.Lutte;
            }
            ButtonFight button = playerUIController.GetButton(i);
            UpdateButton(button, attack[i]);
        }
        for (int i = 0; i < attacking.attack.Length; i++)
        {
            if (attacking.attack[i].ppact == 0)
            {
                attacking.attack[i] = Attaque.Lutte;
            }
        }
        if (PlayerPV <= 0 || attacking.pvactuel <= 0)
        {
            if (attacking.pvactuel <= 0)
            {
                Jeu.Instance.CloseGUI();
                tattacking.gameObject.SetActive(false);
            }
            else
            {
                Jeu.Instance.OpenGUI(GameOverCamera.GetComponentInParent<GameOverGUI>());
            }
            FightBackground.gameObject.SetActive(false);
        }
        else
        {
            textMobPV.text = attacking.pvactuel.ToString() + "/" + attacking.pvmax.ToString() + " PV";

            Color effectColor = Color.white;
            mobUIController.UpdateAppliedEffects(attackingCompat);
            effectColor = Color.white;
            switch (attackingCompat.GetFirstEnvironmentalEffect())
            {
                case Typings.Effect.Paralize:
                    effectColor = Color.yellow;
                    break;
                case Typings.Effect.Burn:
                    effectColor = Color.red;
                    break;
                case Typings.Effect.Toxic:
                    effectColor = Color.magenta;
                    break;
                default:
                    break;
            }
            if (effectColor != Color.white)
            {
                Material mat = new(Jeu.Instance.basicShiftColoredShader);
                mat.SetColor("_color", effectColor);
                mobUIController.ApplyEffectShader(mat);
            } else
            {
                mobUIController.ApplyEffectShader(null);
            }

            playerUIController.UpdateAppliedEffects();
            effectColor = Color.white;
            switch (properties.GetFirstEnvironmentalEffect())
            {
                case Typings.Effect.Paralize:
                    effectColor = Color.yellow;
                    break;
                case Typings.Effect.Burn:
                    effectColor = Color.red;
                    break;
                case Typings.Effect.Toxic:
                    effectColor = Color.magenta;
                    break;
                default:
                    break;
            }
            if (effectColor != Color.white)
            {
                Material playerMat = new(Jeu.Instance.basicShiftColoredShader);
                playerMat.SetColor("_color", effectColor);
                playerUIController.ApplyEffectShader(playerMat);
            } else
            {
                playerUIController.RemoveMaterialIfMatch(Jeu.Instance.basicShiftColoredShader);
            }
        }
    }
    void initAtt(Item item, out Attaque attaque)
    {
            if (item.GetType() == ItemType.Sword)
            {
                Sword sword = (Sword)item;
                attaque = sword.attaque;
            }
            else
            {
                Parchemin parchemin = (Parchemin)item;
                attaque = parchemin.attaque;
        }
            attaque.ppact = attaque.ppmax;
    }

    void initTurn(Attaque attackPlayer)
    {
        Attaque attackEnnemi;
        int i = 0;
        int tempAE;
        do
        {
            tempAE = UnityEngine.Random.Range(0, attacking.attack.Length);
            attackEnnemi = attacking.attack[tempAE];
            i++;
        } while (i != 10 && ((attackEnnemi.effet != Attaque.effect.None && Jeu.Instance.playerProperties.IsEffectSourceApplied(attackEnnemi.nom)) || attackEnnemi.ppact <= 0));
        if (attackEnnemi.ppact == 0)
        {
            attackEnnemi = Attaque.Lutte;
        }
        else
        {
            attacking.attack[tempAE].ppact--;
        }
        turn(attackEnnemi, attackPlayer);

        //effect tick down.
        attackingCompat.TickDown();
        Jeu.Instance.playerProperties.TickDown();
    }

    void turn(Attaque attackEnnemi, Attaque attackPlayer)
    {
        PlayerProperties playerProperties = Jeu.Instance.playerProperties;
        Inventory inventaire = playerProperties.inventaire;

        int enemyPV = attacking.pvactuel;
        int pv = PlayerPV;

        enemyPV = attaque(attackPlayer, playerProperties, attackingCompat, attacking.def, enemyPV);
        if (enemyPV > 0)
        {
            enemyPV = AppliqueEffet(attackPlayer, attacking, attackingCompat, enemyPV);
            if (inventaire.equippedShield != null)
            {
                pv = attaque(attackEnnemi, attackingCompat, playerProperties, inventaire.equippedShield.def, pv);
            }
            else
            {
                pv = attaque(attackEnnemi, attackingCompat, playerProperties, 0, pv);
            }
            if (pv > 0)
            {
                pv = AppliqueEffet(attackEnnemi, playerProperties, playerProperties, pv);
                if (enemyPV > 0)
                {
                    foreach (Typings.Effect pEffect in attackingCompat.GetAccumulatedEffects().Values)
                    {
                        if (Typings.GetEffectTypeOf(pEffect) != Typings.EffectType.Environmental)
                        {
                            continue;
                        }
                        enemyPV = apliqueEtat(attacking.pvmax, pEffect, enemyPV);
                    }
                    if (enemyPV > 0)
                    {
                        foreach (Typings.Effect pEffect in playerProperties.GetAccumulatedEffects().Values)
                        {
                            if (Typings.GetEffectTypeOf(pEffect) != Typings.EffectType.Environmental)
                            {
                                continue;
                            }
                            pv = apliqueEtat(100, pEffect, pv);
                        }
                    }
                }
            }
        }
        playerUIController.DamageEffect(PlayerPV - pv);
        PlayerPV = pv;

        attacking.pvactuel = enemyPV;
    }
    int AppliqueEffet(Attaque att, Typings.ITyped target, Typings.IAfflictable effects, int pv)
    {
        bool isBoss = target.IsBoss();
        bool isSemiBoss = target.IsSemiBoss();

        Attaque.effect effect = att.effet;
        if (effect == Attaque.effect.Burn)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && !target.IsType(Typings.Type.Fire) && !isBoss)
            {
                effects.AddEffect(att.nom + "-Burn", Typings.Effect.Burn);
            }
        }
        else
        if (effect == Attaque.effect.Paralize)
        {
            if (UnityEngine.Random.Range(0, 10) == 0 && !target.IsType(Typings.Type.Elec) && !isBoss)
            {
                effects.AddEffect(att.nom + "-Paralize", Typings.Effect.Paralize);
            }
        }
        else if (effect == Attaque.effect.Toxic && !isBoss)
        {
            effects.AddEffect(att.nom + "-Toxic", Typings.Effect.Toxic);
        }
        else if (effect == Attaque.effect.PowerWind)
        {
            if (target.IsType(Typings.Type.Aerial) && !isSemiBoss)
            {
                return 0; //Instant kill.
            }
        }
        else if (effect == Attaque.effect.LowerPrecision)
        {
            if (UnityEngine.Random.Range(0, 3) >= 0 && !isSemiBoss)
            {
                effects.AddEffect(att.nom + "-LowerPrecision", Typings.Effect.PrecisionLower);
            }
        }
        else if (effect == Attaque.effect.LowerPower)
        {
            if (UnityEngine.Random.Range(0, 3) >= 0 && !isSemiBoss)
            {
                effects.AddEffect(att.nom + "-LowerPower", Typings.Effect.PowerLower);
            }
        }
        else if (effect == Attaque.effect.LowerDef)
        {
            if (UnityEngine.Random.Range(0, 3) >= 0 && !isSemiBoss)
            {
                effects.AddEffect(att.nom + "-LowerDef", Typings.Effect.DefLower);
            }
        }
        return pv;
    }
    int attaque(Attaque att, Typings.IAfflictable effetsA, Typings.IAfflictable effetsE, int def, int pv)
    {
        int precision = att.precision;
        int power = att.power;
        Dictionary<Typings.Effect, int> effetsAcc = effetsA.GetAccumulatedEffects();

        if (effetsAcc.GetValueOrDefault(Typings.Effect.PrecisionLower, 0) > 0)
        {
            precision = precision / 2;
        }
        else if (effetsAcc.GetValueOrDefault(Typings.Effect.PowerLower, 0) > 0)
        {
            power /= 2;
        }
        else if (effetsAcc.GetValueOrDefault(Typings.Effect.DefLower, 0) > 0)
        {
            def -= 1;
        }
        if (UnityEngine.Random.Range(0, 100) <= precision)
        {
            if (effetsAcc.GetValueOrDefault(Typings.Effect.Paralize, 0) > 0 || UnityEngine.Random.Range(0, 1) == 0)
            {
                if (power - def >= 0)
                {
                    return pv - (power - def);
                }
            }
        }
        return pv;
    }
    int apliqueEtat(int pvMax, Typings.Effect effet, int pv)
    {
        if (effet == Typings.Effect.Burn)
        {
            pv -=  10;
        }
        else if (effet == Typings.Effect.Toxic)
        {
            pv -= (pvMax / 10);
        }
        return pv;
    }

    public override void OnSubGuiClosed()
    {
    }

    public override void OnSubGuiOpen()
    {
    }
}

