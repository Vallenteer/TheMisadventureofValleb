using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContiQRRead : MonoBehaviour {
    private bool _isPushed;
    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;
    //public AudioSource Audio;
    private float RestartTime;

    private bool nextValue = false;
    [SerializeField] Button nextButton;
    public static string Museum_ID;

    [SerializeField] List<string> MuseumNames;

    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
    }

    void Start()
    {
        //hide next Button
        nextButton.interactable = false;

        // Create a basic scanner
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) => {
            // Set Orientation & Texture
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
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
            if (barCodeType == "QR_CODE")
            {
                int QRCount = PlayerPrefs.GetInt("CountQR");
                PlayerPrefs.SetInt("CountQR", QRCount + 1);
                if (checkMuseum(barCodeValue))
                {
                    if (barCodeValue == "MUSEUM PPIPTEK TMII")
                    {
                        PlayerPrefs.SetInt("VisitPPIPTEK", 1);
                    }
                    TextHeader.text = barCodeValue;
                    nextButton.interactable = true;
                }
                else
                {
                    TextHeader.text = "Code Museum Salah";
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

    public static void SetIDMuseum(string ID)
    {
        Museum_ID = ID;
    }
    private bool checkMuseum(string BarcodeRead)
    {
        foreach (string museum in MuseumNames)
        {
            if (BarcodeRead == museum)
            {
                Museum_ID = museum;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// The Update method from unity need to be propagated
    /// </summary>
    void Update()
    {
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
        if (Input.GetKey(KeyCode.Escape) && _isPushed == false)
        {


            //Debug.Log("Henshin:");
            StartCoroutine(Pushbutton());
            ClickBack();
        }


    }
    IEnumerator Pushbutton()
    {
        _isPushed = true;
        yield return new WaitForSeconds(0.7f);
        _isPushed = false;
    }

    #region UI Buttons

    public void ClickBack()
    {
        // Try to stop the camera before loading another scene
        //StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene(0);
        //}));
    }
    public void NextButton()
    {
        StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene(2);
        }));
       
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
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}
