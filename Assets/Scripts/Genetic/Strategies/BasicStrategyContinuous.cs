using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicStrategyContinuous : MonoBehaviour, Strategy<BasicGene>
{

    public DNA<BasicGene> dNA;
    public ConfigurableJoint[] joints;
    float totalTime;
    List<Quaternion> initialRotations;

    public void Init(DNA<BasicGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;

        initialRotations = new List<Quaternion>();
        foreach (ConfigurableJoint joint in joints)
        {
            initialRotations.Add(joint.transform.localRotation);
        }
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        if (dNA == null || joints == null)
            return;

        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJoints(dNA.genes[i], joints[i], initialRotations[i]);
        }
    }

    void UpdateJoints(BasicGene gene, ConfigurableJoint joint, Quaternion initialRotation)
    {
        if(totalTime > gene.rotationTime)
            return;


        float t = totalTime / gene.rotationTime;
        Quaternion target = Quaternion.Euler((gene.rotation * 360.0f) + initialRotation.eulerAngles);

        Quaternion rotation = Quaternion.Lerp(initialRotation, target, t);

        joint.SetTargetRotationLocal(rotation, initialRotation);
    }

}