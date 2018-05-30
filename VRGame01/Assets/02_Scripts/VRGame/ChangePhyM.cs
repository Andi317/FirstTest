using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhyM : MonoBehaviour {

	Collider c;
	public PhysicMaterial pmNormal;
	public PhysicMaterial pmGrab;


	// Use this for initialization
	void Start () {
		c = GetComponent<Collider> ();
	}

	public void Change(bool b)
	{
		if (b) {
			c.material = pmGrab;
		} else {
			c.material = pmNormal;
		}
	}
}
