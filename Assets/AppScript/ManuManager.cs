using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManuManager : MonoBehaviour {

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void NextButton()
    {
        SceneManager.LoadScene(2);
    }
    public void MainMenuLoad()
    {
        SceneManager.LoadScene(0);
    }
    public void HowtoPlay()
    {
        SceneManager.LoadScene(4);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Debug.Log(SceneManager.GetActiveScene().buildIndex);
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
               // Debug.Log("test");
                ExitApp();
            }
            else
            {
                MainMenuLoad();
            }
        }
		
	}
}
