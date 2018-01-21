using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ACWatcher : MonoBehaviour
{
    [SerializeField] Image ImageHolder;
    [SerializeField] Sprite[] SpriteImage;

    // Use this for initialization
    void Start()
    {
        ImageHolder.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("AC1") != 1 && PlayerPrefs.GetInt("VisitPPIPTEK") == 1)
        {
            PlayerPrefs.SetInt("AC1", 1);
            StartCoroutine(ShowGetAchivement(0));

        }
        else if (PlayerPrefs.GetInt("AC2") != 1 && PlayerPrefs.GetInt("TotalAllPlayed") > 0)
        {
            PlayerPrefs.SetInt("AC2", 1);
            StartCoroutine(ShowGetAchivement(1));
        }
        else if (PlayerPrefs.GetInt("AC3") != 1 && PlayerPrefs.GetInt("HasPlayed") > 9)
        {
            PlayerPrefs.SetInt("AC3", 1);
            StartCoroutine(ShowGetAchivement(2));
        }
        else if (PlayerPrefs.GetInt("AC4") != 1 && PlayerPrefs.GetInt("CountQR") > 29)
        {
            PlayerPrefs.SetInt("AC4", 1);
            StartCoroutine(ShowGetAchivement(3));
        }
        else if (PlayerPrefs.GetInt("AC5") != 1 && PlayerPrefs.GetInt("PPIPTEKPERFECT") == 1)
        {
            PlayerPrefs.SetInt("AC5", 1);
            StartCoroutine(ShowGetAchivement(4));
        }
    }

    IEnumerator ShowGetAchivement(int number)
    {
        ImageHolder.sprite = SpriteImage[number];
        ImageHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ImageHolder.gameObject.SetActive(false);

    }
}
