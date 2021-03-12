using UnityEngine;

public interface Strategy<G> where G : Gene, new() {

    void Init(DNA<G> dNA, ConfigurableJoint[] joints);

}