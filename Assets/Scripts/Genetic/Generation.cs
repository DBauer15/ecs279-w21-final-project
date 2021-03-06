using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Linq;

class Generation<G, S> where G : Gene, new() where S : MonoBehaviour, Strategy<G>
{
    public static int INSTACE_COUNT = 0;

    public int id;
    public UnityEvent generationFinishedEvent;
    int numberOfCats, numberOfJoints, survivorCutoffPercentage, spawnHeight;
    GameObject catPrefab;
    Dictionary<string, object> catConfig;
    List<Cat> cats;
    List<DNA<G>> dNAs;

    public Generation(GameObject catPrefab, int numberOfCats, int survivorCutoffPercentage, int spawnHeight, Dictionary<string, object> catConfig, List<DNA<G>> dNAs = null)
    {
        generationFinishedEvent = new UnityEvent();
        this.catPrefab = catPrefab;
        this.numberOfCats = numberOfCats;
        this.survivorCutoffPercentage = survivorCutoffPercentage;
        this.spawnHeight = spawnHeight;
        this.cats = new List<Cat>();
        this.catConfig = catConfig;
        this.dNAs = dNAs;
        this.id = INSTACE_COUNT;
        INSTACE_COUNT += 1;
        Cat.INSTACE_COUNT = 0;
    }

    public void RunGeneration()
    {
        float xOffset = -0.9f;
        float zOffset = -0.9f;
        int catsPerRow = 10;

        for (int i = 0; i < numberOfCats; i++)
        {
            float spawnX = (xOffset * (i % catsPerRow)) - xOffset * catsPerRow / 2 + xOffset / 2;
            float spawnY = spawnHeight;
            float spawnZ = (((i - i % catsPerRow) / catsPerRow) * zOffset) - numberOfCats / catsPerRow * zOffset / 2 + zOffset / 2;

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            GameObject catGameObject = GameObject.Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            Cat cat = catGameObject.GetComponent<Cat>();



            if (dNAs == null)
                cat.Init<G, S>(catConfig);
            else
                cat.Init<G, S>(dNAs[i]);

            cats.Add(cat);
        }
    }

    public List<DNA<G>> GetFittestDNAs()
    {
        List<DNA<G>> fittestCats = cats.OrderByDescending(c => c.GetFitness()).Take((int)(numberOfCats * survivorCutoffPercentage * Mathf.Pow(10, -2))).Select(c => c.GetDNA<G>()).ToList();

        return fittestCats;
    }

    public float GetWorstFitness()
    {
        return cats.OrderBy(c => c.GetFitness()).Select(c => c.GetFitness()).First();
    }

    public float GetAverageFitness()
    {
        //float averageFitness = cats.OrderByDescending(c => c.GetFitness()).Take((int)(numberOfCats * survivorCutoffPercentage * Mathf.Pow(10, -2))).Select(c => c.GetFitness()).Average();
        float averageFitness = cats.Select(c => c.GetFitness()).Average();

        float roundedAverageFitness = (float)(System.Math.Truncate((double)averageFitness * 100.0) / 100.0);

        return roundedAverageFitness;
    }

    public float GetBestFitness()
    {
        return cats.OrderByDescending(c => c.GetFitness()).Select(c => c.GetFitness()).First();
    }

    public float GetMedianFitness()
    {
        List<float> fitnesses = cats.OrderByDescending(c => c.GetFitness()).Select(c => c.GetFitness()).ToList();

        return fitnesses[(int)(fitnesses.Count / 2)];
    }

    public void SerializeBest() {
        Serialize(1);
    }
    public void Serialize(int n = 1)
    {
        foreach (Cat cat in cats.OrderByDescending(c => c.GetFitness()).Take(n))
        {
            DNASerializer.ToFile<G>(cat.GetDNA<G>(), $"gen{id}_cat{cat.id}_fit{cat.GetFitness()}");
        }
    }

    public void Cleanup()
    {
        foreach (Cat cat in cats)
        {
            GameObject.Destroy(cat.gameObject);
        }
    }

}