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
	public Vector3 punkt_docelowy;
	public bool cel_osiagniety;
	public bool ide_po_grzyba;
	bool w_domu;
	float zasieg_zbierania;
	//public Canvas canvas;
	Vector3 wzgledna_pozycja;
	Text wyswietlacz;
	GameObject model;


	/* TODO:
	 * Zdarzyło się, że grzybiarz doszedł do punktu bez grzyba i zdobył punkt
	 * Czasami grzyb nie znika, a punkt się nalicza
	 * Grzyby rosnąc wyrastają poza kamerę
	 * Grzyby rosną obok siebie?
	 * Prawdopodobnie grzybiarz może omijać grzyby jeśli podczas podchodzenia do jendego zauważy innego, a ten poprzedni pozostanie w zasiegu widzenia (nie wywola powtornie TriggerEnter)
	 */


	void Start () {
		ide_po_grzyba = false;
		kontroler = GameObject.FindGameObjectWithTag ("Kontroler").GetComponent<GameController>();
		model = GameObject.Find ("Model");
		zasieg_widzenia = Random.Range (10, 20);
		wyswietlacz = transform.Find ("Canvas/Text").GetComponent<Text>();
		licznik_grzybow = 0;
		zasieg_zbierania = 1;
		cel_osiagniety = false;
		punkt_docelowy = kontroler.wylosuj_Pozycje ();
		predkosc_ruchu = 5f;
		wzgledna_pozycja = Vector3.zero;
		transform.Find("ZasiegWidzenia").GetComponent<SphereCollider> ().radius = zasieg_widzenia;

		//Test
		Debug.Log(punkt_docelowy);
	}
	

	void Update () {

		if (transform.position.x >= punkt_docelowy.x - zasieg_zbierania && transform.position.x <= punkt_docelowy.x + zasieg_zbierania)
		{
			if (transform.position.y >= punkt_docelowy.y - zasieg_zbierania && transform.position.y <= punkt_docelowy.y + zasieg_zbierania)
			{
				//jestem w pobliżu grzyba lub punktu
				cel_osiagniety=true;

			}
		}

		idzDoPunktu (punkt_docelowy);
	}

	public void OnTriggerEnterChild(Collider other)
	{			
			this.punkt_docelowy = other.transform.position;
			this.zasieg_zbierania = other.transform.localScale.x/2;
			this.ide_po_grzyba = true;
	}

	public void OnCollisionEnterChild(Collision col)
	{
			//+zatrzymanie się i zbieranie grzyba
			Destroy (col.collider.gameObject);
			cel_osiagniety = true;
	}


	private void idzDoPunktu(Vector3 punkt_docelowy)
	{
		if (!cel_osiagniety)
		{
			patrzNa (punkt_docelowy);
			transform.Translate (wzgledna_pozycja.normalized* Time.deltaTime *predkosc_ruchu);

		} else
		{
			if (ide_po_grzyba)
			{
				ide_po_grzyba = false;
				licznik_grzybow++;
				wyswietlacz.text = licznik_grzybow.ToString ();	
				zasieg_zbierania = 1f;
			} 

			Debug.Log ("zaraz bede losowal nowa pozycje");
			this.punkt_docelowy = kontroler.wylosuj_Pozycje ();
			cel_osiagniety = false;

		}
	}

	private void patrzNa(Vector3 punkt)
	{
		wzgledna_pozycja = new Vector3(punkt.x,transform.position.y,punkt.z) - transform.position;
		Quaternion rotacja = Quaternion.LookRotation(wzgledna_pozycja);
		model.transform.rotation = rotacja;
	}
}
