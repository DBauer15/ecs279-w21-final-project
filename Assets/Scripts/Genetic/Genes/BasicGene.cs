using System;
using UnityEngine;

[Serializable]
public class BasicGene : Gene {

    public Vector3 rotation;
    public float speed;

    public BasicGene() {
        Randomize();
    }

    public void Randomize() {
        rotation = GenerateRandomRotation();
        speed = GenerateRandomSpeed();
        speed = 1.0f;
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }

    float GenerateRandomSpeed() {
        return UnityEngine.Random.Range(0f, 1f);
    }
}