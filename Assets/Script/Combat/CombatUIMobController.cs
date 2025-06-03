using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIMobController : MonoBehaviour
{
    [SerializeField]
    private RawImage baseImage;
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private Image mobPvBase;

    [SerializeField]
    private List<EffectWidget> statusWidgets;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyEffectShader(Material mat)
    {
        if (mat == null)
        {
            mobPvBase.material = null;
            return;
        }
        mat.mainTexture = mobPvBase.mainTexture;
        mobPvBase.material = mat;
    }

    public void SetGradient(Color c1, Color c2)
    {
        if  (c1.Equals(c2) && c1.Equals(Color.white))
        {
            baseImage.enabled = false;
            return;
        }

        if (!baseImage.enabled)
        {
            baseImage.enabled = true;
        }

        if (c2.Equals(Color.white))
        {
            baseImage.color = c1;
            baseImage.material = null;
            return;
        }

        Shader shader = Jeu.Instance.gradientSpriteShader;
        Material material = new(shader)
        {
            mainTexture = baseImage.texture
        };
        material.SetColor("_color1", c1);
        material.SetColor("_color2", c2);

        baseImage.material = material;
    }

    public void UpdateAppliedEffects(Mob.MobCombatCompanion compat)
    {
        Dictionary<Typings.Effect, int> effects = compat.GetAccumulatedEffects();
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
        baseImage.material = null;
        sprite.material = null;
        mobPvBase.material = null;
        UnsetAllAppliedEffects();
    }
}
