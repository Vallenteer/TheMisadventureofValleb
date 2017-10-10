using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayMana : MonoBehaviour {
    [Header("Question Holder")]
    [SerializeField] Text questionHandler;


    [Header("Question List")]
    [SerializeField]
    string[] questionList;
    int indexList;


    // Use this for initialization
    void Start () {
        indexList = 0;
        questionHandler.text = questionList[indexList];
        indexList++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextQuestion()
    {
        //cek apakah soal sudah habis atau belum
        if (indexList < questionList.Length)
        {
            //reset all parameter and move to next question
            questionHandler.text = questionList[indexList];
            indexList++;
            
        }
        else
        {
            //to point shower
            SceneManager.LoadScene(0);
        }

    }
}
