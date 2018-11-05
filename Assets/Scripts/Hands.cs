using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Hands : MonoBehaviour
{
	public enum Chirality { Left, Right };

	[SerializeField]
	private Chirality hand;

	[Header("Pivots")]
	[SerializeField]
	private Transform fingers;
	[SerializeField]
	private Transform thumb;

	private XRNode handNode;
	private string trigger;
	private string grip;

	private Vector3 lastPos;
	private Quaternion lastRot;

	private GameObject grabbed;
	private Tool tool;
	private Vector3 offset;

	void Start ()
    {
		//_hand = hand == Chirality.Left ? XRNode.LeftHand : XRNode.RightHand;
		if (hand == Chirality.Right)
		{
			handNode = XRNode.RightHand;
			trigger = "RightTrigger";
			grip = "RightGrip";
		}
		else
		{
			handNode = XRNode.LeftHand;
			trigger = "LeftTrigger";
			grip = "LeftGrip";
		}
	}
	
	void Update ()
    {
		// Move hand positions
        transform.position = InputTracking.GetLocalPosition(handNode);
		transform.rotation = InputTracking.GetLocalRotation(handNode);

		// Triger interacts or drops item
		if (Input.GetButton(trigger))
		{
			fingers.localRotation = Quaternion.Euler(0, -145f, 0);
			thumb.localRotation = Quaternion.Euler(85f, -30f, 0);

			if (tool)
			{
				if (Input.GetButtonDown(trigger))
				{
					tool.StartInteract();
				}
				else
				{
					tool.Interact();
				}
			}
		}
		else
		{
			fingers.localRotation = Quaternion.Euler(0, 0, 0);
			thumb.localRotation = Quaternion.Euler(90f, 0, 0);

			if (!tool)
			{
				Drop();
			}
			else
			{
				if (Input.GetButtonUp(trigger))
				{
					tool.StopInteract();
				}
			}
		}

		// Grip button drops tools
		if (Input.GetButtonDown(grip) && tool)
		{
			Drop();
		}

		// Move item positions
		if (grabbed)
		{
			grabbed.transform.position = transform.position + transform.rotation * offset;
			grabbed.transform.rotation = transform.rotation;

			lastPos = transform.position;
			lastRot = transform.rotation;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (Input.GetButtonDown(trigger))
		{
			// Get gameobject
			grabbed = other.transform.root.gameObject;

			// Get tool offset, else use default
			Tool tool = grabbed.GetComponent<Tool>();
			Vector3 _offset = tool != null ? tool.Offset : grabbed.transform.position;

			// Set offset
			offset = _offset - transform.position;

			// Modify rigidbody
			Rigidbody rb = grabbed.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.isKinematic = true;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
	}

	private void Drop()
	{
		// Fix rigidbody
		Rigidbody rb = grabbed.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.velocity = (transform.position - lastPos) / Time.deltaTime;
		rb.angularVelocity = (transform.rotation.eulerAngles - lastRot.eulerAngles); // Time.deltaTime;

		grabbed = null;
		tool = null;
	}
}
