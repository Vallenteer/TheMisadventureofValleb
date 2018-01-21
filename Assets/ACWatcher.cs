using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACWatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("AC1") != 1 && PlayerPrefs.GetInt("VisitPPIPTEK") == 1)
        {
            PlayerPrefs.SetInt("AC1", 1);
        }
        else if (PlayerPrefs.GetInt("AC2") != 1 && PlayerPrefs.GetInt("TotalAllPlayed") > 0)
        {
            PlayerPrefs.SetInt("AC2", 1);
        }
        else if (PlayerPrefs.GetInt("AC3") != 1 && PlayerPrefs.GetInt("HasPlayed") > 9)
        {
            PlayerPrefs.SetInt("AC3", 1);
        }
        else if (PlayerPrefs.GetInt("AC4") != 1 && PlayerPrefs.GetInt("CountQR") > 29)
        {
            PlayerPrefs.SetInt("AC4", 1);
        }
        else if (PlayerPrefs.GetInt("AC5") != 1 && PlayerPrefs.GetInt("PPIPTEKPERFECT") ==1)
        {
            PlayerPrefs.SetInt("AC5", 1);
        }
    }
}
