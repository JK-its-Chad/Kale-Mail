using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBox : MonoBehaviour {

    float angle = 0;
    public GameObject seeds;
    public Transform output;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        angle = Vector3.Angle(gameObject.transform.up, Vector3.up);
        if (angle >= 90)
        {
            Instantiate(seeds, output.position, Quaternion.identity);
        }

    }
}
