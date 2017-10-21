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
    [SerializeField] GameObject nextButton;
    [Header("Point Config")]
    [SerializeField] Text pointHolder;
    public int Qpoint = 5;
    public static int PlayerPoin=0;

    [Header("Question Holder")]
    [SerializeField] GameObject Qholder;
    [SerializeField] Text questionHandler;

    [Header("Image Correct Thingy")]
    [SerializeField] Image imageAnnounHolder;
    [SerializeField] Sprite correctImage;
    [SerializeField] Sprite falseImage;
    [SerializeField] GameObject TextAnswerHolder;
    [SerializeField] Text answerTextShow;
    [Header("Question List")]
    [SerializeField] string[] questionList;
    [SerializeField] string[] answerList;
    int sizeArry;
    int indexSoal;

    private bool _isPushed;
    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        //generate soal
        DataService ds = new DataService("museum.db");
        var questions = ds.GetPertanyaanMuseum(ContiQRRead.Museum_ID);
        MakeQuestion(questions);
        _isPushed = false;

    }
    private void MakeQuestion(IEnumerable<Pertanyaan> DaftarPertanyaan)
    {
        int i = 0;
        foreach (var question in DaftarPertanyaan)
        {
            if (i < questionList.Length)
            {
                questionList[i] = question.soal;
                answerList[i] = question.jawaban;
                i++;
            }
        }
    }

    void Start()
    {
        //hide next Button
        nextButton.SetActive(false);
        sizeArry = questionList.Length;
        QRImage.enabled = false;
        imageAnnounHolder.enabled = false;
        TextAnswerHolder.SetActive(false);

        //Set First Question
        indexSoal = 0;
        questionHandler.text=questionList[indexSoal];

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
                answerTextShow.text = barCodeValue;
                TextAnswerHolder.SetActive(true);
                if (barCodeValue == answerList[indexSoal])
                {

                    imageAnnounHolder.enabled = true;
                    imageAnnounHolder.sprite = correctImage;
                   // TextHeader.color = Color.green;
                   // TextHeader.text = "Correct!!";
                    PlayerPoin += Qpoint;
                    nextButton.SetActive(true);
                    CanAnswer = false;
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
        if (BarcodeScanner != null)
        {
            BarcodeScanner.Update();
        }

        // Check if the Scanner need to be started or restarted
        if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
        {
            StartScanner();
            RestartTime = 0;
        }
        if (Input.GetKey(KeyCode.Escape)&& _isPushed==false)
        {
            if (Qholder.activeSelf == true)
            {

                //Debug.Log("Henshin:");
                StartCoroutine(Pushbutton());
                ClickBack();
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
        TextAnswerHolder.SetActive(false);
        imageAnnounHolder.enabled = false;
        QRImage.enabled = true;
        CanAnswer = true;
    }
    public void NextQuestion()
    {
        TextAnswerHolder.SetActive(false);
        QRImage.enabled = false;
        imageAnnounHolder.enabled = false;
        //cek apakah soal sudah habis atau belum
        if (indexSoal < questionList.Length-1)
        {
            //reset all parameter and move to next question
            Qpoint = 5;
            indexSoal++;
           // TextHeader.text = "";
            questionHandler.text = questionList[indexSoal];
            nextButton.SetActive(false);
            CanAnswer = false;
            Qholder.SetActive(true);
        }
        else
        {
            //to point shower
            //StartCoroutine(StopCamera(() => {
                SceneManager.LoadScene(3);
            //}));
        }

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

