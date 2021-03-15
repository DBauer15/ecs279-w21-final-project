using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class SimulationManager : MonoBehaviour
{
    [Range(0.5f, 50f)]
    public float timeScale = 1f;

    [SerializeField]
    int numberOfGenerations = 10;
    [SerializeField]
    int catsPerGeneration = 50;
    [SerializeField]
    int survivorKeepPercentage = 5;
    [SerializeField]
    int survivorCutoffPercentage = 30;
    [SerializeField]
    int mutationRate = 10;
    [SerializeField]
    int mutationChance = 10;
    [SerializeField]
    bool autoProcreation = false;
    [SerializeField]
    GameObject catPrefab;
    [SerializeField]
    Text generationText, fitnessText;
    [SerializeField]
    int spawnHeight = 2;
    

    Simulation<BasicGene, BasicStrategyContinuousLimits> simulation;

    void Start()
    {
        simulation = new Simulation<BasicGene, BasicStrategyContinuousLimits>(
            catPrefab,
            catsPerGeneration,
            survivorCutoffPercentage,
            mutationChance,
            mutationRate,
            survivorKeepPercentage,
            autoProcreation,
            spawnHeight
        );

        StartCoroutine("RunSimulation");
    }

    void Update()
    {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;
    }

    IEnumerator RunSimulation() 
    {
        while (simulation.generationCount < numberOfGenerations) {
            // Run Simulation
            simulation.StartGeneration();
            yield return new WaitForSeconds(5f);
            simulation.CompleteGeneration();

            // Update UI
            generationText.text = ("Generation " + simulation.generationCount);
            fitnessText.text = ($"Mean Fit.: {simulation.generationMeanFitness}, Best Fit.: {simulation.generationBestFitness}"); 
        }
    }

}