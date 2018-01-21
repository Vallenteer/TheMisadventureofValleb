using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACManager : MonoBehaviour {
    [SerializeField] Button[] ACTampilan;
    [SerializeField] GameObject[] Checklist;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < ACTampilan.Length; i++)
        {
            if (PlayerPrefs.GetInt("AC" + (i+1)) == 1)
            {
                ACTampilan[i].interactable = true;
                Checklist[i].gameObject.SetActive(true);
            }
            else
            {
                ACTampilan[i].interactable = false;
                Checklist[i].gameObject.SetActive(false);
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
