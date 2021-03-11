using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNASerializer
{
    public static string Serialize<T> (DNA<T> dna) where T : Gene, new()
    {
        return JsonUtility.ToJson(dna, true);
    }

    public static DNA<T> Deserialize<T> (string dnaJson) where T : Gene, new()
    {
        return JsonUtility.FromJson<DNA<T>>(dnaJson);
    }
}
