using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shears : Tool
{
	[SerializeField]
	private Transform bottomPiece;

    [SerializeField] LayerMask KALE;


    public override void Pickup()
	{
		bottomPiece.localRotation = Quaternion.Euler(0, -90f, 25f);
	}

	public override void Drop()
	{
		bottomPiece.localRotation = Quaternion.Euler(0, -90f, 0);
	}

	public override void Begin()
    {
        // Cut kale
        Collider[] kale = Physics.OverlapSphere(transform.position, .3f, KALE);
        foreach (Collider ak in kale)
        {
            if(ak.gameObject.GetComponent<AllKale>())
            {
                ak.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                ak.gameObject.GetComponent<AllKale>().isPlanted = false;
                ak.gameObject.layer = 9;
            }
        }
    }

	public override void Squeeze(float squeeze)
	{
		bottomPiece.localRotation = Quaternion.Euler(0, -90f, 25f * (1f - squeeze));
	}

    /*public override void Interact(float squeeze)
	{
		bottomPiece.localRotation = Quaternion.Euler(0, 0, 25f);
	}*/
}
