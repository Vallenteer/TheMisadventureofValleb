using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (PlayerPrefs.GetInt("TukarGelas") == 1)
        {
            buttonTukar.GetComponent<Button>().interactable = false;
        }

		
	}

    public void TukarButton()
    {
        int sem = PlayerPrefs.GetInt("PlayerScore");
        if (sem > 19)
        {
            PlayerPrefs.SetInt("PlayerScore", sem - 20);
            PlayerPrefs.SetInt("TukarGelas", 1);
            SceneManager.LoadScene(6);
        }
        else
        {
            PoinHolder.text = "Poin Masih Kurang, Untuk menukarkan gelas butuh 20 poin";
        }
    }
}
