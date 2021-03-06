﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour {

    public float weight = 1.0f;

    [HideInInspector]
    public Boid boid;

    public void Awake()
    {
        boid = GetComponent<Boid>();
    }

    public abstract Vector3 Calculate();
}
