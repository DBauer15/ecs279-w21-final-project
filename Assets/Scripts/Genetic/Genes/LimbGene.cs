using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class LimbGene : Gene {

    public string limbName;
    public int numberOfJoints;
    public List<Vector3> rotations;
    public float rotationTime;

    public void Init(int id, Dictionary<string, object> config)
    {
        limbName = ((string[])config["limbs"]).Distinct().ToList()[id];
        numberOfJoints = ((string[])config["limbs"]).Where(l => l == limbName).ToList().Count();

        rotations = new List<Vector3>();

        Randomize();
    }

    public void Randomize() {
        rotations.Clear();

        if(limbName == "back")
        {
            rotations.Add(new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-10f, 10f)));
            rotations.Add(new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-10f, 10f)));
            rotations.Add(new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-10f, 10f)));
        }
        else
        {
            rotations.Add(new Vector3(0, UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(0, 60f)));
            rotations.Add(new Vector3(0, UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-80, 0)));
            rotations.Add(new Vector3(0, 0, UnityEngine.Random.Range(-30, 0)));
        }

        rotationTime = GenerateRandomSpeed();
    }

    float GenerateRandomSpeed()
    {
        return UnityEngine.Random.Range(0.1f, 1f);
    }

    public object Clone()
    {
        LimbGene clone = new LimbGene();
        clone.limbName = limbName;
        clone.numberOfJoints = numberOfJoints;
        clone.rotationTime = rotationTime;
        clone.rotations = new List<Vector3>();

        foreach(Vector3 rotation in rotations)
        {
            clone.rotations.Add(new Vector3(rotation.x, rotation.y, rotation.z));
        }

        return clone;
    }
}