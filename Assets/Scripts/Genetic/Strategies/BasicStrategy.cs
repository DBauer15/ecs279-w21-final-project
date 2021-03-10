using System;
using UnityEngine;

public class BasicStrategy : MonoBehaviour {

    public DNA<BasicGene> dNA;

    void Update() {
        if (dNA == null)
            return;

        print("Running basic strategy");
    }

}