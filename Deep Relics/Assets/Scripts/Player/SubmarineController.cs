﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SubmarineController : MonoBehaviour {

    public Rigidbody rbSm;
    public Camera camera;
    public RelicCounter relicCounter;
    public AudioSource aSourcePropeller;
    
    public int vRotationSpeed = 15;
    public float minRotation = -80f;
    public float maxRotation = 80f;
    private Vector3 rotationCam;

    public int maxSpeed = 15;
    public int forwardAccel = 5;
    public int backwardsAccel = 5;
    public int turnSpeed = 50;
    public int ascendSpeed = 10;
    public float submarinePropellerSound;
    

    void FixedUpdate() {
        SubmarineMovement();
        SubmarineStrafe();
        SubmarineTurn();
        SubmarineAscend();

        CameraYRotation();
        CloseGame();
    }

    void SubmarineMovement() {
        if (Input.GetKey("w")) {
            rbSm.AddForce(rbSm.transform.forward * maxSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        } 
        else if(Input.GetKey("s")){
            rbSm.AddForce(-rbSm.transform.forward * maxSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        }
        else {
            aSourcePropeller.volume = 0.5f;
        }
        //Vector3 localVelocity = transform.InverseTransformDirection(rbSm.velocity);
        //localVelocity.x = 0;
        //rbSm.velocity = transform.TransformDirection(localVelocity);
    }

    void SubmarineTurn() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            //rbSm.AddTorque(Vector3.up * turnSpeed);
            rbSm.transform.rotation = Quaternion.Euler(rbSm.transform.rotation.eulerAngles + new Vector3(0f, turnSpeed * Time.deltaTime, 0f));
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            //rbSm.AddTorque(-Vector3.up * turnSpeed);
            rbSm.transform.rotation = Quaternion.Euler(rbSm.transform.rotation.eulerAngles + new Vector3(0f, -turnSpeed * Time.deltaTime, 0));
        }
        /*else if (Input.GetKeyDown("r")) {
            rbSm.transform.rotation = Quaternion.Euler(rbSm.transform.rotation.eulerAngles + new Vector3(0f, 180, 0));
        }
        else if (Input.GetKeyUp("r")) {
            rbSm.transform.rotation = Quaternion.Euler(rbSm.transform.rotation.eulerAngles + new Vector3(0f, 0, 0));
        }*/
    }

    void SubmarineStrafe() {
        if (Input.GetKey("a")) {
            rbSm.AddForce(-rbSm.transform.right * maxSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        }
        else if (Input.GetKey("d")) {
            rbSm.AddForce(rbSm.transform.right * maxSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        }
    }

    void SubmarineAscend() {
        if (Input.GetKey("q") || Input.GetKey(KeyCode.LeftControl)) {
            rbSm.AddForce(Vector3.down * ascendSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        } 
        else if (Input.GetKey("e") || Input.GetKey(KeyCode.Space)) {
            rbSm.AddForce(Vector3.up * ascendSpeed);
            aSourcePropeller.volume = submarinePropellerSound;
        }
    }

    private void CameraYRotation() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            rotationCam += new Vector3(vRotationSpeed * Time.deltaTime, 0);
            rotationCam.x = Mathf.Clamp(rotationCam.x, minRotation, maxRotation);
            camera.transform.localEulerAngles = rotationCam;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) {
            rotationCam -= new Vector3(vRotationSpeed * Time.deltaTime, 0);
            rotationCam.x = Mathf.Clamp(rotationCam.x, minRotation, maxRotation);
            camera.transform.localEulerAngles = rotationCam;
        }
    }

    private void CloseGame() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
