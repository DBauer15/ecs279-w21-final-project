using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    DNA dNA;

    public void Run()
    {
        dNA = GetComponent<DNA>();
    }
}