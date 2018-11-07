using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(LineRenderer))]
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

    [Header("Debug")]
    [SerializeField]
    private bool manualControl = false;
    [SerializeField]
    private bool manualTrigger = false;

    private LineRenderer laser;

	private XRNode handNode;
	private string trigger;
	private string grip;

	private Vector3 lastPos;
	private Quaternion lastRot;

	private GameObject grabbed;
	private Tool tool;
	private Vector3 offsetPos;
    private Quaternion offsetRot;

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

		laser = GetComponent<LineRenderer>();
	}
	
	void Update ()
    {
        if (!manualControl)
        {
            // Move hand positions
            transform.localPosition = InputTracking.GetLocalPosition(handNode);
            transform.rotation = InputTracking.GetLocalRotation(handNode);// * Quaternion.Euler(10f, 0, 0);
        }
        
		// Triger interacts or drops item
		if (Input.GetButton(trigger) || manualTrigger)
		{
			fingers.localRotation = Quaternion.Euler(0, -145f, 0);
			thumb.localRotation = Quaternion.Euler(85f, -30f, 0);

			if (tool != null)
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

			if (tool == null)
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
		if (Input.GetButtonDown(grip) && tool != null)
		{
			Drop();
		}

		if (Input.GetButton(grip) && grabbed == null)
		{
			laser.enabled = true;

			if (Input.GetButtonDown(trigger))
			{
				RaycastHit hit;

				if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, 1 << 9))
				{
					Grab(hit.transform.root.gameObject);
				}
			}
		}
		else
		{
			laser.enabled = false;
		}

		// Move item positions
		if (grabbed)
		{
			grabbed.transform.position = transform.position + transform.rotation * offsetPos;
            grabbed.transform.rotation = transform.rotation * offsetRot;

			lastPos = transform.position;
			lastRot = transform.rotation;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (Input.GetButtonDown(trigger))
		{
			Grab(other.transform.root.gameObject);
		}
	}

	private void Grab(GameObject toGrab)
	{
		// Get gameobject and tool script
		grabbed = toGrab;
		tool = grabbed.GetComponent<Tool>();

		// Set offsets
		if (tool != null)
		{
			offsetPos = tool.Offset;
		}
		else
		{
            offsetPos = grabbed.transform.position - transform.position;
            offsetRot = Quaternion.Inverse(transform.rotation) * grabbed.transform.rotation;
            offsetPos = Quaternion.Inverse(transform.rotation) * offsetPos;
		}
		
		// Modify rigidbody
		Rigidbody rb = grabbed.GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	private void Drop()
	{
		if (grabbed == null) { return; }

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
