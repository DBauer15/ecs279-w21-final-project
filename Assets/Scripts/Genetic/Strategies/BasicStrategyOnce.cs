using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrategyOnce : MonoBehaviour, Strategy<BasicGene>
{

    public DNA<BasicGene> dNA;
    public ConfigurableJoint[] joints;

    public void Init(DNA<BasicGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;

        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJoints(dNA.genes[i], joints[i]);
        }
    }

    void UpdateJoints(BasicGene gene, ConfigurableJoint joint)
    {
        Quaternion currentRotation = joint.transform.localRotation;
        
        Quaternion totalRotation = Quaternion.Euler(
            // assuming low and high x limit is the same
            gene.rotation.x * 360,
            gene.rotation.y * 360,
            gene.rotation.z * 360
        );

        if(joint.gameObject.name == "CatGirl2:Chest_M")
            Debug.Log("Rotating joint " + joint + " by " + totalRotation.eulerAngles + " total rot " + new Vector3(gene.rotation.x * 360, gene.rotation.y * 360, gene.rotation.z * 360));

        joint.SetTargetRotationLocal(totalRotation, currentRotation);
    }

}