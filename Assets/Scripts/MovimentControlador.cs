﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentControlador : MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
