using UnityEngine;

public class BasicGene : Gene {

    public Vector3 rotation;
    public float speed;

    public BasicGene() {
        rotation = GenerateRandomRotation();
        speed = GenerateRandomSpeed();
        speed = 1.0f;
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    float GenerateRandomSpeed() {
        return Random.Range(0f, 1f);
    }
}