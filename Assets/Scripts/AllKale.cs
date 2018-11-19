using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllKale : MonoBehaviour {

    bool isPlanted = false;
    bool inDirt = false;
    public int stage = 0; //0=seed, 1=babby, 2=meh, 3=decent, 4=BESTGRAB, 5=old, 6=dead
    public float lifeTime = 0;
    public LayerMask soil;

    public int fertilizer = 1;




	void Start ()
    {
		
	}

	
	// Update is called once per frame
	void Update ()
    {
        inDirt = Physics.CheckSphere(transform.position, .125f, soil);

        if (lifeTime == 0 && inDirt)
        {
            isPlanted = true;
        }
        else if(!inDirt)
        {
            isPlanted = false;
        }
        if(isPlanted)
        {
            if (stage < 5)
            {
                lifeTime += Time.deltaTime * fertilizer;
                transform.localScale = new Vector3(lifeTime / 50, lifeTime / 50, lifeTime / 50);
            }
            if (stage >= 5 && lifeTime > 60)
            {
                lifeTime -= Time.deltaTime * fertilizer;
                transform.localScale = new Vector3(lifeTime / 50, lifeTime / 50, lifeTime / 50);
            }
            if (stage == 6)
            {
                //wither
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
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

        if (stage == 5 && lifeTime <= 60)
        {
            stage++;
        }
   
    }
}
