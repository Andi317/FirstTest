using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explo : MonoBehaviour
{
	public float radius = 2.5f;
	public float power = 5f;

	float t;
	bool boom;
	bool active;

	// Use this for initialization
	void Start ()
	{
		t = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (boom)
		{
			Vector3 v = transform.position - transform.GetComponent<Rigidbody> ().velocity.normalized * 0.5f;

			Collider[] colls = Physics.OverlapSphere (transform.position, radius * 0.5f);

			foreach (Collider col in colls)
			{
				Rigidbody rb = col.GetComponent<Rigidbody> ();
				if (rb != null)
				{
					rb.AddExplosionForce (power, v, radius);
				}
			}

			gameObject.SetActive (false);
		}
	}


	void OnCollisionBegin(Collision collision)
	{
		if (active && Time.time > t + 0.07f)
		{
			boom = true;
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (active && Time.time > t + 0.07f)
		{
			boom = true;
		}
	}

	public void Activate()
	{
		t = Time.time;
		active = true;
	}
}