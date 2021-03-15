using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSingleCat : MonoBehaviour
{
    Cat cat;

    void Start()
    {
        cat = gameObject.GetComponent<Cat>();
        cat.Init<BasicGene, BasicStrategy>();
    }
}
