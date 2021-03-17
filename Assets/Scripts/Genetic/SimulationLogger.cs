using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SimulationLogger : MonoBehaviour
{
    const string fileHeader = "generation,min,max,avg,med";

    public string fileName;

    StreamWriter writer;

    // Start is called before the first frame update
    void Start()
    {
        writer = new StreamWriter($"{Application.dataPath}/logs/{fileName}.csv");


        writer.WriteLine(fileHeader);
    }

    public void Log(int generation, float min, float max, float avg, float med)
    {
        writer.WriteLine($"{generation},{min},{max},{avg},{med}");
    }


    public void OnDestroy()
    {
        writer.Flush();
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
