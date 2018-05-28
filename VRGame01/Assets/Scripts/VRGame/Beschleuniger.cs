using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beschleuniger : MonoBehaviour
{
	public float force = 250f;

	bool go;

	bool interact;

	public RaycastHit[] munition;
	
	// Update is called once per frame
	void Update ()
	{
		if (go)
		{
			RaycastHit[] hit = Physics.RaycastAll (this.transform.position, this.transform.forward, 1.3f);
			for(int i=0; i < hit.Length; i++)
			{
				if (hit[i].rigidbody != null)
				{
					hit[i].rigidbody.AddForce (this.transform.forward * force, ForceMode.Impulse);
					Explo e = hit[i].transform.GetComponent<Explo> ();
					if (e != null)
					{
						e.Activate ();
					}
				}
			}
		}

		if (interact)
		{
			munition = Physics.RaycastAll (this.transform.position, this.transform.forward, 1.3f);
		}
	}

	public void Go()
	{
		go = !go;
	}

	public void Interaction()
	{
		interact = !interact;
	}
}