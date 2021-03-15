using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyframeStrategy : MonoBehaviour, Strategy<KeyframeGene>
{

    public DNA<KeyframeGene> dNA;
    public ConfigurableJoint[] joints;
    public List<Quaternion> initialRotations;

    float initialHeight;

    public void Init(DNA<KeyframeGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;
        this.initialHeight = transform.position.y;
        foreach(ConfigurableJoint joint in joints)
        {
            initialRotations.Add(joint.gameObject.transform.localRotation);
        }
    }

    void Update()
    {
        if (dNA == null || joints == null)
            return;


        for (int i = 0; i < joints.Length; i++)
        {
            UpdateJoint(joints[i], Quaternion.Euler(dNA.genes[0].rotations[i]), initialRotations[i]);
        }
    }


    void UpdateJoint(ConfigurableJoint joint, Quaternion rotation, Quaternion initialRotation)
    {
        //Quaternion currentRotation = joint.transform.localRotation;
        // assuming low and high x limit is the same



        joint.SetTargetRotationLocal(rotation, initialRotation);


    }

}