using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;


public class PoinManager : MonoBehaviour {

    [SerializeField] Text PoinHolder;
    [SerializeField] GameObject buttonTukar;
    // Use this for initialization
    void Start()
    {
        PoinHolder.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        if (PlayerPrefs.GetInt("TotalAllPlayed") > 0)
        {
            buttonTukar.SetActive(true);
        }
        else
        {
            buttonTukar.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void TukarButton()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
    }
}
