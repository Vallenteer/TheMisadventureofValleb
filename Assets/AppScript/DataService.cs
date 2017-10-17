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
		_connection.DropTable<Pertanyaan> ();
		_connection.CreateTable<Pertanyaan> ();

		_connection.InsertAll (new[]{
			new Pertanyaan{
				soal = "Nama presiden republik indonesia?",
				jawaban = "Jokowi",
				museum_id = "RI"
			},
			new Pertanyaan{
				soal = "Nama wakil presiden republik indonesia?",
				jawaban = "Jusuf Kalla",
				museum_id = "RI"
			}
		});
	}

	public IEnumerable<Pertanyaan> GetPertanyaan(){
		return _connection.Table<Pertanyaan>();
	}

	public IEnumerable<Pertanyaan> GetPertanyaanWhereJawabanJK(){
	Debug.Log (_connection.Table<Pertanyaan>().Where(x => x.jawaban == "Jusuf Kalla"));
		return _connection.Table<Pertanyaan>().Where(x => x.jawaban == "Jusuf Kalla");
	}

	public Pertanyaan GetJokowi(){
		return _connection.Table<Pertanyaan>().Where(x => x.jawaban == "Jokowi").FirstOrDefault();
	}

	public Pertanyaan CreatePertanyaan(){
		var p = new Pertanyaan{
			soal = "Ibu negara RI?",
			jawaban = "Iriana Jokowi",
			museum_id = "RI"
		};
		_connection.Insert (p);
		return p;
	}
}