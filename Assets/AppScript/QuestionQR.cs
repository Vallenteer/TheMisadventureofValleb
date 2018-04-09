using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionQR : MonoBehaviour
{

    private IScanner BarcodeScanner;
   // public Text TextHeader;
    public RawImage QRImage;
    //public AudioSource Audio;
    private float RestartTime;
    
    private bool CanAnswer = false;

    private bool nextValue = false;
    [SerializeField] GameObject exitCanvas;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject next5Canvas;
    [Header("Clue Config")]
    [SerializeField] GameObject ClueCanvas;
    [SerializeField] Text ClueText;

    [Header("Point Config")]
    [SerializeField] Text pointHolder;
    public int Qpoint = 5;
    public static int PlayerPoin=0;

    [Header("Question Holder")]
    [SerializeField] GameObject Qholder;
    [SerializeField] Text questionHandler;
    [SerializeField] Button scanCaller;

    [Header("Image Correct Thingy")]
    [SerializeField] GameObject AnswerCanvas;
    [SerializeField] Image imageAnnounHolder;
    [SerializeField] Sprite correctImage;
    [SerializeField] Sprite falseImage;
    [SerializeField] GameObject TextAnswerHolder;
    [SerializeField] Text answerTextShow;
    [Header("Question List")]
    [SerializeField] Text currentIndex;
    [SerializeField] Text maxIndex;
    [SerializeField] string[] questionList;
    [SerializeField] string[] answerList;
    [SerializeField]int[] IDquestionList;
    [SerializeField]string[] petunjutkList;
    bool[] isAnswered;
    int sizeArry;
    int indexSoal;
    DataService ds;

    private bool _isPushed;
    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;


        //generate soal
        ds = new DataService("museum.db");
        var questions = ds.GetPertanyaanMuseum(ContiQRRead.Museum_ID);
        MakeQuestion(questions);
        _isPushed = false;
        //set point to zero
        PlayerPoin = 0;
    }
    private void MakeQuestion(IEnumerable<Pertanyaan> DaftarPertanyaan)
    {
        int playCount = PlayerPrefs.GetInt(ContiQRRead.Museum_ID + "Played");
        int i = 0;
        //soal akan terus ngurut kebawah saja ( Sementara ini nanti akan dibuat random)
        foreach (var question in DaftarPertanyaan)
        {
            if (i < questionList.Length && question.telah_dijawab == false)
            {
                IDquestionList[i] = question.id;
                questionList[i] = question.soal;
                answerList[i] = question.jawaban;
                petunjutkList[i] = question.petunjuk;
                i++;
            }
        }

        

        
    }

    void Start()
    {
        //hide next Button
        //nextButton.SetActive(false);
        sizeArry = questionList.Length;
        QRImage.enabled = false;
        //openAnswer();
        //imageAnnounHolder.enabled = true;
        //TextAnswerHolder.SetActive(true);
        closeAnswer();


        //Set First Question
        indexSoal = 0;
        questionHandler.text=questionList[indexSoal];
        maxIndex.text = questionList.Length.ToString();
        isAnswered = new bool[questionList.Length];
        //default : false
        //Debug.Log(isAnswered[0].ToString());

        // Create a basic scanner
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) => {
            // Set Orientation & Texture
            QRImage.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            QRImage.transform.localScale = BarcodeScanner.Camera.GetScale();
            QRImage.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = QRImage.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

            RestartTime = Time.realtimeSinceStartup;
        };
    }

    /// <summary>
    /// Start a scan and wait for the callback (wait 1s after a scan success to avoid scanning multiple time the same element)
    /// </summary>
    private void StartScanner()
    {
        BarcodeScanner.Scan((barCodeType, barCodeValue) => {
            BarcodeScanner.Stop();
            //if (TextHeader.text.Length > 250)
            //{
            //	TextHeader.text = "";
            //}

            //if Succeed Read Code
            if (barCodeType == "QR_CODE" && CanAnswer==true)
            {
                int QRCount=PlayerPrefs.GetInt("CountQR");
                PlayerPrefs.SetInt("CountQR", QRCount + 1);
                answerTextShow.text = barCodeValue;
                openAnswer();
                TextAnswerHolder.SetActive(true);
                //cek apakah jawaban benar atau tidak
                if (barCodeValue == answerList[indexSoal])
                {
                    ds.UpdateStatusSoal(IDquestionList[indexSoal], 1);
                    imageAnnounHolder.enabled = true;
                    imageAnnounHolder.sprite = correctImage;
                    // TextHeader.color = Color.green;
                    // TextHeader.text = "Correct!!";
                    if(isAnswered[indexSoal]==false){
                        PlayerPoin += Qpoint;
                        UpdateScore(Qpoint);
                    }
                   
                    nextButton.SetActive(true);
                    CanAnswer = false;
                    //update ke database bahwa soal benar && update poin langsung.
                    ds.UpdateStatusSoal(IDquestionList[indexSoal], 1);
                    
                    isAnswered[indexSoal] = true;
                    //NextQuestion();

                }
                else
                {
                    imageAnnounHolder.enabled = true;
                    imageAnnounHolder.sprite = falseImage;
                   // TextHeader.color = Color.red;
                   // TextHeader.text = "False Answer";
                    if (Qpoint > 1)
                    {
                        Qpoint--;
                    }
                    Qholder.SetActive(true);
                    QRImage.enabled = false;
                    nextButton.SetActive(true);
                    CanAnswer = false;
                }

            }
            RestartTime += Time.realtimeSinceStartup + 1f;

            // Feedback
            //Audio.Play();

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        });
    }

    /// <summary>
    /// The Update method from unity need to be propagated
    /// </summary>
    void Update()
    {
        pointHolder.text = PlayerPoin.ToString();
        currentIndex.text = (indexSoal + 1).ToString();
        if (BarcodeScanner != null)
        {
            BarcodeScanner.Update();
        }
        //if question already answerd cannot ansewer again
        if (isAnswered[indexSoal] == true)
        {
            scanCaller.interactable = false;
        }
        else
        {
            scanCaller.interactable = true;
        }


        // Check if the Scanner need to be started or restarted
        if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
        {
            StartScanner();
            RestartTime = 0;
        }
        if (Input.GetKey(KeyCode.Escape)&& _isPushed==false)
        {
            if (AnswerCanvas.activeSelf == true)
            {
                closeAnswer();
                StartCoroutine(Pushbutton());
            }
            else if (ClueCanvas.activeSelf==true)
            {
                closeClue();
                StartCoroutine(Pushbutton());
            }
            else if (next5Canvas.activeSelf == true)
            {
                closeNext();
                StartCoroutine(Pushbutton());
            }
            else if (Qholder.activeSelf == true)
            {

                //Debug.Log("Henshin:");
                StartCoroutine(Pushbutton());
                //ClickBack();
                openExit();
            }
            else
            {
                StartCoroutine(Pushbutton());
                QRImage.enabled = false;
                CanAnswer = false;
                Qholder.SetActive(true);
            }
        }
    }

    public void openNext()
    {
        next5Canvas.gameObject.SetActive(true);
    }
    public void closeNext()
    {
        next5Canvas.gameObject.SetActive(false);
    }
    public void openExit() {
        exitCanvas.gameObject.SetActive(true);
    }
    public void closeExit() {
        exitCanvas.gameObject.SetActive(false);
    }
    public void openAnswer()
    {
        AnswerCanvas.gameObject.SetActive(true);
    }
    public void closeAnswer()
    {
        //QRImage.enabled = false;
        AnswerCanvas.gameObject.SetActive(false);        
    }

    public void closeClue()
    {
        ClueCanvas.gameObject.SetActive(false);
    }
    public void OpenClue()
    {
        ClueCanvas.gameObject.SetActive(true);
        ClueText.text = petunjutkList[indexSoal];
    }
    public void UpdateScore(int getScore)
    {
        //menambahkan score dari dulu ke yang sekarang
        int currentPoint = PlayerPrefs.GetInt("PlayerScore") + getScore;
        PlayerPrefs.SetInt("PlayerScore", currentPoint);
    }

    IEnumerator Pushbutton()
    {
        _isPushed = true;
        yield return new WaitForSeconds(0.7f);
        _isPushed = false;
    }

    #region UI Buttons
    public void ScanStart()
    {
        Qholder.SetActive(false);
        closeAnswer();
        //TextAnswerHolder.SetActive(false);
        //imageAnnounHolder.enabled = false;
        QRImage.enabled = true;
        CanAnswer = true;
    }

    public void PrevQuestion()
    {
        QRImage.enabled = false;
        closeAnswer();
        //TextAnswerHolder.SetActive(false);
        //imageAnnounHolder.enabled = false;
        //cek apakah soal sudah habis atau belum
        if (indexSoal > 0)
        {
            //reset all parameter and move to next question
            Qpoint = 5;
            indexSoal--;
            // TextHeader.text = "";
            questionHandler.text = questionList[indexSoal];
            //nextButton.SetActive(false);
            CanAnswer = false;
            Qholder.SetActive(true);
        }

    }

    public void NextQuestionProxy()
    {
        if (isAnswered[indexSoal] == true)
        {
            NextQuestion();
        }
    }
    public void NextQuestion()
    {
        
        QRImage.enabled = false;
        closeAnswer();
        //TextAnswerHolder.SetActive(false);
        //imageAnnounHolder.enabled = false;
        //cek apakah soal sudah habis atau belum
        if (indexSoal < questionList.Length-1)
        {
            //reset all parameter and move to next question
            Qpoint = 5;
            indexSoal++;
           // TextHeader.text = "";
            questionHandler.text = questionList[indexSoal];
            //nextButton.SetActive(false);
            CanAnswer = false;
            Qholder.SetActive(true);
        }
        else
        {

            bool containsFalse = false;
            for (int j = 0; j < isAnswered.Length; j++)
            {
                //if the current element the array is equals to false, then containsFalse is true,
                //then exit for loop
                if (isAnswered[j] == false)
                {
                    containsFalse = true;
                    break;
                }
            }

            if (containsFalse)
            {
                //your true_or_false array contains a false then open next5 canvas.
                Qholder.SetActive(true);
                openNext();

            }
            else {
                //open score page
                ScorePage();
            }
        }

    }
    public void ScorePage()
    {
        int playAllCount = PlayerPrefs.GetInt("TotalAllPlayed");
        int playCount = PlayerPrefs.GetInt(ContiQRRead.Museum_ID + "Played");


        PlayerPrefs.SetInt(ContiQRRead.Museum_ID + "Played", playCount + 1);
        PlayerPrefs.SetInt("TotalAllPlayed", playAllCount + 1);



        //batasnya harus dirubah nanti pke variable
        if (ContiQRRead.Museum_ID == "MUSEUM PPIPTEK TMII" && PlayerPoin >= (sizeArry * 5))
        {
            PlayerPrefs.SetInt("PPIPTEKPERFECT", 1);
        }

        //to point shower
        //StartCoroutine(StopCamera(() => {
        SceneManager.LoadScene(3);
        //}));
    }

    //exit to main menu
    public void ClickBack()
    {
        // Try to stop the camera before loading another scene
        //StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene(0);
        //}));
    }


    /// <summary>
    /// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
    /// Trying to stop the camera in OnDestroy provoke random crash on Android
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        QRImage = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}

