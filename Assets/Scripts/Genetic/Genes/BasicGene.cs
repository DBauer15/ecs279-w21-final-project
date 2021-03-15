using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicGene : Gene {

    public Vector3 rotation;
    public float speed;

    public void Init(Dictionary<string, object> config)
    {
        Randomize();
    }

    public void Randomize() {
        rotation = GenerateRandomRotation();
        speed = GenerateRandomSpeed();
        speed = 1.0f;
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f));
    }

    float GenerateRandomSpeed() {
        return UnityEngine.Random.Range(0f, 1f);
    }
}