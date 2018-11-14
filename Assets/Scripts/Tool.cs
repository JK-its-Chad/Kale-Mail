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

	public virtual void StartInteract() { }

	public virtual void Interact() { }

	public virtual void StopInteract() { }

	public virtual void Pickup() { }

	public virtual void Drop() { }
}
