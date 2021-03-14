using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Linq;

class Generation<G, S> where G : Gene, new() where S : MonoBehaviour, Strategy<G>
{
    public UnityEvent generationFinishedEvent;
    int numberOfCats, numberOfJoints, survivorKeepPercentage, spawnHeight;
    GameObject catPrefab;
    List<Cat> cats;
    List<DNA<G>> dNAs;

    public Generation(GameObject catPrefab, int numberOfCats, int survivorKeepPercentage, int spawnHeight, List<DNA<G>> dNAs = null)
    {
        generationFinishedEvent = new UnityEvent();
        this.catPrefab = catPrefab;
        this.numberOfCats = numberOfCats;
        this.survivorKeepPercentage = survivorKeepPercentage;
        this.spawnHeight = spawnHeight;
        this.cats = new List<Cat>();
        this.dNAs = dNAs;
    }

    public void RunGeneration()
    {
        float xOffset = -0.5f;
        float zOffset = -0.5f;
        int catsPerRow = 10;

        for (int i = 0; i < numberOfCats; i++)
        {
            float spawnX = (xOffset * (i % catsPerRow))  - xOffset * catsPerRow / 2;
            float spawnY = spawnHeight;
            float spawnZ = ((i - i % catsPerRow)/catsPerRow) * zOffset;

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            GameObject catGameObject = GameObject.Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            Cat cat = catGameObject.GetComponent<Cat>();

            if (dNAs == null)
                cat.Init<G, S>();
            else
                cat.Init<G, S>(dNAs[i]);

            cats.Add(cat);
        }
    }

    public List<DNA<G>> GetFittestDNAs()
    {
        List<DNA<G>> fittestCats = cats.OrderBy(c => c.GetFitness()).Take((int)(numberOfCats * survivorKeepPercentage * Mathf.Pow(10, -2))).Select(c => c.GetDNA<G>()).ToList();

        return fittestCats;
    }

    public float GetAverageFitness()
    {
        float averageFitness = cats.OrderBy(c => c.GetFitness()).Take((int)(numberOfCats * survivorKeepPercentage * Mathf.Pow(10, -2))).Select(c => c.GetFitness()).Average();
        float roundedAverageFitness = (float)(System.Math.Truncate((double)averageFitness * 100.0) / 100.0);

        return roundedAverageFitness;
    }

    public void Cleanup()
    {
        foreach (Cat cat in cats)
        {
            GameObject.Destroy(cat.gameObject);
        }
    }

}