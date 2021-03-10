using System.Collections.Generic;
using UnityEngine;

public class DNA 
{
    public List<Gene> genes;

    public DNA()
    {
        genes = new List<Gene>();
    }

    public DNA(int numberOfJoints)
    {
        genes = new List<Gene>();

        for (int i = 0; i < numberOfJoints; i++)
        {
            genes.Add(new Gene());
        }

    }

    public static DNA operator +(DNA a, DNA b)
    {
        DNA combinedDNA = new DNA();

        for (int i = 0; i < a.genes.Count; i++)
        {
            int rand = Random.Range(0, 2);
            combinedDNA.genes.Add(rand == 0 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

}