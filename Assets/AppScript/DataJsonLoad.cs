using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataJsonLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GetPertanyaan());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GetPertanyaan()
    {
        string getPertanyaanUrl = "http://museumadv.azurewebsites.net/museum/list";
        using (UnityWebRequest www = UnityWebRequest.Post(getPertanyaanUrl,"1"))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isError || www.responseCode==500 || www.responseCode==404)
            {
                Debug.Log(www.responseCode);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult =System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    //JPertanyaan[] entities = JsonHelper.getJsonArray<JPertanyaan>(jsonResult);
                    JMuseum[] entities = JsonHelper.getJsonArray<JMuseum>(jsonResult);


                    foreach (var pert in entities)
                    {
                        Debug.Log(pert.museum);
                    }

                }
                
            }
        }
    }

}
