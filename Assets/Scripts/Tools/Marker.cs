using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : Tool
{
	public enum Color { Red, Blue, Green };

	[SerializeField]
	private Transform cap;
	[SerializeField]
	private Color color = Color.Red;

	public override void Pickup()
	{
		cap.localPosition = new Vector3(-0.1875f, 0, 0);
	}

	public override void Drop()
	{
		cap.localPosition = new Vector3(0.1875f, 0, 0);
	}

	public override void Begin()
	{
		// Paint
	}
}
