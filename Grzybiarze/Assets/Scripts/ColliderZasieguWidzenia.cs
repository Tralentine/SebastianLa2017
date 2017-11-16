using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZasieguWidzenia : MonoBehaviour {


	void OnTriggerEnter(Collider col)
	{
		if (col.transform.tag == "Grzyb")
		{
			//Debug.Log ("wszedłem w zasieg widzenia grzyba");
			transform.parent.GetComponent<Shroomer> ().OnTriggerEnterChild (col);
		}
	}

}
