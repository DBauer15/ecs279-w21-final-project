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
    



    CharacterJoint[] joints;
    public DNA<BasicGene> dNA;
    BasicStrategy strategy;


    void Start() {
        // joints = GetComponentsInChildren<CharacterJoint>();
        CharacterJoint cj = GameObject.FindGameObjectWithTag("Test Joint").GetComponent<CharacterJoint>();

        Vector3 rot = new Vector3(0,0,30);
        float spd = 2;


        cj.gameObject.transform.Rotate(60 * rot * spd * Time.deltaTime, Space.World);
        


        // dNA = new DNA<BasicGene>(joints.Length);
        // strategy = gameObject.AddComponent<BasicStrategy>();
        // strategy.Init(dNA, joints);
    }
}
