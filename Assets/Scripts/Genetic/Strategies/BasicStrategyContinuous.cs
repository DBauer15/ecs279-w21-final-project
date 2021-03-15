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
            gene.rotation.x * 360 * Time.deltaTime / gene.rotationTime,
            gene.rotation.y * 360 * Time.deltaTime / gene.rotationTime,
            gene.rotation.z * 360 * Time.deltaTime / gene.rotationTime
        );

        if(joint.gameObject.name == "CatGirl2:Chest_M")
            Debug.Log("Rotating joint " + joint + " by " + partialRotation.eulerAngles + " total rot " + new Vector3(gene.rotation.x * 360, gene.rotation.y * 360, gene.rotation.z * 360));

        joint.SetTargetRotationLocal(partialRotation, currentRotation);
    }

}