using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrategy : MonoBehaviour {

    public DNA<BasicGene> dNA;
    public CharacterJoint[] joints;

    

    public void Init(DNA<BasicGene> dNA, CharacterJoint[] joints) {
        this.dNA = dNA;
        this.joints = joints;
    }

    void Update() {
        if (dNA == null || joints == null)
            return;

        for(int i = 0; i < joints.Length; i++) {
            UpdateJoint(dNA.genes[i], joints[i]);
        }
    }


    void UpdateJoint(BasicGene gene, CharacterJoint joint) {
        joint.gameObject.transform.Rotate(60 * gene.rotation * gene.speed * Time.deltaTime, Space.World);
    }

}