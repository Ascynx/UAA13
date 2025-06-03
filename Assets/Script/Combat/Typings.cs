using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Typings
{
    public static Sprite Unset => Jeu.Instance.statusEffectSpriteAtlas.GetSprite("uns_0");

    public static string GetSpriteKeyOf(Effect effect, int lvl = 0)
    {
        string baseKey = effect switch
            {
            Effect.Paralize => "par",
            Effect.Burn => "bur",
            Effect.Toxic => "tox",
            Effect.PrecisionLower => "pre",
            Effect.PowerLower => "pow",
            Effect.DefLower => "def",
            _ => "uns",
        };

        if (effect == Effect.PrecisionLower || effect == Effect.PowerLower || effect == Effect.DefLower)
        {
            lvl = -lvl;
        }
        return baseKey + "_" + lvl;
    }

    public static Optional<Sprite> GetSpriteOf(Effect effect, int lvl = 0)
    {
        if (GetEffectTypeOf(effect) != EffectType.Status)
        {
            return Optional<Sprite>.Empty();
        }

        SpriteAtlas atlas = Jeu.Instance.statusEffectSpriteAtlas;
        return Optional<Sprite>.OfNullable(atlas.GetSprite(GetSpriteKeyOf(effect, lvl)));
    }

    public static EffectType GetEffectTypeOf(Effect effect)
    {
        EffectType type = effect switch
        {
            Effect.Paralize => EffectType.Environmental,
            Effect.Burn => EffectType.Environmental,
            Effect.Toxic => EffectType.Environmental,
            Effect.PrecisionLower => EffectType.Status,
            Effect.PowerLower => EffectType.Status,
            Effect.DefLower => EffectType.Status,
            _ => EffectType.Unknown,
        };
        return type;
    }

    public static int GetCappedEffectLevel(Effect effect)
    {
        int v = 1;
        v = GetEffectTypeOf(effect) switch
        {
            EffectType.Environmental => 1,
            EffectType.Status => 3,
            _ => 0,
        };
        return v;
    }

    [System.Serializable]
    public enum Type
    {
        None,
        Fire,
        Elec,
        Aerial,
        Water,
        Boss,
        SemiBoss,
    }
    [System.Serializable]
    public enum Effect
    {
        None,
        Paralize,
        Burn,
        Toxic,
        PrecisionLower,
        PowerLower,
        DefLower,
    }

    [System.Serializable]
    public enum EffectType
    {
        Status,
        Environmental,
        Unknown
    }

    public interface IAfflictable
    {
        public Dictionary<string, Effect> AppliedEffects { get; }
        public Dictionary<string, int> EffectTurns { get; set; }

        void AddEffect(string source, Effect effect);

        void TickDown();
        Dictionary<Effect, int> GetAccumulatedEffects();

        ///nullable enable
        Effect? GetFirstEnvironmentalEffect();

        bool IsEffectSourceApplied(string source);

        void ClearEffects();
    }

    public interface ITyped
    {
        public Type Type1 { get; }
        public Type Type2 { get; }

        /// <summary>
        /// L'un des types dit qu'il est un boss
        /// </summary>
        /// <returns></returns>
        public bool IsBoss()
        {
            return Type1 == Type.Boss || Type2 == Type.Boss;
        }

        /// <summary>
        /// L'un des types dit qu'il est soit un boss soit un demi boss
        /// </summary>
        /// <returns></returns>
        public bool IsSemiBoss()
        {
            return IsBoss() || Type1 == Type.SemiBoss || Type2 == Type.SemiBoss;
        }


        /// <summary>
        /// L'un des types correspond au type demandé.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsType(Type type)
        {
            return Type1 == type || Type2 == type;
        }

        /// <summary>
        /// Les 2 types correspondent aux types demandés.
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public bool IsTypeAndType(Type type1, Type type2)
        {
            return (Type1 == type1 && Type2 == type2) || (Type1 == type2 && Type2 == type1);
        }
    }
}
