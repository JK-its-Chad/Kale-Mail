﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Tool
{
	public override void StartInteract()
	{
		transform.localScale = Vector3.one * 0.5f;
	}

	public override void Interact()
	{
		// Do nothing
	}

	public override void StopInteract()
	{
		transform.localScale = Vector3.one;
	}
}
