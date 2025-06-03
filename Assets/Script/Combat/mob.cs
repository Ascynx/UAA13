using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Attaque;
using static Mob;
using static Unity.VisualScripting.Member;

[CreateAssetMenu(fileName = "Mobs", menuName = "Mobs/New Mob")]
public class Mob : ScriptableObject, Typings.ITyped
{
    public Typings.Type Type1;
    public Typings.Type Type2;

    public int pvmax;
    public int pvactuel;
    public int def;

    public Attaque[] attack;

    public Sprite sprite;

    Typings.Type Typings.ITyped.Type1 => Type1;
    Typings.Type Typings.ITyped.Type2 => Type2;

    /// <summary>
    /// Remet les Points d'actions au maximum.
    /// </summary>
    public void ResetPP()
    {
        foreach (Attaque item in attack)
        {
            if (item != null)
            {
                item.ppact = item.ppmax;
            }
        }
    }

    public Mob(Typings.Type type1, Typings.Type type2, int pvmax, Attaque[] attack)
    {
        Type1 = type1;
        Type2 = type2;
        this.pvmax = pvmax;
        this.attack = attack;
    }

    public MobCombatCompanion CreateCompanion()
    {
        return new MobCombatCompanion(this);
    }

    public Color GetColor1()
    {
        switch (Type1)
        {
            case Typings.Type.Fire:
                return Color.red;
            case Typings.Type.Elec:
                return Color.yellow;
            case Typings.Type.Aerial:
                return Color.blue;
            case Typings.Type.Water:
                return Color.cyan;
            case Typings.Type.Boss:
                return Color.black;
            case Typings.Type.SemiBoss:
                return Color.gray;
            default:
                return Color.white;
        }
    }
    public Color GetColor2()
    {
        switch (Type2)
        {
            case Typings.Type.Fire:
                return Color.red;
            case Typings.Type.Elec:
                return Color.yellow;
            case Typings.Type.Aerial:
                return Color.blue;
            case Typings.Type.Water:
                return Color.cyan;
            case Typings.Type.Boss:
                return Color.black;
            case Typings.Type.SemiBoss:
                return Color.gray;
            default:
                return Color.white;
        }
    }

    public class MobCombatCompanion : Typings.IAfflictable, Typings.ITyped
    {
        Typings.Type type1;
        Typings.Type type2;

        Typings.Type Typings.ITyped.Type1 => type1;
        Typings.Type Typings.ITyped.Type2 => type2;

        private Dictionary<string, Typings.Effect> effects = new();
        private Dictionary<string, int> effectTurns = new();

        public Dictionary<string, Typings.Effect> AppliedEffects => effects;
        public Dictionary<string, int> EffectTurns { get => effectTurns; set => effectTurns = value; }
        public MobCombatCompanion(Mob mob)
        {
            type1 = mob.Type1;
            type2 = mob.Type2;
        }

        public void AddEffect(string source, Typings.Effect effect)
        {
            if (AppliedEffects.ContainsKey(source))
            {
                AppliedEffects[source] = effect;
            }
            else
            {
                AppliedEffects.Add(source, effect);
            }

            if (EffectTurns.ContainsKey(source))
            {
                EffectTurns.Remove(source);
            }
            EffectTurns.Add(source, 10);
        }

        public void TickDown()
        {
            IEnumerable<string> keys = new List<string>(EffectTurns.Keys);
            foreach (string source in keys)
            {
                if (!EffectTurns.Keys.Contains(source))
                {
                    continue;
                }

                EffectTurns[source]--;
                if (EffectTurns[source] <= 0)
                {
                    AppliedEffects.Remove(source);
                    EffectTurns.Remove(source);
                }
            }
        }

        public Dictionary<Typings.Effect, int> GetAccumulatedEffects()
        {
            Dictionary<Typings.Effect, int> accumulatedEffects = new Dictionary<Typings.Effect, int>();
            foreach (KeyValuePair<string, Typings.Effect> effect in AppliedEffects)
            {
                if (Typings.GetEffectTypeOf(effect.Value) == Typings.EffectType.Unknown)
                {
                    Debug.LogWarning("Type d'effet inconnu pour " + effect.Value);
                    continue;
                }

                if (accumulatedEffects.ContainsKey(effect.Value))
                {
                    if (accumulatedEffects[effect.Value] >= Typings.GetCappedEffectLevel(effect.Value))
                    {
                        //already maxed out.
                        continue;
                    }
                    accumulatedEffects[effect.Value]++;
                }
                else
                {
                    accumulatedEffects.Add(effect.Value, 1);
                }
            }
            return accumulatedEffects;
        }

        ///nullable enable
        public Typings.Effect? GetFirstEnvironmentalEffect()
        {
            foreach (KeyValuePair<string, Typings.Effect> effect in AppliedEffects)
            {
                if (Typings.GetEffectTypeOf(effect.Value) == Typings.EffectType.Environmental)
                {
                    return effect.Value;
                }
            }
            return null;
        }

        public bool IsEffectSourceApplied(string source)
        {
            return AppliedEffects.ContainsKey(source);
        }

        public void ClearEffects()
        {
            AppliedEffects.Clear();
            EffectTurns.Clear();
        }
    }
}
