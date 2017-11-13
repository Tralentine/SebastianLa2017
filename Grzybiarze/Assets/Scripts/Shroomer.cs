using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroomer : MonoBehaviour {

	GameController kontroler;
	Vector3 pozycja;
	int licznik_grzybow;
	float zasieg_widzenia;
	float predkosc_ruchu;
	Vector3 kolor;
	Vector3 punkt_docelowy;
	bool cel_osiagniety;
	bool w_domu;
	float zasieg_zbierania;

//Testowe
	public GameObject grzyb;

	void Start () {
		kontroler = GameObject.FindGameObjectWithTag ("Kontroler").GetComponent<GameController>();
		zasieg_zbierania = 1;
		cel_osiagniety = false;
		punkt_docelowy = kontroler.wylosuj_Pozycje ();

		//Test
		Debug.Log(punkt_docelowy);
		Instantiate (grzyb, punkt_docelowy , Quaternion.identity);
		
	}
	

	void Update () {

		if (transform.position.x >= punkt_docelowy.x - zasieg_zbierania && transform.position.x <= punkt_docelowy.x + zasieg_zbierania)
		{
			if (transform.position.y >= punkt_docelowy.y - zasieg_zbierania && transform.position.y <= punkt_docelowy.y + zasieg_zbierania)
			{
				//jestem w zasiegu zbierania grzyba
				cel_osiagniety=true;
			}
		}

		idzDoPunktu (punkt_docelowy);
	}


	private void idzDoPunktu(Vector3 punkt_docelowy)
	{
		if (!cel_osiagniety)
		{
			patrzNa (punkt_docelowy);

			
		}
	}

	private void patrzNa(Vector3 punkt)
	{
		Vector3 wzgledna_pozycja = new Vector3(punkt.x,transform.position.y,punkt.z) - transform.position;
		Quaternion rotacja = Quaternion.LookRotation(wzgledna_pozycja);
		transform.rotation = rotacja;
	}
}
