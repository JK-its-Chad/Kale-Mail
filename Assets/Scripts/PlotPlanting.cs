using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotPlanting : MonoBehaviour {

    public GameObject kale;
    bool plotTaken = false;
    GameObject plant;

    private void Update()
    {
        if (plant == null) { return; }
        
        if(plant.transform.position != transform.position && plotTaken == true)
        {
            plotTaken = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12 && plotTaken == false)
        {
            Destroy(other);
            plant = Instantiate(kale, transform.position, Quaternion.identity) as GameObject;
            plotTaken = true;
        }
    }
}
