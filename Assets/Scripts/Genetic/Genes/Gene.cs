using System;
using System.Collections.Generic;
using UnityEngine;

public interface Gene : ICloneable
{
    void Init(int id, Dictionary<string, object> config);
    void Randomize();
}