using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrategy : MonoBehaviour, Strategy<BasicGene>
{

    public DNA<BasicGene> dNA;
    public ConfigurableJoint[] joints;
    float totalTime;

    public void Init(DNA<BasicGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;
    }

    void FixedUpdate()
    {
        totalTime += Time.deltaTime;

        if (dNA == null || joints == null)
            return;

        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJoints(dNA.genes[i], joints[i]);
        }
    }

    void UpdateJoints(BasicGene gene, ConfigurableJoint joint)
    {
        if(totalTime > gene.rotationTime)
            return;

        Quaternion currentRotation = joint.transform.localRotation;
        
        Quaternion partialRotation = Quaternion.Euler(
            // assuming low and high x limit is the same
            gene.rotation.x * joint.highAngularXLimit.limit * Time.deltaTime / gene.rotationTime,
            gene.rotation.y * joint.angularYLimit.limit * Time.deltaTime / gene.rotationTime,
            gene.rotation.z * joint.angularZLimit.limit * Time.deltaTime / gene.rotationTime
        );

        if(joint.gameObject.name == "CatGirl2:Chest_M")
            Debug.Log("Rotating joint " + joint + " by " + partialRotation.eulerAngles + " total rot " + new Vector3(gene.rotation.x * joint.highAngularXLimit.limit, gene.rotation.y * joint.angularYLimit.limit, gene.rotation.z * joint.angularZLimit.limit));

        joint.SetTargetRotationLocal(partialRotation, currentRotation);
    }

}