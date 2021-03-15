using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlayer : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float timeScale = 1f;

    public string catFile;

    // Start is called before the first frame update
    void Start()
    {
        if (catFile != "") {
            Debug.Log($"Loading cat {catFile}");
            DNA<BasicGene> dna = DNASerializer.FromFile<BasicGene>(catFile);
            GetComponent<Cat>().Init<BasicGene, BasicStrategyContinuous>(dna);
        } else { 
            Debug.Log("Generating random cat");
            GetComponent<Cat>().Init<BasicGene, BasicStrategyContinuous>();
        }
    }

    void Update() {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;
    }
}
