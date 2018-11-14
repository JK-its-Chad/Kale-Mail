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

	public virtual void Begin() { }

	public virtual void Hold() { }

	public virtual void Stop() { }

	public virtual void Squeeze(float squeeze) { }

	public virtual void Pickup() { }

	public virtual void Drop() { }
}
