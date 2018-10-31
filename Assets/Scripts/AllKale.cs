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
        //inDirt = Physics.SphereCast(transform.position, 1f, Vector3.zero, out hit, 1, soil);
        inDirt = true;
        if (lifeTime == 0 && inDirt)
        {
            isPlanted = true;
        }
		if(isPlanted && stage >= 5)
        {
            lifeTime += Time.deltaTime;
            transform.localScale = new Vector3(lifeTime / 50, lifeTime / 50, lifeTime / 50);
        }
        if(stage == 5)
        {
            lifeTime -= Time.deltaTime;
            transform.localScale = new Vector3(lifeTime / 50, lifeTime / 50, lifeTime / 50);
        }
        if(stage == 6)
        {
            //wither
        }

        if (stage == 0 && lifeTime >= 20)
        {

            stage++;
        }
        if (stage == 1 && lifeTime >= 40)
        {
            
            stage++;
        }
        if (stage == 2 && lifeTime >= 60)
        {

            stage++;
        }
        if (stage == 3 && lifeTime >= 80)
        {

            stage++;
        }
        if (stage == 4 && lifeTime >= 100)
        {

            stage++;
        }
        if (stage == 5 && lifeTime >= 120)
        {

            stage++;
        }
        if (stage == 6 && lifeTime >= 140)
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);
            stage = -1;
        }
        
    }
}
