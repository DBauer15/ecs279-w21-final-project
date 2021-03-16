using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class LimbProcreation<G> : Procreation<G> where G : BasicGene, new()
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
            DNA<G> newDNA = new DNA<G>();

            foreach (string limbName in Util.limbsJoints.Keys)
            {
                int randomValueLimb = Random.Range(0, 2);
                
                if(randomValueLimb == 1)
                {
                    newDNA.genes.AddRange(dNAParent1.genes.Where(g => g.limbName == limbName).Select(g => (G)g.Clone()));
                }
                else
                {
                    newDNA.genes.AddRange(dNAParent2.genes.Where(g => g.limbName == limbName).Select(g => (G)g.Clone()));
                }
            }

            // mutate only mutationChance percentage of cats
            float randomValueMutation = Random.Range(0, 101);
            if(randomValueMutation < mutationChance)
                newDNA.Mutate(mutationRate);

            newDNAs.Add(newDNA);
        }

        return newDNAs;
    }
}