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
        transform.position = InputTracking.GetLocalPosition(handNode);
		transform.rotation = InputTracking.GetLocalRotation(handNode);

		if (Input.GetButton(trigger))
		{
			fingers.localRotation = Quaternion.Euler(0, -145f, 0);
			thumb.localRotation = Quaternion.Euler(85f, -30f, 0);
		}
		else
		{
			fingers.localRotation = Quaternion.Euler(0, 0, 0);
			thumb.localRotation = Quaternion.Euler(90f, 0, 0);
		}
	}
}
