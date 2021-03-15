using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicGene : Gene {

    public Vector3 rotation;
    public float rotationTime;

    public void Init(Dictionary<string, object> config)
    {
        Randomize();
    }

    public void Randomize() {
        rotation = GenerateRandomRotation();
        rotationTime = GenerateRandomSpeed();
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }

    float GenerateRandomSpeed() {
        return UnityEngine.Random.Range(0.1f, 2f);
    }
}