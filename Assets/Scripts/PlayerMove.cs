﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody rig;
    int speed = 10;
    public float angle = 0;
    public GameObject cam;

    public float score = 100;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime;
        Vector3 cameraRot = cam.transform.rotation.eulerAngles;

        if (Input.GetAxis("RightJoyX") > .1 || Input.GetAxis("RightJoyX") < -.1)
        {
            angle += Input.GetAxis("RightJoyX") * .5f;
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + angle, 0));
        }
        transform.position += Quaternion.Euler(0, cameraRot.y, 0) * MoveDir;
    }
}
