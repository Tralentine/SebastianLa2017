using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

	private Vector3 pozycja;
	private float tempo_wzrostu;
	private float wielkosc;


	void Start () {
			
	}
	

	void Update () {
		
	}

	public Vector3 GetSetPozycja
	{
		get{return pozycja;}
		set{ pozycja = value; }
	}

	public float GetSetTempoWzrostu
	{
		get{return tempo_wzrostu;}
		set{ tempo_wzrostu = value; }
	}

	public float GetSetWielkosc
	{
		get{return wielkosc;}
		set{ wielkosc = value; }
	}



}
