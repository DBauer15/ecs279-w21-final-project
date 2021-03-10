using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    CharacterJoint[] joints;
    public DNA<BasicGene> dNA;
    BasicStrategy strategy;


    void Start() {
        joints = GetComponentsInChildren<CharacterJoint>();
        dNA = new DNA<BasicGene>(joints.Length);
        strategy = gameObject.AddComponent<BasicStrategy>();
        strategy.Init(dNA, joints);
    }
}
