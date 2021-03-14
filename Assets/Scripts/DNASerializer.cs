using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNASerializer
{
    public static string BASE_PATH = $"{Application.dataPath}/export";

    public static string Serialize<T> (DNA<T> dna) where T : Gene, new()
    {
        return JsonUtility.ToJson(dna, true);
    }

    public static DNA<T> Deserialize<T> (string dnaJson) where T : Gene, new()
    {
        return JsonUtility.FromJson<DNA<T>>(dnaJson);
    }

    public static void ToFile<T>(DNA<T> dna, string path) where T : Gene, new() 
    {
        string dnaString = Serialize<T>(dna);

        File.WriteAllText($"{BASE_PATH}/{path}.json", dnaString );
    }

    public static DNA<T> FromFile<T>(string path) where T : Gene, new()
    {
        string dnaString = File.ReadAllText($"{BASE_PATH}/{path}.json");

        return Deserialize<T>(dnaString);
    }
}
