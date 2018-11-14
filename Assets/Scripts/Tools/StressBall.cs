using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressBall : Tool
{
	public override void Begin()
	{
		transform.localScale = Vector3.one * 0.1f;
	}

	public override void Stop()
	{
		transform.localScale = Vector3.one * 0.2f;
	}
}
