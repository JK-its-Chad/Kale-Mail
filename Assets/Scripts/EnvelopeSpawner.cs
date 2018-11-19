using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeSpawner : MonoBehaviour {

    [SerializeField] GameObject envelope;

	void Start ()
    {
        GameObject spawnME = Instantiate(envelope, transform.position, Quaternion.Euler(90, 0, -90)) as GameObject;
    }
	

	void Update ()
    {
        //if buyButton, spend money, spawn more
	}
}
