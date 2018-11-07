using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody rig;
    int speed = 10;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rig.velocity = Vector3.zero;
        if (Input.GetAxis("Horizontal") > .05 || Input.GetAxis("Horizontal") < -.05)
        {
            rig.AddForce(transform.right * speed * Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") > .05 || Input.GetAxis("Vertical") < -.05)
        {
            rig.AddForce(transform.forward * speed * Input.GetAxis("Vertical"));
        }
    }
}
