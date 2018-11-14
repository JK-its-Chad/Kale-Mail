using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shears : Tool
{
	[SerializeField]
	private Transform bottomPiece;

	public override void Pickup()
	{
		bottomPiece.localRotation = Quaternion.Euler(0, 0, 25f);
	}

	public override void Drop()
	{
		bottomPiece.localRotation = Quaternion.identity;
	}

	public override void StartInteract()
	{
		bottomPiece.localRotation = Quaternion.identity;
	}

	public override void StopInteract()
	{
		bottomPiece.localRotation = Quaternion.Euler(0, 0, 25f);
	}
}
