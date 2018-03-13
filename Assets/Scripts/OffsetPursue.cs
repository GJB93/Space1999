using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour {

    public Boid leader;
    public Vector3 offset;
    public Vector3 worldTarget;

    public override Vector3 Calculate()
    {
        worldTarget = leader.transform.TransformPoint(offset);
        float dist = Vector3.Distance(leader.transform.position, transform.position);
        float time = dist / boid.maxSpeed;

        Vector3 target = worldTarget + (time * leader.velocity);
        return boid.ArriveForce(target, 10.0f);
    }

    void Start()
    {
        offset = leader.transform.position - transform.position;
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }

}
