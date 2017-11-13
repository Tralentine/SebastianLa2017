using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	List<Shroom> lista_grzybow;
	List<Shroomer> lista_grzybiarzy;
	List<House> lista_domow;



	void Start () {
		
	}
	

	void Update () {
		
	}

	public Vector3 wylosuj_Pozycje()
	{
		Vector3 pozycja = Camera.main.ViewportToWorldPoint (new Vector3 (Random.value, Random.value, Random.value));
		pozycja.y = transform.position.y;
		return pozycja;
	}
}
