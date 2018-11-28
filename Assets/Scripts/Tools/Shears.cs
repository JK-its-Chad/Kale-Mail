using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shears : Tool
{
	[SerializeField]
	private Transform bottomPiece;

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
	}

	public override void Squeeze(float squeeze)
	{
		bottomPiece.localRotation = Quaternion.Euler(0, -90f, 25f * (1f - squeeze));
        //if(Physics.SphereCast)
	}

	/*public override void Interact(float squeeze)
	{
		bottomPiece.localRotation = Quaternion.Euler(0, 0, 25f);
	}*/
}
