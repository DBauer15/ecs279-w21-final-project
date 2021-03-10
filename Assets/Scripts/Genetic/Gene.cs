using UnityEngine;

public class Gene {
    
    Vector3 rotation;

    public Gene()
    {
        this.rotation = GenerateRandomRotation();
    }

    Vector3 GenerateRandomRotation()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

}