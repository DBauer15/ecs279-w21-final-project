using System.Collections.Generic;

public static class Util
{

    public static Dictionary<string, string[]> limbsJoints = new Dictionary<string, string[]>(){
        {"legFrontLeft", new string[]{"CatGirl2:frontHip_L", "CatGirl2:frontKnee_L", "CatGirl2:frontAnkle_L"}},
        {"legFrontRight", new string[]{"CatGirl2:frontHip_R", "CatGirl2:frontKnee_R", "CatGirl2:frontAnkle_R"}},
        { "legBackLeft", new string[]{ "CatGirl2:backHip_L", "CatGirl2:backKnee_L", "CatGirl2:backAnkle_L"} },
        { "legBackRight", new string[]{ "CatGirl2:backHip_R", "CatGirl2:backKnee_R", "CatGirl2:backAnkle_R"} },
        { "neck", new string[]{ "CatGirl2:NeckPart1_M"} },
        { "back", new string[]{ "CatGirl2:BackA_M", "CatGirl2:BackB_M", "CatGirl2:Chest_M"} },
        { "tail", new string[]{ "CatGirl2:Tail0_M", "CatGirl2:Tail1_M"} },
    };

}