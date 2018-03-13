using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float mass = 1.0f;
    public float maxSpeed = 10.0f;

	// Use this for initialization
	void Start () {
        SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

        foreach(SteeringBehaviour b in behaviours)
        {
            this.behaviours.Add(b);
        }
	}
	
	// Update is called once per frame
	void Update () {
        force = Calculate();

        Vector3 newAcceleration = force / mass;
        acceleration = Vector3.Lerp(acceleration, newAcceleration, 0.1f);

        velocity += acceleration * Time.deltaTime;

        if(velocity.magnitude > float.Epsilon)
        {
            transform.LookAt(velocity);
            velocity *= 0.99f;
        }

        transform.position += velocity * Time.deltaTime;
	}

    public Vector3 Calculate()
    {
        force = Vector3.zero;

        foreach(SteeringBehaviour b in behaviours)
        {
            force += b.Calculate() * b.weight;
        }

        return force;
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    public Vector3 ArriveForce(Vector3 target, float slowingDistance)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        if (dist == 0)
        {
            return Vector3.zero;
        }
        float ramped = maxSpeed * (dist / slowingDistance);
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        return desired - velocity;
    }
}
