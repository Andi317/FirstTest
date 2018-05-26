using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schuss : MonoBehaviour {

	public float force = 30;
	NewtonVR.NVRInteractableItem i;
	Rigidbody r;
	Explo e;

	// Use this for initialization
	void Start () {
		i = GetComponent<NewtonVR.NVRInteractableItem> ();
		r = GetComponent<Rigidbody> ();
		e = GetComponent<Explo> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Go()
	{
		r.AddForce (i.AttachedHand.CurrentForward * force, ForceMode.VelocityChange);
		//i.AttachedHands.Clear ();
		i.EndInteraction(i.AttachedHand);
		e.Activate ();
	}
}
