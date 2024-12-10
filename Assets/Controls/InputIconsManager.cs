using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class InputIconsManager : ScriptableObject
{

    private readonly Dictionary<string, Sprite> _icones_vrac = new Dictionary<string, Sprite>();
    void Awake()
    {
        Sprite[] resources = Resources.LoadAll<Sprite>("ControlIcons");
        for (int i = 0; i < resources.Length; i++)
        {
            Sprite tex = resources[i];
            _icones_vrac[tex.name] = tex;
        }

        SortLoadedIcons();

        KeyIcon[] keys = _icones_triees.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            Debug.Log("Loaded and sorted sprite: " + keys[i]);
        }
    }

    public Sprite GetIconTex(KeyIcon icon)
    {
        return _icones_triees.GetValueOrDefault(icon, null);
    }

    public bool TryGetIconTex(KeyIcon icon, out Sprite tex)
    {
        return _icones_triees.TryGetValue(icon, out tex);
    }

#nullable enable
    public bool FindIconTex(string scheme, string key, out Sprite? tex)
    {
        return FindIconTex(scheme, key, null, null, out tex);
    }

#nullable enable
    public bool FindIconTex(string scheme, string key, string? style, out Sprite? tex)
    {
        return FindIconTex(scheme, key,  null, style, out tex);
    }

    #nullable enable
    public bool FindIconTex(string scheme, string key, string? spec, string? style, out Sprite? tex)
    {
        if (scheme == null || key == null)
        {
            tex = null;
            return false;
        }
        for (int i = 0; i < _icones_triees.Count; i++)
        {
            KeyValuePair<KeyIcon, Sprite> kvp = _icones_triees.ElementAt(i);
            KeyIcon keyIcon = kvp.Key;
            if (!keyIcon.Scheme.StartsWith(scheme) || !keyIcon.Key.StartsWith(key))
            {
                continue;
            }

            if (style != null && style != " ")
            {
                if (!keyIcon.Style.StartsWith(style))
                {
                    continue;
                }
            }

            if (spec != null && spec != " ")
            {
                if (!keyIcon.Spec.StartsWith(spec))
                {
                    continue;
                }
            }

            tex = kvp.Value;
            return true;
        }
        tex = null;
        return false;
    }

    private readonly Regex _icon_pattern = new("^T_(?<scheme>P4|X|P5|S|Keyboard)?_?(?<key>[a-zA-Z0-9]*|[a-zA-Z0-9_]*_Key)_?(?<spec>[a-zA-Z0-9]*)?_?(?<style>[a-zA-Z0-9_]*)-?(?<alt>1)?$", RegexOptions.IgnoreCase);


    private readonly Dictionary<KeyIcon, Sprite> _icones_triees = new();
    private void SortLoadedIcons()
    {
        string[] keys = _icones_vrac.Keys.ToArray();
        Sprite[] textures = _icones_vrac.Values.ToArray();
        for (int i = 0; i < _icones_vrac.Count; i++)
        {
            string key = keys[i];
            Sprite tex = textures[i];
            if (!GetKeyParsed(key, out KeyIcon? icon) || icon == null)
            {
                Debug.LogError("Could not parse texture key: " + key + ", throwing it away.");
                continue;
            }

            _icones_triees[icon.Value] = tex;
        }
    }

    private bool GetKeyParsed(string name, out KeyIcon? icon)
    {
        Match match = _icon_pattern.Match(name);
        if (!match.Success)
        {
            icon = null;
            return false;
        }

        string? scheme = null;
        string? style = null;
        string? key = null;
        string? spec = null;

        for (int i = 0; i < match.Groups.Count; i++)
        {
            Group group = match.Groups[i];
            switch(group.Name)
            {
                case "0": break; //on évite le groupe par défaut, sinon ça va dans default, ce qui va montrer une erreur.
                case "scheme": {
                        if (group.Value == "") break; //le groupe est vide, on skip;
                        scheme = group.Value; break;
                    }
                case "style":
                    {
                        if (group.Value == "") break; //le groupe est vide, on skip;
                        style = group.Value; break;
                    }
                case "key":
                    {
                        if (group.Value == "") break; //le groupe est vide, on skip;
                        key = group.Value; break;
                    }
                case "alt":
                case "spec":
                    {
                        if (group.Value == "") break; //le groupe est vide, on skip;
                        spec = group.Value; break;
                    }
                default:
                    {
                        Debug.LogError("Found unexpected group '" + group.Name + "' with value '"+ group.Value + "' in icon key format parser.");
                        break;
                    }
            }
        }

        if (key == null)
        {
            throw new System.Exception("Failed to parse icon key:  " + name);
        }

        Debug.Log("creating new KeyIcon: scheme=" + scheme + ", style=" + style + ", key=" + key +", spec=" + spec);
        icon = new KeyIcon(scheme, style, key, spec);
        return true;
    }

    public struct KeyIcon
    {
        private string _scheme;
        private string _style;
        private string _key;
        private string _spec;

        public string Scheme { get { return _scheme; } }
        public string Style { get { return _style; } }
        public string Key { get { return _key; } }
        public string Spec { get { return _spec; } }


        #nullable enable
        public KeyIcon(string? scheme, string? style, string key, string? spec)
        {
            if (scheme == null)
            {
                scheme = "Keyboard";
            }

            _scheme = scheme;
            _style = style ?? "";
            _key = key;
            _spec = spec ?? "";
        }

        public override string ToString()
        {
            return "scheme: " + Scheme + ", style: " + Style + ", key: " + Key + ", spec: " + Spec;
        }
    }
}
