using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    //  joint rotation limits 
    [SerializeField]
    Vector3 backARotationLimits = new Vector3();
    [SerializeField]
    Vector3 backBRotationLimits = new Vector3();
    [SerializeField]
    Vector3 chestRotationLimits = new Vector3();
    [SerializeField]
    Vector3 neckPart1RotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 frontHipLRotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 frontKneeLRotationLimits = new Vector3();
    [SerializeField]
    Vector3 frontAnkleLRotationLimits = new Vector3();
    [SerializeField]
    Vector3 frontHipRRotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 frontKneeRRotationLimits = new Vector3();
    [SerializeField]
    Vector3 frontAnkleRRotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 backHipLRotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 backKneeLRotationLimits = new Vector3();
    [SerializeField]
    Vector3 backAnkleLRotationLimits = new Vector3();
    [SerializeField]
    Vector3 backHipRRotationLimits = new Vector3();
    
    [SerializeField]
    Vector3 backKneeRRotationLimits = new Vector3();
    [SerializeField]
    Vector3 backAnkleRRotationLimits = new Vector3();
    [SerializeField]
    Vector3 tail0RotationLimits = new Vector3();
    [SerializeField]
    Vector3 tail1RotationLimits = new Vector3();
    



    ConfigurableJoint[] joints;
    Fitness fitness;
    public object dNA;
    public object strategy;


    public void Init<G,S>() where G : Gene, new() where S : MonoBehaviour, Strategy<G>
    {
        this.joints = GetComponentsInChildren<ConfigurableJoint>();
        DNA<G> dNA = new DNA<G>(joints.Length);
        Init<G,S>(dNA);
    }

    public void Init<G,S>(DNA<G> dNA) where G : Gene, new() where S : MonoBehaviour, Strategy<G>
    {
        this.joints = GetComponentsInChildren<ConfigurableJoint>();
        this.dNA = dNA;
        S strategy = gameObject.AddComponent<S>();
        strategy.Init(GetDNA<G>(), joints);
        this.strategy = strategy;

        this.fitness = gameObject.AddComponent<Fitness>();
    }

    public DNA<G> GetDNA<G>() where G : Gene, new() 
    {
        return (DNA<G>)dNA;
    }

    public Strategy<G> GetStrategy<G>() where G : Gene, new() {
        return (Strategy<G>)strategy;
    }

    public float GetFitness() {
        return this.fitness.GetFitness();
    }

}
