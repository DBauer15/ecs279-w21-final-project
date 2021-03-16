using System.Collections.Generic;
using UnityEngine;

class RandomProcreation<G> : Procreation<G> where G : Gene, new()
{

    public List<DNA<G>> BuildNextGeneration(List<DNA<G>> fittest, int generationSize, int survivorKeepPercentage, int mutationChance, int mutationRate, bool autoProcreation) {
        
        List<DNA<G>> newDNAs = new List<DNA<G>>();

        int survivorKeep = Mathf.RoundToInt(survivorKeepPercentage * Mathf.Pow(10, -2) * generationSize);
        
        // add survivors
        for (int i = 0; i < survivorKeep; i++)
        {
            newDNAs.Add(fittest[i]);
        }

        // procreate and mutate for the rest of population
        while (newDNAs.Count < generationSize)
        {
            // choose random fittest cats
            int randomParentIndex1 = Random.Range(0, fittest.Count);
            int randomParentIndex2 = Random.Range(0, fittest.Count);
            while(randomParentIndex1 == randomParentIndex2 && !autoProcreation)
            {
                randomParentIndex2 = Random.Range(0, fittest.Count);
            }

            DNA<G> dNAParent1 = fittest[randomParentIndex1];
            DNA<G> dNAParent2 = fittest[randomParentIndex2];
            DNA<G> newDNA = dNAParent1 + dNAParent2;

            // mutate only mutationChance percentage of cats
            float randomValue = Random.Range(0, 101);
            if(randomValue < mutationChance)
                newDNA.Mutate(mutationRate);

            newDNAs.Add(newDNA);
        }

        return newDNAs;
    }
}