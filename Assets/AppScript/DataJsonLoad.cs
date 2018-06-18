using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataJsonLoad : MonoBehaviour {

    Museum[] museumList;
    Pertanyaan[] pertanyaanList;
    double version ;
    // Use this for initialization
    void Start () {
        //StartCoroutine(GetVersion());
        museumList = null;
        pertanyaanList=null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateDB(DataJsonLoad jsonData, SQLiteConnection _connection)
    {
        StartCoroutine(WaitUpdate(jsonData,_connection));
    }

    IEnumerator WaitUpdate(DataJsonLoad jsonData, SQLiteConnection _connection)
    {
        //Museum[] museumList = jsonData.GetAllMuseum();
        //Pertanyaan[] pertanyaanList = jsonData.GetAllPertanyaan();
        StartCoroutine(GetMuseum());
        StartCoroutine(GetPertanyaan());
        yield return new WaitForSeconds(3f);
        //foreach (var item in museumList)
        //{
        //    _connection.Insert(item);
        //}
        _connection.InsertAll(museumList);
         _connection.InsertAll(pertanyaanList);

        Debug.Log("Update DB");
    }


    public  Pertanyaan[] GetAllPertanyaan()
    {
        StartCoroutine(GetPertanyaan());
        return pertanyaanList;
    }

    public  Museum[] GetAllMuseum()
    {
        StartCoroutine(GetMuseum());
        return museumList;
    }

    IEnumerator GetMuseum()
    {
        string getPertanyaanUrl = "http://museumadv.azurewebsites.net/museum/list";
        using (UnityWebRequest www = UnityWebRequest.Post(getPertanyaanUrl, "1"))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isError || www.responseCode == 500 || www.responseCode == 404)
            {
                Debug.Log(www.responseCode);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    //JPertanyaan[] entities = JsonHelper.getJsonArray<JPertanyaan>(jsonResult);
                    JMuseum[] entities = JsonHelper.getJsonArray<JMuseum>(jsonResult);

                    museumList = new Museum[entities.Length];
                    int i = 0;
                    foreach (var pert in entities)
                    {
                        //Debug.Log(pert.soal);
                        museumList[i] = new Museum();
                        museumList[i].museum_name =pert.museum;
                        i++;
                    }

                }

            }
        }
    }
    IEnumerator GetPertanyaan()
    {
        string getPertanyaanUrl = "http://museumadv.azurewebsites.net/pertanyaan/list";
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

                    JPertanyaan[] entities = JsonHelper.getJsonArray<JPertanyaan>(jsonResult);
                    //JMuseum[] entities = JsonHelper.getJsonArray<JMuseum>(jsonResult);


                    pertanyaanList = new Pertanyaan[entities.Length];
                    int i = 0;
                    foreach (var pert in entities)
                    {
                        //Debug.Log(pert.soal);
                        pertanyaanList[i] = new Pertanyaan();
                        pertanyaanList[i].soal = pert.soal;
                        pertanyaanList[i].jawaban = pert.jawaban;
                        pertanyaanList[i].museum_id = pert.museum;
                        pertanyaanList[i].petunjuk = pert.petunjuk;

                        i++;
                    }

                }
                
            }
        }
    }

    IEnumerator  GetVersion()
    {
        string getPertanyaanUrl = "http://museumadv.azurewebsites.net/version/list";
        using (UnityWebRequest www = UnityWebRequest.Post(getPertanyaanUrl, "1"))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isError || www.responseCode == 500 || www.responseCode == 404)
            {
                Debug.Log(www.responseCode);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    //JPertanyaan[] entities = JsonHelper.getJsonArray<JPertanyaan>(jsonResult);
                    JVersion[] entities = JsonHelper.getJsonArray<JVersion>(jsonResult);

                    museumList = new Museum[entities.Length];
                    int i = 0;
                    foreach (var pert in entities)
                    {
                        Debug.Log(pert.version +"aa");
                        version = pert.version;
                        PlayerPrefs.SetFloat("versionNext", (float)version);
                    }

                }

            }
        }
    }

  

    //true if need to be update
    public bool compareVersion()
    {
        StartCoroutine(GetVersion());
        double currentVersion = PlayerPrefs.GetFloat("version");
        double nextVersion = PlayerPrefs.GetFloat("versionNext");
        if (currentVersion <= nextVersion)
        {
            PlayerPrefs.SetFloat("version", (float)version);
            Debug.Log("Jalan");
            return true;
        }
        else
        {
            return false;
        }
    }


}
