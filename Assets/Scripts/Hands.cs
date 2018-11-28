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
	private string triggerAnalog;

	private Vector3 lastPos;
	private Quaternion lastRot;
	private bool triggered = false;

	private GameObject grabbed;
	private Tool tool;
	private Vector3 offsetFloaty;
	private Quaternion offsetSpinny;
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
			triggerAnalog = "RightTriggerAnalog";
		}
		else
		{
			handNode = XRNode.LeftHand;
			trigger = "LeftTrigger";
			grip = "LeftGrip";
			triggerAnalog = "LeftTriggerAnalog";
		}

		laser = GetComponent<LineRenderer>();
	}
	
	void Update ()
    {
        if (!manualControl)
        {
            // Move hand positions
            transform.localPosition = InputTracking.GetLocalPosition(handNode);
            transform.localRotation = InputTracking.GetLocalRotation(handNode) * Quaternion.Euler(60f, 0, 0);
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
					tool.Begin();
				}
				else
				{
					tool.Hold();
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
					tool.Stop();
				}
			}
		}

		// Send tool squeeze
		if (tool != null)
		{
			tool.Squeeze(Input.GetAxisRaw(triggerAnalog));
		}

		// Grip button drops tools
		if (Input.GetButtonDown(grip) && tool != null)
		{
			Drop();
		}
		
		// Grip button also functions as grab laser
		if (Input.GetButton(grip) && grabbed == null)
		{
			laser.enabled = true;
			laser.SetPosition(1, new Vector3(0, 0, 15f));

			RaycastHit hit;

			if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, 1 << 9))
			{
				laser.SetPosition(1, new Vector3(0, 0, hit.distance * 10f));

				if (Input.GetButtonDown(trigger))
				{
					GameObject hitGameObject = hit.transform.root.gameObject;

					Vector3 hitOffset = hitGameObject.transform.position - hit.point;

					Grab(hitGameObject, hitOffset);
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
			grabbed.transform.position = transform.position + transform.rotation * offsetFloaty;
            grabbed.transform.rotation = transform.rotation * offsetRot;

			lastPos = transform.position;
			lastRot = transform.rotation;

			offsetFloaty = Vector3.Lerp(offsetFloaty, offsetPos, 0.1f);
			offsetSpinny = Quaternion.Lerp(offsetSpinny, offsetRot, 0.05f);
		}

		// Send trigger input to fixed update
		if (!triggered)
		{
			triggered = Input.GetButtonDown(trigger);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (triggered)
		{
			Grab(other.transform.root.gameObject);
		}

		triggered = false;
	}

	private void Grab(GameObject toGrab, Vector3? laser = null)
	{
		// Return early if another hand is already grabbing
		if (toGrab.GetComponent<Rigidbody>().isKinematic) { return; }

		// Get gameobject and tool script
		grabbed = toGrab;
		tool = grabbed.GetComponent<Tool>();

		// Set offsets
		offsetFloaty = transform.InverseTransformPoint(grabbed.transform.position) / 10f;
		if (tool != null)
		{
			offsetPos = tool.Offset;
			offsetRot = Quaternion.identity;
			offsetSpinny = grabbed.transform.rotation;
			tool.Pickup();
		}
		else
		{
			offsetPos = grabbed.transform.position - transform.position;
			if (laser != null)
			{
				offsetPos = (Vector3)laser;
			}
			offsetRot = Quaternion.Inverse(transform.rotation) * grabbed.transform.rotation;
			offsetPos = Quaternion.Inverse(transform.rotation) * offsetPos;
			if (laser == null)
			{
				offsetFloaty = offsetPos;
			}
			offsetSpinny = offsetRot;
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

		if (tool != null)
		{
			tool.Drop();
		}

		grabbed = null;
		tool = null;
	}
}
