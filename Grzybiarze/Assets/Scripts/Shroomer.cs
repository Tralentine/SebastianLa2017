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
	float zasieg_zbierania;
	//public Canvas canvas;
	Vector3 wzgledna_pozycja;
	Text wyswietlacz;
	GameObject model;
	[SerializeField]
	List<Shroom> lista_grzybow;


	/* TODO:
	 * Grzyby rosnąc wyrastają poza kamerę
	 * Grzyby rosną obok siebie?
	 */


	void Start () {
		lista_grzybow = new List<Shroom> ();
		w_domu = false;
		//ide_po_grzyba = false;
		kontroler = GameObject.FindGameObjectWithTag ("Kontroler").GetComponent<GameController>();
		model = transform.Find ("Model").gameObject;
		zasieg_widzenia = Random.Range (10, 20);
		wyswietlacz = transform.Find ("Canvas/Text").GetComponent<Text>();
		licznik_grzybow = 0;
		zasieg_zbierania = 1;
		cel_osiagniety = false;
		cel_chodu = kontroler.wylosuj_Pozycje ();
		predkosc_ruchu = 10f;
		wzgledna_pozycja = Vector3.zero;
		transform.Find("ZasiegWidzenia").GetComponent<SphereCollider> ().radius = zasieg_widzenia;

		//Test
		Debug.Log(cel_chodu);
	}
	

	void Update () {
		if (!w_domu)
		{
			if (transform.position.x >= cel_chodu.x - zasieg_zbierania && transform.position.x <= cel_chodu.x + zasieg_zbierania)
			{
				if (transform.position.z >= cel_chodu.z - zasieg_zbierania && transform.position.z <= cel_chodu.z + zasieg_zbierania)
				{
					//jestem w pobliżu grzyba lub punktu
					cel_osiagniety=true;

				}
			}


			if (cel_osiagniety)
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
		lista_grzybow.Add (other.gameObject.GetComponent<Shroom>());
		cel_chodu = ustawCel ();
		//cel_osiagniety = true;
		//this.punkt_docelowy = other.transform.position;
		//this.zasieg_zbierania = other.transform.localScale.x/2;
		//this.ide_po_grzyba = true;
	}

	public void OnCollisionEnterChild(Collision col)
	{
			//+zatrzymanie się i zbieranie grzyba
		lista_grzybow.Remove(col.collider.gameObject.GetComponent<Shroom>());
		Destroy (col.collider.gameObject);
		cel_osiagniety = true;
		licznik_grzybow++;
		wyswietlacz.text = licznik_grzybow.ToString ();	
		zasieg_zbierania = 0.5f;
	}

	private Vector3 ustawCel()
	{
		Vector3 cel = Vector3.zero;
		if (lista_grzybow.Count != 0)
		{
			if (lista_grzybow [0] != null)
			{
				cel = lista_grzybow [0].transform.position;
				
				cel_osiagniety = false;
			} 
			else
			{
				lista_grzybow.RemoveAt (0);
				//cel = lista_grzybow [0].transform.position;
			}

			
		} else
		{
			cel = kontroler.wylosuj_Pozycje ();
			cel_osiagniety = false;
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

	public bool GetSetWDomu
	{
		get{ return w_domu; }
		set{ w_domu = value;
			if (value == true)
			{
				transform.Find ("Model/Male_1").GetComponent<SkinnedMeshRenderer> ().enabled = false;
			} else
			{
				transform.Find ("Model/Male_1").GetComponent<SkinnedMeshRenderer> ().enabled = true;
			}
		
		}
	}
}
