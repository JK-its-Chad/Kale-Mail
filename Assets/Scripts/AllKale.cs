using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllKale : MonoBehaviour {

    bool isPlanted = false;
    public int stage = 0; //0=seed, 1=babby, 2=meh, 3=decent, 4=BESTGRAB, 5=old, 6=dead
    public float lifeTime = 0;
    public LayerMask soil;
    bool inDirt = false;


	void Start ()
    {
		
	}

	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        inDirt = Physics.SphereCast(transform.position, 1f, Vector3.zero, out hit, 1, soil);
        if (lifeTime == 0 && inDirt)
        {
            isPlanted = true;
        }
		if(isPlanted)
        {
            lifeTime += Time.deltaTime;
        }
        if(stage == 0 && lifeTime >= 100)
        {
            transform.localScale = new Vector3(lifeTime/100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 1 && lifeTime >= 200)
        {
            transform.localScale = new Vector3(lifeTime / 100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 2 && lifeTime >= 300)
        {
            transform.localScale = new Vector3(lifeTime / 100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 3 && lifeTime >= 500)
        {
            transform.localScale = new Vector3(lifeTime / 100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 4 && lifeTime >= 700)
        {
            transform.localScale = new Vector3(lifeTime / 100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 5 && lifeTime >= 900)
        {
            transform.localScale = new Vector3(lifeTime / 100, lifeTime / 100, lifeTime / 100);
            stage++;
        }
        if (stage == 6 && lifeTime >= 1000)
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);
            stage = 0;
            lifeTime = 0;
        }
    }
}
