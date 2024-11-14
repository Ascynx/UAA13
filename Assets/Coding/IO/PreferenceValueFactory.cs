using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceValueFactory
{
    public static ValueHandle<string> CreateHandleOf(string key, string t)
    {
        return new StringHandle(key);
    }

    public static ValueHandle<int> CreateHandleOf(string key, int t)
    {
        return new IntHandle(key);
    }

    public static ValueHandle<float> CreateHandleOf(string key, float t)
    {
        return new FloatHandle(key);
    }

    public static ValueHandle<bool> CreateHandleOf(string key, bool t)
    {
        return new BooleanHandle(key);
    }

    public record BooleanHandle : ValueHandle<bool>
    {
        public BooleanHandle(string key) : base(key) { }

        public override bool GetValue()
        {
            return PlayerPrefs.GetInt(_key) == 1;
        }

        public override void SetValue(bool value)
        {
            PlayerPrefs.SetInt(_key, value ? 1 : 0);
        }
    }

    public record FloatHandle : ValueHandle<float>
    {
        public FloatHandle(string key) : base(key) { }

        public override float GetValue()
        {
            return PlayerPrefs.GetFloat(_key);
        }

        public override void SetValue(float value)
        {
            PlayerPrefs.SetFloat(_key, value);
        }
    }

    public record IntHandle : ValueHandle<int>
    {
        public IntHandle(string key) : base(key) { }

        public override int GetValue()
        {
            return PlayerPrefs.GetInt(_key);
        }

        public override void SetValue(int value)
        {
            PlayerPrefs.SetInt(_key, value);
        }
    }

    public record StringHandle : ValueHandle<string>
    {
        public StringHandle(string key): base(key) {}

        public override string GetValue()
        {
            return PlayerPrefs.GetString(_key);
        }

        public override void SetValue(string value)
        {
            PlayerPrefs.SetString(_key, value);
        }
    }

    public abstract record ValueHandle<T>
    {
        protected string _key;

        protected ValueHandle(string key)
        {
            _key = key;
        }

        public abstract T GetValue();

        public abstract void SetValue(T value);

        protected static void Clear(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
