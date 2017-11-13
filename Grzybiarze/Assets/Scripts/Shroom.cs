using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

	private float tempo_wzrostu;
	float czas_powstania;
	int czas_odPowstania;


	void Start () {

		tempo_wzrostu = 0.05f;
		czas_odPowstania = 1;
		czas_powstania = Time.time;
			
	}
	

	void Update () {

		if (czas_powstania + czas_odPowstania < Time.time)
		{
			czas_odPowstania++;
			transform.localScale += new Vector3 (tempo_wzrostu, tempo_wzrostu, tempo_wzrostu);
			transform.position += new Vector3 (0, tempo_wzrostu / 2, 0);
		}


		
	}

	public float GetSetTempoWzrostu
	{
		get{return tempo_wzrostu;}
		set{ tempo_wzrostu = value; }
	}



}
