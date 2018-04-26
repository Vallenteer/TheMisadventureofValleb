using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayMana : MonoBehaviour {
    [Header("Question Holder")]
    [SerializeField] Image questionHandler;
    [SerializeField] GameObject prevButton;

    [Header("Question List")]
    [SerializeField]
    Sprite[] questionList;
    int indexList;


    // Use this for initialization
    void Start () {
        indexList = 0;
        questionHandler.sprite = questionList[indexList];
        //indexList++;
    }
	
	// Update is called once per frame
	void Update () {
        if (indexList == 0)
        {
            prevButton.SetActive(false);
        }
        else {
            prevButton.SetActive(true);
        }
		
	}
    public void NextQuestion()
    {
        //cek apakah soal sudah habis atau belum
        indexList++;
        if (indexList < questionList.Length)
        {
            //reset all parameter and move to next question
            
            questionHandler.sprite = questionList[indexList];
            
            
        }
        else
        {
            //to point shower
            SceneManager.LoadScene(0);
        }

    }
    public void PrevQuestion()
    {
        //cek apakah soal sudah habis atau belum
        
        if (indexList > 0)
        {
            //reset all parameter and move to next question
            indexList--;
            questionHandler.sprite = questionList[indexList];
            
        }
    }
}
