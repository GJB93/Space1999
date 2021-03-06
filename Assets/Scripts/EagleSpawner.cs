﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour {

    public float gap = 20;
    public float followers = 2;
    public GameObject prefab;
    
	void Awake () {
        //GameObject leaderObject = Instantiate(prefab, transform.position + new Vector3(0, 0, 0), transform.rotation);
        GameObject leaderObject = Instantiate(prefab, transform, false);
        GameObject target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        target.transform.position = leaderObject.transform.position + (leaderObject.transform.forward * 1000);
        leaderObject.AddComponent<Boid>();
        leaderObject.AddComponent<Seek>().targetGameObject = target;
        for(int i = 1; i <= followers; i += 1)
        {
            //GameObject followerLeft = Instantiate(prefab, transform.position + new Vector3((i * -20), 0, (i * 20)), transform.rotation);
            //GameObject followerRight = Instantiate(prefab, transform.position + new Vector3((i * 20), 0, (i * 20)), transform.rotation);
            GameObject followerLeft = Instantiate(prefab, transform, false);
            GameObject followerRight = Instantiate(prefab, transform, false);
            followerLeft.transform.position += new Vector3((i * -gap), 0, (i * gap));
            followerRight.transform.position += new Vector3((i * gap), 0, (i * gap));
            followerLeft.AddComponent<Boid>();
            followerRight.AddComponent<Boid>();
            followerLeft.AddComponent<OffsetPursue>().leader = leaderObject.GetComponent<Boid>();
            followerRight.AddComponent<OffsetPursue>().leader = leaderObject.GetComponent<Boid>();
        }
	}
	
	void Update () {
		
	}
}
