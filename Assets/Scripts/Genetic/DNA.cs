using System.Collections.Generic;
using UnityEngine;

public class DNA<G> where G : Gene, new()
{
    public List<G> genes;

    public DNA()
    {
        genes = new List<G>();
    }

    public DNA(int numberOfJoints)
    {
        genes = new List<G>();

        for (int i = 0; i < numberOfJoints; i++)
        {
            genes.Add(new G());
        }

    }

    public static DNA<G> operator +(DNA<G> a, DNA<G> b)
    {
        DNA<G> combinedDNA = new DNA<G>();

        for (int i = 0; i < a.genes.Count; i++)
        {
            int rand = Random.Range(0, 2);
            combinedDNA.genes.Add(rand == 0 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

}