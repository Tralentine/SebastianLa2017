using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	List<Shroom> lista_grzybow;
	List<Shroomer> lista_grzybiarzy;
	List<House> lista_domow;
	public GameObject shroomPrefab;
	float czas_spawnu_grzyba;
	float cooldown_spawnu_grzyba;



	void Start () {
		czas_spawnu_grzyba = 0;
		cooldown_spawnu_grzyba = 10;
		
	}
	

	void Update () {

		if (czas_spawnu_grzyba + cooldown_spawnu_grzyba < Time.time)
		{
			czas_spawnu_grzyba = Time.time;
			Instantiate (shroomPrefab, wylosuj_Pozycje(), Quaternion.identity);
		}


		
	}

	public Vector3 wylosuj_Pozycje()
	{
		Vector3 pozycja = Camera.main.ViewportToWorldPoint (new Vector3 (Random.value, Random.value, Random.value));
		pozycja.y = transform.position.y;
		return pozycja;
	}
}
