using UnityEngine;
using System.Collections.Generic;

public class SimulationConfig : MonoBehaviour
{
    [SerializeField]
    int numberOfJoints;
    [SerializeField]
    int numberOfGenes;

    public Dictionary<string, object> config;
   

    // Use this for initialization
    void Start()
    {
        config = new Dictionary<string, object>()
        {
            { "numberOfJoints", numberOfJoints },
            { "numberOfGenes", numberOfGenes }
        };
    }
}
