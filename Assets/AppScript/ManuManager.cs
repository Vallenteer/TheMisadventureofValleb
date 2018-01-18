﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManuManager : MonoBehaviour {

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenuLoad()
    {
        SceneManager.LoadScene(0);
    }
    public void HowtoPlay()
    {
        SceneManager.LoadScene(4);
    }
    public void ProfilePage()
    {
        SceneManager.LoadScene(5);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    
    // Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
           // PlayerPrefs.SetInt("HasPlayed", 0);
            int playTime = PlayerPrefs.GetInt("HasPlayed");
            
            // Debug.Log("test");
            DataService ds = new DataService("museum.db");
            Debug.Log(playTime);
            if (playTime == 0)
            {
                ds.CreateDB();
                PlayerPrefs.SetInt("HasPlayed", 1);
            }
            else
            {
                
                playTime++;
                PlayerPrefs.SetInt("HasPlayed", playTime);
            }
        }
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
