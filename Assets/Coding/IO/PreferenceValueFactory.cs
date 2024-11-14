using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceValueFactory
{
    public static Value<string> CreateHandleOf(string key, string value)
    {
        return new StringValue(key, value);
    }

    public static Value<int> CreateHandleOf(string key, int value)
    {
        return new IntValue(key, value);
    }

    public static Value<float> CreateHandleOf(string key, float value)
    {
        return new FloatValue(key, value);
    }

    public static Value<bool> CreateHandleOf(string key, bool value)
    {
        return new BooleanValue(key, value);
    }

    public record BooleanValue : Value<bool>
    {
        public BooleanValue(string key,  bool value) : base(key, value) { }

        public override bool GetValue()
        {
            return PlayerPrefs.GetInt(_key) == 1;
        }

        public override void SetValue()
        {
            PlayerPrefs.SetInt(_key, _value ? 1 : 0);
        }
    }

    public record FloatValue : Value<float>
    {
        public FloatValue(string key, float value) : base(key, value) {}

        public override float GetValue()
        {
            return PlayerPrefs.GetFloat(_key);
        }

        public override void SetValue()
        {
            PlayerPrefs.SetFloat(_key, _value);
        }
    }

    public record IntValue : Value<int>
    {
        public IntValue(string key, int value) : base(key, value) {}

        public override int GetValue()
        {
            return PlayerPrefs.GetInt(_key);
        }

        public override void SetValue()
        {
            PlayerPrefs.SetInt(_key, _value);
        }
    }

    public record StringValue : Value<string>
    {
        public StringValue(string key, string value): base (key, value) {}

        public override string GetValue()
        {
            return PlayerPrefs.GetString(_key);
        }

        public override void SetValue()
        {
            PlayerPrefs.SetString(_key, _value);
        }
    }

    public abstract record Value<T>
    {
        protected T _value;
        protected string _key;

        protected Value(string key, T value)
        {
            this._key = key;
            this._value = value;
        }

        public abstract void SetValue();

        public abstract T GetValue();

        void Clear()
        {
            PlayerPrefs.DeleteKey(this._key);
        }
    }
}
