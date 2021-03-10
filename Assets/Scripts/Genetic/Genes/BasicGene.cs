using UnityEngine;

public class BasicGene : Gene {

    Vector3 rotation;
    float speed;

    public BasicGene() {
        rotation = GenerateRandomRotation();
        speed = GenerateRandomSpeed();
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    float GenerateRandomSpeed() {
        return Random.Range(0f, 1f);
    }
}