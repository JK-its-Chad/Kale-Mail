using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	[SerializeField]
	private Vector3 offset;

	public Vector3 Offset
	{
		get { return offset; }
	}

	public abstract void StartInteract();

	public abstract void Interact();

	public abstract void StopInteract();
}
