using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicGene : Gene {
    public string jointName;
    public string limbName;
    public Vector3 rotation;
    public float rotationTime;

    public void Init(int id, Dictionary<string, object> config)
    {
        jointName = ((string[])config["joints"])[id];
        limbName = ((string[])config["limbs"])[id];
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

    public object Clone()
    {
        BasicGene clone = new BasicGene();
        clone.jointName = jointName;
        clone.limbName = limbName;
        clone.rotation = new Vector3(rotation.x, rotation.y, rotation.z);
        clone.rotationTime = rotationTime;

        return clone;
    }
}