using UnityEngine;
using System.Collections.Generic;

public class SimulationConfig : MonoBehaviour
{
    [SerializeField]
    int numberOfJoints;
    [SerializeField]
    int numberOfGenes;

    public Dictionary<string, object> config;
   

    // Use this for initialization
    void Awake()
    {
        config = new Dictionary<string, object>()
        {
            { "numberOfJoints", numberOfJoints },
            { "numberOfGenes", numberOfGenes },
            { "joints", new string[]{
                "CatGirl2:frontHip_L",
                "CatGirl2:frontKnee_L",
                "CatGirl2:frontAnkle_L",
                "CatGirl2:frontHip_R", 
                "CatGirl2:frontKnee_R", 
                "CatGirl2:frontAnkle_R",
                "CatGirl2:backHip_L", 
                "CatGirl2:backKnee_L", 
                "CatGirl2:backAnkle_L",
                "CatGirl2:backHip_R", 
                "CatGirl2:backKnee_R", 
                "CatGirl2:backAnkle_R",
                "CatGirl2:NeckPart1_M",
                "CatGirl2:BackA_M", 
                "CatGirl2:BackB_M", 
                "CatGirl2:Chest_M",
                "CatGirl2:Tail0_M", 
                "CatGirl2:Tail1_M"
            }},
            {"limbs", new string[]{
                "legFrontLeft",
                "legFrontLeft",
                "legFrontLeft",
                "legFrontRight", 
                "legFrontRight", 
                "legFrontRight",
                "legBackLeft", 
                "legBackLeft", 
                "legBackLeft",
                "legBackRight", 
                "legBackRight", 
                "legBackRight",
                "neck",
                "back", 
                "back", 
                "back",
                "tail", 
                "tail"
            }}
        };
    }
}
