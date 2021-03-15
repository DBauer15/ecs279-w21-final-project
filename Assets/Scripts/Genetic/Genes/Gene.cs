using System.Collections.Generic;
using UnityEngine;

public interface Gene {

    void Init(Dictionary<string, object> config);
    void Randomize();
}