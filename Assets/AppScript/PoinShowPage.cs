using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoinShowPage : MonoBehaviour {

    [SerializeField] Text PoinHolder;
    // Use this for initialization
	void Start () {
        PoinHolder.text = QuestionQR.PlayerPoin.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
