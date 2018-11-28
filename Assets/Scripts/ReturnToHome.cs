using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToHome : MonoBehaviour {

    Vector3 spawn;
    [SerializeField] int speed = 1;

    Rigidbody rig;

	void Start ()
    {
        spawn = transform.position;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate((transform.position - spawn).normalized * -speed * Time.deltaTime);
        rig.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
	}
}
