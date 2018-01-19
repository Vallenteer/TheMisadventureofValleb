﻿using SQLite4Unity3d;
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
                soal = "Bagaimana cara menghindar terjadinya penularan sakit flu melalui bersin?",
                jawaban = "Etika Bersin",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Lapisan ozon yang paling jauh dari bumi adalah?",
                jawaban = "Lapisan Ozon Mesosfer",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Molekul rantai tunggal yang pembentukannya dikode oleh DNA dan dapat ditranslasikan menjadi protein adalah? ",
                jawaban = "RNA (Ribose Nucleic Acid)",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Beberapa hal yang dapat membuat virus mati adalah? ",
                jawaban = "Panas dan pengawetan dengan pengeringan",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Berapa tinggi burung Kasuari? ",
                jawaban = "1,8 Meter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },



            new Pertanyaan{
                soal = "Apakah benar parabola dapat mengumpulkan dan menguatkan sinyal suara dengan mengumpulkannya pada suatu titik fokus?",
                jawaban = "Benar",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Bunyi merambat melalui medium yaitu? ",
                jawaban = "Udara",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Planet yang memiliki kecepatan paling tinggi ketika mengelilingi matahari adalah?",
                jawaban = "Merkurius",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Apa rumus untuk mencari kecepatan (V)?",
                jawaban = "l/t",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa alat yang dapat membantu manusia untuk melihat benda yang terlalu kecil untuk dilihat dengan mata telanjang?",
                jawaban = "Mikroskop",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },



            new Pertanyaan{
                soal = "Apakah benar udara mengalir memiliki tekanan yang lebih tinggi dibandingkan tekanan udara di sekitarnya? ",
                jawaban = "Salah",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Dinosaurus apakah yang yang memiliki cula seperti badak ? ",
                jawaban = "Triceratops",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Terbagi menjadi berapa periode kah era mesozoic? ",
                jawaban = "Tiga Periode",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Ketika muatan listrik pada bola logam van de graff mengalir menuju tubuh dan rambut kita, maka rambut kita akan naik karena terjadi?",
                jawaban = "Tolak Menolak",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa teknologi yang digunakan Shinkanzen untuk menjaga agar tekanan di dalam kereta tetap?",
                jawaban = "Kereta Kedap Udara",
                petunjuk ="Lantai 3",
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