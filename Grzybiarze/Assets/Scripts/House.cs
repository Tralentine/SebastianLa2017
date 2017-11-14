using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	Vector3 pozycja;
	int liczba_grzybiarzy_now;
	int max_grzybiarzy;

	void Start () {

		liczba_grzybiarzy_now = 0;
		max_grzybiarzy = Random.Range (1, 3);
	}
	

	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log(col.transform.tag);
		if (col.transform.tag == "Grzybiarz" && liczba_grzybiarzy_now<max_grzybiarzy)
		{
			Debug.Log ("Grzybiarz w domu");
			col.transform.parent.gameObject.GetComponent<Shroomer> ().GetSetWDomu = true;
			liczba_grzybiarzy_now++;
		}
	}

	void OnTriggerStay (Collider col)
	{

	}

	void OnTriggerExit (Collider col)
	{

	}
}
