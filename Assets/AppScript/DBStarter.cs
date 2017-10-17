using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DataService ds = new DataService("museum.db");
		//ds.CreatePertanyaan ();
		ds.GetPertanyaanWhereJawabanJK ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
