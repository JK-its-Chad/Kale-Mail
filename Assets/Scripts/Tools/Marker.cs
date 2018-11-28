using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : Tool
{
	[SerializeField]
	private Transform cap;
	[SerializeField]
	public Color color = Color.red;

	public override void Pickup()
	{
		cap.localPosition = new Vector3(0, 0, -0.225f);
	}

	public override void Drop()
	{
		cap.localPosition = new Vector3(0, 0, 0.15f);
	}

	public override void Begin()
	{
		// Paint
	}
}
