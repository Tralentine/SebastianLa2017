using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public List<Shroom> lista_grzybow {  get; set; }
	public List<Shroomer> lista_grzybiarzy {  get; set; }
	public List<House> lista_domow {  get; set; } 
	public GameObject shroomPrefab;
	public GameObject shroomerPrefab;
	public GameObject housePrefab;
	Shroomer najlepszy_grzybiarz;
	
	float czas_spawnu_grzyba;
	float cooldown_spawnu_grzyba;

	int liczba_domow_startowych;
	int liczba_grzybow_startowych;
	int liczba_grzybiarzy_startowych;

	public Material dead;
	public Material lead;
	public Material def;




	void Start () {
		
		liczba_domow_startowych = 1;
		liczba_grzybiarzy_startowych = 3;
		liczba_grzybow_startowych = 0;
		czas_spawnu_grzyba = 0;
		cooldown_spawnu_grzyba = 1;
		lista_domow = new List<House> ();
		lista_grzybow = new List<Shroom> ();
		lista_grzybiarzy = new List<Shroomer> ();

		for (int i = 0; i < liczba_domow_startowych; i++)
		{
			spawn_Domku ();
		}

		for (int i = 0; i < liczba_grzybiarzy_startowych; i++)
		{
			spawn_Grzybiarza ();
		}

		for (int i = 0; i < liczba_grzybow_startowych; i++)
		{
			spawn_Grzyba ();
		}
		najlepszy_grzybiarz = lista_grzybiarzy [0];


	}
	

	void Update () {

		if (czas_spawnu_grzyba + cooldown_spawnu_grzyba < Time.time)
		{
			czas_spawnu_grzyba = Time.time;
			spawn_Grzyba ();
			//Instantiate (shroomPrefab, wylosuj_Pozycje(1), Quaternion.identity);
		}

		switch (Input.inputString)
		{
		case "1":
			spawn_Grzyba ();
			break;
		case "2":
			spawn_Grzybiarza ();
			break;
		case "3":
			spawn_Domku ();
			break;
		case "4":
			do_Domu();
			break;
		case "5":
			na_Zewnatrz();
			break;
		case "6":
			wskrzes();
			break;

		}



	}

	public Vector3 wylosuj_Pozycje(int y)
	{
		Vector3 pozycja = Camera.main.ViewportToWorldPoint (new Vector3 (Random.value, Random.value, Random.value));
		pozycja.y = y;
		return pozycja;
	}

	public void spawn_Domku()
	{
		House domek = Instantiate (housePrefab, wylosuj_Pozycje (0), Quaternion.identity).GetComponent<House>();
		lista_domow.Add (domek);
	}

	public void spawn_Grzyba()
	{
		Shroom grzyb = Instantiate (shroomPrefab, wylosuj_Pozycje (1), Quaternion.identity).GetComponent<Shroom>();
		lista_grzybow.Add (grzyb);
	}

	public void spawn_Grzybiarza()
	{
		Shroomer grzybiarz = Instantiate (shroomerPrefab, wylosuj_Pozycje (0), Quaternion.identity).GetComponent<Shroomer>();
		lista_grzybiarzy.Add (grzybiarz);
	}

	public void do_Domu()
	{
		foreach (Shroomer shrmr in lista_grzybiarzy)
		{
			shrmr.idzDoDomu (true);
		}


	}

	public void na_Zewnatrz()
	{
		foreach (Shroomer shrmr in lista_grzybiarzy)
		{
			shrmr.GetSetWDomu = false;
			shrmr.idzDoDomu (false);
		}
	}

	public void wskrzes()
	{
		foreach (Shroomer shr in lista_grzybiarzy)
		{
			if (shr.GetSetDead)
			{
				shr.GetSetDead = false;
				shr.GetSetCelOsiagniety = true;
				shr.zmienKolor (def);
				nagrodzNajlepszego (shr);
			}
		}
	}

	public void nagrodzNajlepszego(Shroomer shr)
	{
		if (shr.GetLicznikGrzybow > najlepszy_grzybiarz.GetLicznikGrzybow || najlepszy_grzybiarz == shr)
		{
//			Debug.Log ("Zmieniam kolory");
			if(!najlepszy_grzybiarz.GetSetDead)
				najlepszy_grzybiarz.zmienKolor (def);
			najlepszy_grzybiarz = shr;
			shr.zmienKolor (lead);

		}
	}

	public Material GetDeathMat
	{
		get{ return dead; }
	}
}
