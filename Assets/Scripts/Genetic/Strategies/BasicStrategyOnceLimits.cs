using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrategy : MonoBehaviour, Strategy<BasicGene>
{

    public DNA<BasicGene> dNA;
    public ConfigurableJoint[] joints;

    public void Init(DNA<BasicGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;

        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJointOnceWithinLimits(dNA.genes[i], joints[i]);
        }
    }


    void UpdateJoints(BasicGene gene, ConfigurableJoint joint)
    {
        Quaternion currentRotation = joint.transform.localRotation;
        
        Quaternion totalRotation = Quaternion.Euler(
            // assuming low and high x limit is the same
            gene.rotation.x * joint.highAngularXLimit.limit,
            gene.rotation.y * joint.angularYLimit.limit,
            gene.rotation.z * joint.angularZLimit.limit
        );

        if(joint.gameObject.name == "CatGirl2:Chest_M")
            Debug.Log("Rotating joint " + joint + " by " + totalRotation.eulerAngles + " total rot " + new Vector3(gene.rotation.x * joint.highAngularXLimit.limit, gene.rotation.y * joint.angularYLimit.limit, gene.rotation.z * joint.angularZLimit.limit));

        joint.SetTargetRotationLocal(totalRotation, currentRotation);
    }

}