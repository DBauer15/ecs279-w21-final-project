using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LimbStrategy : MonoBehaviour, Strategy<LimbGene>
{

    public DNA<LimbGene> dNA;
    public ConfigurableJoint[] joints;

    public Dictionary<string, Quaternion> initialRotations;
    public Dictionary<string, List<ConfigurableJoint>> limbLookup;

    float totalTime;

    public void Init(DNA<LimbGene> dNA, ConfigurableJoint[] joints)
    {
        this.dNA = dNA;
        this.joints = joints;

        initialRotations = new Dictionary<string, Quaternion>();
        limbLookup = new Dictionary<string, List<ConfigurableJoint>>();
        foreach (ConfigurableJoint joint in joints)
        {
            initialRotations.Add(joint.gameObject.name, joint.gameObject.transform.localRotation);
        }

        foreach(LimbGene gene in dNA.genes)
        {
            limbLookup.Add(gene.limbName, joints.Where(j => Util.limbsJoints[gene.limbName].Contains(j.gameObject.name)).ToList());
        }
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        if (dNA == null || joints == null)
            return;

        foreach (LimbGene gene in dNA.genes)
        {
            UpdateLimb(limbLookup[gene.limbName], gene);
        }
    }

    void UpdateLimb(List<ConfigurableJoint> joints, LimbGene gene)
    {
        for (int i = 0; i < joints.Count; i++)
        {
            UpdateJoint(gene, joints[i], gene.rotations[i], initialRotations[joints[i].gameObject.name]);
        }
    }


    void UpdateJoint(LimbGene gene, ConfigurableJoint joint, Vector3 targetRotation, Quaternion initialRotation)
    {
        float t = totalTime / gene.rotationTime;
        Quaternion target = Quaternion.Euler(targetRotation + initialRotation.eulerAngles);

        Quaternion t1 = Quaternion.Lerp(initialRotation, target, t);

        joint.SetTargetRotationLocal(t1, initialRotation);


    }

}