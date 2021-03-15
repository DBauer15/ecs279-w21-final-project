using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DNA<G> where G : Gene, new()
{
    public List<G> genes;

    public DNA()
    {
        genes = new List<G>();
    }

    public DNA(Dictionary<string, object> config)
    {
        genes = new List<G>();

        for (int i = 0; i < (int)config["numberOfGenes"]; i++)
        {
            Gene gene = new G();
            gene.Init(config);
            genes.Add(new G());
        }

    }

    // randomly picks between genes of dna a and dna b
    public static DNA<G> operator +(DNA<G> a, DNA<G> b)
    {
        DNA<G> combinedDNA = new DNA<G>();

        for (int i = 0; i < a.genes.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            combinedDNA.genes.Add(rand == 0 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

    // takes first half of dna a, second of dna b
    public static DNA<G> operator *(DNA<G> a, DNA<G> b)
    {
        DNA<G> combinedDNA = new DNA<G>();

        for (int i = 0; i < a.genes.Count; i++)
        {
            combinedDNA.genes.Add(i < a.genes.Count/2 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

    // randomly matches limbs of both dNAs
    public static DNA<G> operator /(DNA<G> a, DNA<G> b)
    {
        DNA<G> combinedDNA = new DNA<G>();

        foreach (var limb in Util.limbsJoints)
        {
            
        }

        for (int i = 0; i < a.genes.Count; i++)
        {



            combinedDNA.genes.Add(i < a.genes.Count/2 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

    public void Mutate(int mutationRate)
    {
        for (int i = 0; i < genes.Count; i++)
        {
            float randomValue = UnityEngine.Random.Range(0, 101);

            if(randomValue < mutationRate)
            {
                genes[i].Randomize();
            }
        }
    }

}