using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {
	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

		#if UNITY_EDITOR
		var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
		#else
		// check if file exists in Application.persistentDataPath
		var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

		if (!File.Exists(filepath))
		{
			Debug.Log("Database not in Persistent path");
			// if it doesn't ->
			// open StreamingAssets directory and load the db ->

		#if UNITY_ANDROID 
		var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
		while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		// then save to Application.persistentDataPath
		File.WriteAllBytes(filepath, loadDb.bytes);
		#elif UNITY_IOS
		var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#elif UNITY_WP8
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);

		#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		#else
			var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);

		#endif

			Debug.Log("Database written");
		}

		var dbPath = filepath;
		#endif
		_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
		Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
        //drop all
		_connection.DropTable<Pertanyaan> ();
        _connection.DropTable<tb_achivement>();
        //create all
        _connection.CreateTable<Pertanyaan> ();
        _connection.CreateTable<tb_achivement>();


        _connection.InsertAll(new[]{
            new tb_achivement{
                nama_achivement="First Use",
                sudah_tercapai="true"
               
            }
        });
		_connection.InsertAll (new[]{
            
            new Pertanyaan{
				soal = "Mobil apa yang pernah digunakan ratu Elizabeth?",
				jawaban = "Mobil Bentley",
				museum_id = "Museum Prototipe"
            },
			new Pertanyaan{
				soal = "Alat apa yang dapat digunakan untuk pertanian, upacara , dan barter di jawa barat?",
				jawaban = "Adze",
				museum_id = "Museum Prototipe"
            },new Pertanyaan{
                soal = "Kubur batu yang hanya untuk masyrakat dengan status sosial tinggi pada zaman dahulu adalah..",
                jawaban = "Sarkofagus",
                museum_id = "Museum Prototipe"
            },
            new Pertanyaan{
                soal = "Rumah khusus untuk laki laki papua adalah...",
                jawaban = "Rumah Kon",
                museum_id = "Museum Prototipe"
            },new Pertanyaan{
                soal = "Koleksi terbesar di museum Zoologi Bogor ...",
                jawaban = "Ikan Paus Biru Raksasa",
                museum_id = "Museum Prototipe"
            },
            new Pertanyaan{
                soal = "Siapa yang menemukan hukum gravitasi dan membuat teleskop refleksi pertama kali?",
                jawaban = "Sir Isaac Newton",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Benda yang digunakan untuk melihat benda-benda yang berukuran kecil (mikroskopis)",
                jawaban = "Mikroskop",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Pembangkit yang mengandalkan  energipotensial  dan kinetik dari air untuk menghasilkan energi listrik",
                jawaban = "PLTA",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Alat dari selsurya (silium) yang mengubah cahaya menjadi listrik adalah?",
                jawaban = "Panel surya",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Lapisan di atmosfer pada ketinggian 20−35 km di atas permukaan bumi.",
                jawaban = "Lapisan ozon",
                museum_id = "MUSEUM PPIPTEK TMII"
            }
             
        });
        
    }

    //read All list
	public IEnumerable<Pertanyaan> GetPertanyaan(){
		return _connection.Table<Pertanyaan>();
	}

    //read Query
	public IEnumerable<Pertanyaan> GetPertanyaanMuseum(string museumID){
		return _connection.Table<Pertanyaan>().Where(x => x.museum_id ==museumID );
	}
    public IEnumerable<tb_achivement> CheckFirstTimeUse()
    {
        return _connection.Table<tb_achivement>();
    }
	public Pertanyaan CreatePertanyaan(){
		var p = new Pertanyaan{
			soal = "Soal",
			jawaban = "Jawaban",
			museum_id = "ID"
		};
		_connection.Insert (p);
		return p;
	}

    public void UpdateStatusSoal(int idSoal, int statusSoal)
    {

        _connection.Execute("UPDATE Pertanyaan SET telah_dijawab = " + statusSoal + " WHERE id = " + idSoal);

    }
    public void UpdateSoal(int idSoal, string statusSoal)
    {
        
        _connection.Execute("UPDATE Pertanyaan SET soal = '" + statusSoal + "' WHERE id = " + idSoal);

    }
}