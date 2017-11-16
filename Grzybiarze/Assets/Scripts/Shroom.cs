using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

	[SerializeField]
	float tempo_wzrostu;
	float czas_powstania;
	int czas_od_powstania;


	void Start () {
		
		tempo_wzrostu = Random.Range (1, 10) / 100f;
		czas_od_powstania = 1;
		czas_powstania = Time.time;
			
	}
	

	void Update () {

		if (czas_powstania + czas_od_powstania < Time.time)
		{
			czas_od_powstania++;
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
