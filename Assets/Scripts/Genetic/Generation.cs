using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Linq;

class Generation<G,S> where G : Gene, new() where S : MonoBehaviour, Strategy<G>
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
        float xOffset = 2;

        for (int i = 0; i < numberOfCats; i++)
        {
            Vector3 spawnPosition = new Vector3(0, spawnHeight, 0 + xOffset * i);
            GameObject catGameObject = GameObject.Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            Cat cat = catGameObject.GetComponent<Cat>();

            if (dNAs == null)
                cat.Init<G, S>();
            else 
                cat.Init<G, S>(dNAs[i]);
            
            cats.Add(cat);
        }
    }

    public List<DNA<G>> EvaluateGeneration()
    {
        List<DNA<G>> result = cats.OrderBy(c => c.GetFitness()).Take((int)(cats.Count  * survivorKeepPercentage * 10^-2)).Select(c => c.GetDNA<G>()).ToList();

        foreach(Cat cat in cats) {
            GameObject.Destroy(cat.gameObject);
        }

        return result;
    }

}