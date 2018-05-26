using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verbindung : MonoBehaviour {

	public Transform t;
	public float force = 100;
	Rigidbody r;

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		r.AddForce((t.position - transform.position) * force, ForceMode.Impulse);
	}
}
