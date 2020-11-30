﻿using System;
using System.Numerics;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Anglerfish : MonoBehaviour {
    
    public float speed;
    private Transform target;
    public float turnSpeed = 0f;
    public Rigidbody rb;
    public float targetDistance;
    private Vector3 movement;
    private Animator anim;
    public anglerfishState state = anglerfishState.chasing;

    public enum anglerfishState {
        chasing,
        biting
    }
    void Start() {
        //rb = this.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.Rotate(new Vector3(0, 90, 0));
        //rb.isKinematic = true;
    }

    private void Update() {
        
        targetDistance = Vector3.Distance(target.position, transform.position);
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;

        switch (state) {
            case anglerfishState.chasing:
                LookAtTarget();
                print("chasing");
                anim.Play("swimming");
                break;
            case anglerfishState.biting:
                anim.Play("biting");
                print("biting");
                break;
        }
    }

    private void FixedUpdate() {
        if (state == anglerfishState.chasing) {
            MoveToTarget(movement);
        }
    }

    void LookAtTarget() {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.fixedDeltaTime / 10;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void MoveToTarget(Vector3 direction) {
        if (targetDistance > 70f) {
            rb.velocity = direction * speed;
        }
        else if (targetDistance > 50f && state == anglerfishState.chasing) {
            state = anglerfishState.biting;
        }
    }
}
