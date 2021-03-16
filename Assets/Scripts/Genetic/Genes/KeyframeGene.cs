using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyframeGene : Gene {

    protected int numberOfJoints;
    public List<Vector3> rotations;

    public void Init(int id, Dictionary<string, object> config)
    {
        numberOfJoints = (int)config["numberOfJoints"];
        Randomize();
    }

    public void Randomize() {
        rotations = new List<Vector3>();
        for (int i = 0; i < numberOfJoints; i++)
        {
            rotations.Add(GenerateRandomRotation());
        }
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f));
    }

    public object Clone()
    {
        KeyframeGene clone = new KeyframeGene();
        clone.numberOfJoints = numberOfJoints;
        clone.rotations = new List<Vector3>();
        clone.rotations.AddRange(rotations);

        return clone;
    }
}