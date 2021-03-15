using System;
using System.Collections.Generic;
using UnityEngine;

public class TestStrategy : MonoBehaviour, Strategy<BasicGene>
{

    public DNA<BasicGene> dNA;
    public ConfigurableJoint[] joints;
    float totalTime;
    Quaternion initialRotation;

    public void Init(DNA<BasicGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;
        totalTime = 0f;

        for (int i = 0; i < joints.Length; i++)
        {
            if(joints[i].name == "CatGirl2:Tail0_M")
                //UpdateJoints(dNA.genes[i], joints[i]);
                initialRotation = joints[i].transform.localRotation;
        }

    }

    void Update()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            if(joints[i].name == "CatGirl2:Tail0_M" && totalTime < 10.0f)
                UpdateJoints(dNA.genes[i], joints[i]);
        }
        totalTime += Time.deltaTime;
    }

    void UpdateJoints(BasicGene gene, ConfigurableJoint joint)
    {
        float t = totalTime / 5.0f;
        Quaternion target = Quaternion.Euler(new Vector3(
            0,
            45,
            0
        ) + initialRotation.eulerAngles);

        Quaternion t1 = Quaternion.Lerp(initialRotation, target, t);

        joint.SetTargetRotationLocal(t1, initialRotation);
    }

}