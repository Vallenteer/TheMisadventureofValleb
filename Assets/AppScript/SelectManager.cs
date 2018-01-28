using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectManager : MonoBehaviour {

    public void MuseumSelector()
    {
        SceneManager.LoadScene(1);
    }
    public void MuseumList()
    {
        SceneManager.LoadScene(9);
    }
    public void GoToPPIPTEK()
    {
        ContiQRRead.SetIDMuseum("MUSEUM PPIPTEK TMII");
        SceneManager.LoadScene(2);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
