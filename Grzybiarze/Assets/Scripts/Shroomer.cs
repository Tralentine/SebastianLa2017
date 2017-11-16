using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shroomer : MonoBehaviour {

	GameController kontroler;
	Vector3 pozycja;
	int licznik_grzybow;
	float zasieg_widzenia;
	float predkosc_ruchu;
	Vector3 kolor;
	[SerializeField]
	Vector3 cel_chodu;
	[SerializeField]
	bool cel_osiagniety;

	//public bool ide_po_grzyba;
	[SerializeField]
	bool w_domu;
	bool wedrowka;
	[SerializeField]
	bool zmierza_do_domku;
	bool dead;
	float zasieg_zbierania;
	//public Canvas canvas;
	Vector3 wzgledna_pozycja;
	Text wyswietlacz;
	GameObject model;
	[SerializeField]
	List<Shroom> lista_pobliskich_grzybow;
	Renderer render;
	Animator animacja;


	/* TODO:
	 * Grzyby rosnąc wyrastają poza kamerę
	 * Grzyby rosną obok siebie?
	 */


	void Start () {
		animacja = transform.Find ("Model").GetComponent<Animator> ();
		//Debug.Log (animacja.name);
		wedrowka = true;
		dead = false;
		zmierza_do_domku = false;
		lista_pobliskich_grzybow = new List<Shroom> ();
		w_domu = false;
		//ide_po_grzyba = false;
		kontroler = GameObject.FindGameObjectWithTag ("Kontroler").GetComponent<GameController>();
		model = transform.Find ("Model").gameObject;
		zasieg_widzenia = Random.Range (10, 20);
		wyswietlacz = transform.Find ("Canvas/Text").GetComponent<Text>();
		licznik_grzybow = 0;
		zasieg_zbierania = 0.5f;
		cel_osiagniety = false;
		cel_chodu = kontroler.wylosuj_Pozycje (0);
		predkosc_ruchu = 5f;
		wzgledna_pozycja = Vector3.zero;
		transform.Find("ZasiegWidzenia").GetComponent<SphereCollider> ().radius = zasieg_widzenia;
		render = transform.Find ("Model/Male_1").GetComponent<Renderer> ();

		//Test
		//Debug.Log(cel_chodu);
	}
	

	void Update () {
		if (!w_domu && !dead)
		{
			if (transform.position.x >= cel_chodu.x - zasieg_zbierania && transform.position.x <= cel_chodu.x + zasieg_zbierania)
			{
				if (transform.position.z >= cel_chodu.z - zasieg_zbierania && transform.position.z <= cel_chodu.z + zasieg_zbierania)
				{
					//jestem w pobliżu grzyba lub punktu
					cel_osiagniety=true;

				}
			}


			if (cel_osiagniety && zmierza_do_domku)
			{
				cel_chodu = znajdzDom (kontroler.lista_domow);
				cel_osiagniety = false;
				lista_pobliskich_grzybow.RemoveRange (0, lista_pobliskich_grzybow.Count);

			} else if (cel_osiagniety)
			{
				cel_chodu = ustawCel ();
			} else
			{
				idzDoPunktu (cel_chodu);
			}
		}


	}

	public void OnTriggerEnterChild(Collider other)
	{			
		lista_pobliskich_grzybow.Add (other.gameObject.GetComponent<Shroom>());
		cel_chodu = ustawCel ();
		//cel_osiagniety = true;
		//this.punkt_docelowy = other.transform.position;
		//this.zasieg_zbierania = other.transform.localScale.x/2;
		//this.ide_po_grzyba = true;
	}

	public void OnCollisionEnterChild(Collision col)
	{
			//+zatrzymanie się i zbieranie grzyba
		lista_pobliskich_grzybow.Remove(col.collider.gameObject.GetComponent<Shroom>());
		Destroy (col.collider.gameObject);
		cel_osiagniety = true;
		licznik_grzybow++;
		kontroler.nagrodzNajlepszego (this);
		wyswietlacz.text = licznik_grzybow.ToString ();	
		zasieg_zbierania = 0.5f;
	}

	public void zmienKolor(Material mat)
	{
		render.material = mat;
	}

	private Vector3 ustawCel()
	{
		Vector3 cel = Vector3.zero;
		if (lista_pobliskich_grzybow.Count != 0)
		{
			if (lista_pobliskich_grzybow [0] != null)
			{
				cel = lista_pobliskich_grzybow [0].transform.position;
				animacja.SetBool ("PoGrzyba", true);
				predkosc_ruchu = 8f;
				
				cel_osiagniety = false;
			} 
			else
			{
				lista_pobliskich_grzybow.RemoveAt (0);
				//cel = lista_grzybow [0].transform.position;
			}

			
		} else
		{
			cel = kontroler.wylosuj_Pozycje (0);
			cel_osiagniety = false;
			animacja.SetBool ("PoGrzyba", false);
			predkosc_ruchu = 5f;
		}

		return cel;

	}


	private void idzDoPunktu(Vector3 punkt_docelowy)
	{
			patrzNa (punkt_docelowy);
			transform.Translate (wzgledna_pozycja.normalized* Time.deltaTime *predkosc_ruchu);
	}

	private void patrzNa(Vector3 punkt)
	{
		wzgledna_pozycja = new Vector3(punkt.x,transform.position.y,punkt.z) - transform.position;
		Quaternion rotacja = Quaternion.LookRotation(wzgledna_pozycja);
		model.transform.rotation = rotacja;
	}

	public Vector3 znajdzDom(List<House> lista_Domow)
	{
		float dystans = Mathf.Infinity;
		House najblizszy_dom = null;
		List<House> lista_pustych_domow = new List<House> ();
		foreach (House dom in lista_Domow)
		{
			if (dom.GetPozostaleMiejsca > 0)
			{
				lista_pustych_domow.Add (dom);
			}
				
		}

		if (lista_pustych_domow.Count == 0)
		{
			dead = true;
			animacja.SetBool ("Play", false);
			render.material = kontroler.GetDeathMat;
			return Vector3.zero;
		}

		foreach (House dom in lista_pustych_domow)
		{
			if (Vector3.Distance (transform.position, dom.transform.position) < dystans)
			{
				najblizszy_dom = dom;
				dystans = Vector3.Distance (transform.position, dom.transform.position);
			}
		}

		return najblizszy_dom.transform.position;
	}

	public bool GetSetWDomu
	{
		get{ return w_domu; }
		set{ w_domu = value;
			if (value == true)
			{
				wedrowka = false;
				render.enabled = false;
			} else
			{
				render.enabled = true;
			}
		
		}
	}

	public void idzDoDomu(bool tak)
	{
		if (tak)
		{
			zmierza_do_domku = true;
			cel_osiagniety = true;

		} else
		{
			zmierza_do_domku = false;
		}
	}

	public bool GetZmierzaDoDomu
	{
		get{ return zmierza_do_domku; }
	}

	public bool GetSetWedrowka
	{
		get { return wedrowka; }
		set { wedrowka = value; }
	}

	public int GetLicznikGrzybow
	{
		get{ return licznik_grzybow; }		
	}

	public bool GetSetCelOsiagniety
	{
		get{ return cel_osiagniety; }
		set{ cel_osiagniety = value; }
	}

	public bool GetSetDead
	{
		get{ return dead; }
		set { dead = value;
			if (!value)
			{
				animacja.SetBool ("Play", true);
			} 
		}
	}


}
