using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    List<GameObject> joints;
    public DNA<BasicGene> dNA;
    BasicStrategy strategy;


    void OnEnable()
    {
        dNA = new DNA<BasicGene>(joints.Count);
    }

    void Start() {
        strategy = gameObject.AddComponent<BasicStrategy>();
        strategy.dNA = dNA;
    }
}
