using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandScripts : MonoBehaviour
{
	public enum Chirality { Left, Right };

	[SerializeField]
	private Chirality hand;

	private XRNode _hand;

	void Start ()
    {
		_hand = hand == Chirality.Left ? XRNode.LeftHand : XRNode.RightHand;
	}
	
	void Update ()
    {
        transform.position = InputTracking.GetLocalPosition(_hand);
		transform.rotation = InputTracking.GetLocalRotation(_hand);
	}
}
