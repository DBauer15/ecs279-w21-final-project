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
    }

    void Update()
    {
        if (dNA == null || joints == null)
            return;

        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJoint(dNA.genes[i], joints[i]);
        }
    }


    void UpdateJoint(BasicGene gene, ConfigurableJoint joint)
    {
        Quaternion currentRotation = joint.transform.localRotation;
        // assuming low and high x limit is the same
        Quaternion rotationInLimits = Quaternion.Euler(
            gene.rotation.x * joint.highAngularXLimit.limit * Time.deltaTime,
            gene.rotation.y * joint.angularYLimit.limit * Time.deltaTime,
            gene.rotation.z * joint.angularZLimit.limit * Time.deltaTime
        );

        joint.SetTargetRotationLocal(rotationInLimits, currentRotation);

    }

}