using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZwykly : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.transform.tag == "Grzyb")
		{
			Debug.Log ("wszedlem w grzyba");
			transform.parent.GetComponent<Shroomer>().OnCollisionEnterChild (col);

		}
	}
}
