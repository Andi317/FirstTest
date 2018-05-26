using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beschleuniger : MonoBehaviour
{
	public float force = 250f;

	bool go;
	
	// Update is called once per frame
	void Update ()
	{
		if (go)
		{
			RaycastHit hit;
			if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, 1.3f))
			{
				Rigidbody rb = hit.rigidbody;
				if (rb != null)
				{
					rb.AddForce (this.transform.forward * force, ForceMode.Impulse);
					Explo e = hit.transform.GetComponent<Explo> ();
					if (e != null)
					{
						e.Activate ();
					}
				}
			}
		}
	}

	public void Go()
	{
		go = !go;
	}
}