using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json; // Ensure Newtonsoft.Json is available

public static class SerializationUtility
{
    // --- JSON METHODS ---
    public static string ToJson<T>(T data, bool prettyPrint = false)
    {
        return JsonConvert.SerializeObject(data, prettyPrint ? Formatting.Indented : Formatting.None);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    // --- YAML METHODS (Basic) ---
    public static string ToYaml<T>(T data)
    {
        Dictionary<string, object> dict = ConvertToDictionary(data);
        return DictionaryToYaml(dict);
    }

    public static T FromYaml<T>(string yaml)
    {
        Dictionary<string, object> dict = YamlToDictionary(yaml);
        string json = JsonConvert.SerializeObject(dict);
        return FromJson<T>(json);
    }

    // --- FILE HANDLING ---
    public static void SaveToFile(string filePath, string data)
    {
        File.WriteAllText(filePath, data);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public static string LoadFromFile(string filePath)
    {
        return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
    }

    // --- HELPERS ---
    private static Dictionary<string, object> ConvertToDictionary(object obj)
    {
        string json = ToJson(obj);
        return FromJson<Dictionary<string, object>>(json);
    }

    private static string DictionaryToYaml(Dictionary<string, object> dict)
    {
        string yaml = "";
        foreach (var kvp in dict)
        {
            yaml += $"{kvp.Key}: {kvp.Value}\n";
        }
        return yaml.Trim();
    }

    private static Dictionary<string, object> YamlToDictionary(string yaml)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        string[] lines = yaml.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(new[] { ':' }, 2);
            if (parts.Length == 2)
            {
                dict[parts[0].Trim()] = parts[1].Trim();
            }
        }
        return dict;
    }
}
