using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalePress : MonoBehaviour {

    public GameObject kaleChips;
    public GameObject pressTop;
    public GameObject lever;
    public float angle = 0;


    int timer = 0;
    bool pressed = false;

    Vector3 lockedPosition;
    

	void Start ()
    {
        lockedPosition = lever.transform.position;
	}
	
	void Update ()
    {
        lever.transform.position = lockedPosition;
        Vector3 holder = pressTop.transform.localPosition;
        angle = Quaternion.Angle(lever.transform.rotation, Quaternion.Euler(Vector3.zero));
        if (angle >= 70)
        {
            angle = 70;
            holder.y = .40f;
        }
        if (angle <= .3f)
        {
            angle = .3f;
            holder.y = .00f;
        }
        holder.y = angle / 175;
        holder.y += .05f;
        pressTop.transform.localPosition = holder;
        lever.transform.rotation = Quaternion.Euler(angle, 0, 0);

        if (holder.y <= .1f)
        {
            pressed = true;
            if (timer >= 2)
            {
                pressed = false;
            }
            timer++;
        }
        if(holder.y >= .15f)
        {
            timer = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
  
        if(other.GetComponent<AllKale>())
        {
            if(pressed)
            {
                Destroy(other.gameObject);
                GameObject chip = Instantiate(kaleChips, new Vector3(.9f, .815f, .4f), Quaternion.identity) as GameObject;
                chip.GetComponent<KaleChipScript>().value = other.GetComponent<AllKale>().lifeTime;
            }
        }
    }
}
