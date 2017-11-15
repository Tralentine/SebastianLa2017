using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	Vector3 pozycja;
	[SerializeField]
	int liczba_grzybiarzy_now;
	[SerializeField]
	int max_grzybiarzy;
	GameController kontroler;

	void Start () {
		kontroler = GameObject.FindGameObjectWithTag ("Kontroler").GetComponent<GameController>();
		liczba_grzybiarzy_now = 0;
		max_grzybiarzy = Random.Range (1, 3);
	}
	

	void Update () {

		
		
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log(col.transform.tag);
		if (col.transform.tag == "Grzybiarz" && liczba_grzybiarzy_now<max_grzybiarzy && col.transform.parent.gameObject.GetComponent<Shroomer>().GetZmierzaDoDomu)
		{
		//	Debug.Log ("Grzybiarz w domu");
			col.transform.parent.gameObject.GetComponent<Shroomer> ().GetSetWDomu = true;
			liczba_grzybiarzy_now++;

			if (liczba_grzybiarzy_now == max_grzybiarzy)
			{
				foreach (Shroomer shr in kontroler.lista_grzybiarzy)
				{
					if (!shr.GetSetWDomu)
					{
						shr.GetSetCelOsiagniety = true;
					}
				}
			}
		}
	}

	void OnTriggerStay (Collider col)
	{

	}

	void OnTriggerExit (Collider col)
	{
		if (col.transform.tag == "Grzybiarz" && !col.transform.parent.gameObject.GetComponent<Shroomer>().GetSetWedrowka)
		{
			col.transform.parent.gameObject.GetComponent<Shroomer> ().GetSetWedrowka = true;
			liczba_grzybiarzy_now--;
		}
	}

	public int GetPozostaleMiejsca
	{
		get{ return max_grzybiarzy - liczba_grzybiarzy_now; }
	}
}
