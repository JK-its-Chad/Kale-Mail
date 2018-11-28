using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalePress : MonoBehaviour
{
	[SerializeField]
    private GameObject kaleChips;
	[SerializeField]
	public Transform press;
	[SerializeField]
	public Transform lever;
    
    private int timer = 0;
	private bool pressed = false;
	
	void Update ()
    {
		float angle = Quaternion.Angle(lever.rotation, Quaternion.Euler(225f, 0, 0)) / 90f;

		press.localPosition = new Vector3(0.05f, 0.45f - angle * 0.4f, 0);

		if (press.localPosition.y <= 0.1f)
		{
			pressed = true;
			if (timer >= 2)
			{
				pressed = false;
			}
			++timer;
		}
		else
		{
			timer = 0;
		}
	}

    private void OnTriggerStay(Collider other)
    {
  
        if (other.GetComponent<AllKale>())
        {
            if (pressed)
            {
                Destroy(other.gameObject);
                GameObject chip = Instantiate(kaleChips, transform.position + new Vector3(0, .1f, .4f), Quaternion.identity) as GameObject;
                chip.GetComponent<KaleChipScript>().value = other.GetComponent<AllKale>().lifeTime;
            }
        }
    }
}
