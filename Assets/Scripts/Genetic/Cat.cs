using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public DNA dNA;
    public Brain brain;

    public void Init(int numberOfJoints)
    {
        dNA = new DNA(numberOfJoints);
        brain = new Brain();
    }

    public void Run()
    {
        
    }

}
