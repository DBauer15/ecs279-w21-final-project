using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyframeStrategy : MonoBehaviour, Strategy<KeyframeGene>
{

    public DNA<KeyframeGene> dNA;
    public ConfigurableJoint[] joints;
    public List<Quaternion> initialRotations;
    public List<float> initialHeights;


    public void Init(DNA<KeyframeGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;

        initialRotations = new List<Quaternion>();
        initialHeights = new List<float>();
        foreach (ConfigurableJoint joint in joints)
        {
            initialRotations.Add(joint.gameObject.transform.localRotation);
            initialHeights.Add(joint.transform.position.y);
        }
    }

    void Update()
    {
        if (dNA == null || joints == null)
            return;


        for (int i = 0; i < joints.Length; i++)
        {
            // Select a gene based on height
            int phase = (int)Mathf.Lerp(-0.5f, dNA.genes.Count - 0.5f, (1 - (joints[i].transform.position.y / initialHeights[i])));
            KeyframeGene gene = dNA.genes[phase];

            UpdateJoint(joints[i], Quaternion.Euler(gene.rotations[i]), initialRotations[i], phase, i);
        }
    }


    void UpdateJoint(ConfigurableJoint joint, Quaternion targetRotation, Quaternion initialRotation, int phase, int index)
    {
        float t = Mathf.Lerp(0, dNA.genes.Count, (1 - (joint.transform.position.y / initialHeights[index]))) - phase;

        //if (joint.gameObject.name == "CatGirl2:Tail0_M")
        //    Debug.Log($"{index}: {Mathf.Lerp(0, dNA.genes.Count, (1 - (joint.transform.position.y / initialHeight)))}, {t}");

        Quaternion target = Quaternion.Euler(targetRotation.eulerAngles + initialRotation.eulerAngles);

        Quaternion rotation = Quaternion.Lerp(initialRotation, target, t);

        joint.SetTargetRotationLocal(rotation, initialRotation);


    }

}