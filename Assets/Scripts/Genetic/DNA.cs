using System.Collections.Generic;
using UnityEngine;

public class DNA<T> where T : Gene, new()
{
    public List<T> genes;

    public DNA()
    {
        genes = new List<T>();
    }

    public DNA(int numberOfJoints)
    {
        genes = new List<T>();

        for (int i = 0; i < numberOfJoints; i++)
        {
            genes.Add(new T());
        }

    }

    public static DNA<T> operator +(DNA<T> a, DNA<T> b)
    {
        DNA<T> combinedDNA = new DNA<T>();

        for (int i = 0; i < a.genes.Count; i++)
        {
            int rand = Random.Range(0, 2);
            combinedDNA.genes.Add(rand == 0 ? a.genes[i] : b.genes[i]);
        }

        return combinedDNA;
    }

}