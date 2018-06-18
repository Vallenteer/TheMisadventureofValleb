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

    public void UpdateDBLink(DataJsonLoad jsonData)
    {
        //drop all
        _connection.DropTable<Pertanyaan>();
        _connection.DropTable<Museum>();
        //create all
        _connection.CreateTable<Pertanyaan>();
        _connection.CreateTable<Museum>();


        jsonData.updateDB(jsonData, _connection);
        

       
    }

    


	public void CreateDB(){
        //drop all
		_connection.DropTable<Pertanyaan> ();
        _connection.DropTable<tb_achivement>();
        _connection.DropTable<Museum>();
        //create all
        _connection.CreateTable<Pertanyaan> ();
        _connection.CreateTable<tb_achivement>();
        _connection.CreateTable<Museum>();


        _connection.InsertAll(new[]{
            new tb_achivement{
                nama_achivement="First Use",
                sudah_tercapai="true"
               
            }
        });
        
        _connection.InsertAll(new[] {
            new Museum{
                museum_name="Museum Prototipe"
            },
            new Museum{
                museum_name="MUSEUM PPIPTEK TMII"
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
                soal = "Dinosaurus apakah yang yang memiliki cula seperti badak ? ",
                jawaban = "Triceratops",
                petunjuk ="Lantai 1 - Dinosaurus",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Terbagi menjadi berapa periode kah era mesozoic? ",
                jawaban = "Tiga Periode",
                petunjuk ="Lantai 1 - Dinosaurus",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Bagaimana cara menghindar terjadinya penularan sakit flu melalui bersin?",
                jawaban = "Etika Bersin",
                petunjuk ="Lantai 3 - Influenza",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Lapisan ozon yang paling jauh dari bumi adalah?",
                jawaban = "Lapisan Ozon Mesosfer",
                petunjuk ="Lantai 3 - Ozon",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Molekul rantai tunggal yang pembentukannya dikode oleh DNA dan dapat ditranslasikan menjadi protein adalah? ",
                jawaban = "RNA (Ribose Nucleic Acid)",
                petunjuk ="Lantai 3 - Flu Burung",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Beberapa hal yang dapat membuat virus mati adalah? ",
                jawaban = "Panas dan pengawetan dengan pengeringan",
                petunjuk ="Lantai 3 - Flu Burung",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Berapa tinggi burung Kasuari? ",
                jawaban = "1,8 Meter",
                petunjuk ="Lantai 3 - Flu Burung",
                museum_id = "MUSEUM PPIPTEK TMII"
            },



            new Pertanyaan{
                soal = "Apakah benar parabola dapat mengumpulkan dan menguatkan sinyal suara dengan mengumpulkannya pada suatu titik fokus?",
                jawaban = "Benar",
                petunjuk ="Lantai 2 - Suara",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Bunyi merambat melalui medium yaitu? ",
                jawaban = "Udara",
                petunjuk ="Lantai 2 - Suara",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Planet yang memiliki kecepatan paling tinggi ketika mengelilingi matahari adalah?",
                jawaban = "Merkurius",
                petunjuk ="Lantai 2 - Bola Tennis",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Apa rumus untuk mencari kecepatan (V)?",
                jawaban = "L/T",
                petunjuk ="Lantai 2 - Ilmu Matematika (Sebelah Cahaya)",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa alat yang dapat membantu manusia untuk melihat benda yang terlalu kecil untuk dilihat dengan mata telanjang?",
                jawaban = "Mikroskop",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },



            new Pertanyaan{
                soal = "Apakah benar udara mengalir memiliki tekanan yang lebih tinggi dibandingkan tekanan udara di sekitarnya? ",
                jawaban = "Salah",
                petunjuk ="Lantai 2 - 2 Bola",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Ketika muatan listrik pada bola logam van de graff mengalir menuju tubuh dan rambut kita, maka rambut kita akan naik karena terjadi?",
                jawaban = "Tolak Menolak",
                petunjuk ="Lantai 1 - Fisika",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa teknologi yang digunakan Shinkanzen untuk menjaga agar tekanan di dalam kereta tetap?",
                jawaban = "Kereta Kedap Udara",
                petunjuk ="Lantai 1 - Fisika",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Dinosaurus apakah yang yang memiliki cula seperti badak ? ",
                jawaban = "Triceratops",
                petunjuk ="Lantai 1 - Dinosaurus",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Terbagi menjadi berapa periode kah era mesozoic? ",
                jawaban = "Tiga Periode",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            

        
            //soal untuk dimainkan ke-dua kali
            			new Pertanyaan{
                soal = "Bagaimana sakit flu dapat menular kepada orang lain?",
                jawaban = "Melalui Semburan Air Liur",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Bagaimana cara menghindari penyebaran virus melalui sentuhan? ",
                jawaban = "Mencuci Tangan Dengan Teratur",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apakah benar seseorang dapat terkena flu karena debu?",
                jawaban = "Salah",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Apakah benar seseorang dapat terkena flu karena memakai kipas angin?",
                jawaban = "Salah",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Pelangi dapat terbentuk ketika tetesan air hujan menyebar diantara? ",
                jawaban = "Cahaya matahari",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },


                        new Pertanyaan{
                soal = "Cahaya matahari dering disebut cahaya putih atau juga dapat disebut?",
                jawaban = "Polikromatik",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Lapisan ozon yang melindungi bumi dari radiasi UV-B matahari adalah?",
                jawaban = "Lapisan Ozon Stratosfer",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Molekul ozon pada lapisan mana yang dapat sebagai pencemar dan berbahaya bagi kesehatan manusia",
                jawaban = "Lapisan Ozon Troposfer",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Glikoprotein pada permukaan virus, berfungsi sebagai pengikat antara virus dengan permukaan sel inang adalah?",
                jawaban = "Haemaglutinin",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Glikoprotein pada permukaan virus, sebagai enzim yang membantu proses inspeksi ke sel inang adalah?",
                jawaban = "Neuraminidase",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },




                new Pertanyaan{
                soal = "Ketika kita menghirup udara saat bernapas, maka rongga udara pada paru-paru akan ... ?",
                jawaban = "Mengembang",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Sinar yang melewati prisma akan terurai menjadi? ",
                jawaban = "Spektrum Pelangi",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa prinsip yang digunakan pada layar TV, layar komputer dan monitor video? ",
                jawaban = "Pixel",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Apa benda pengukur waktu yang terdiri atas dua tabung (atas dan bawah) dan berisi pasir kering?",
                jawaban = "Jam pasir",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa bentuk lintasan orbit benda langit seperti planet dan bintang? ",
                jawaban = "Elips",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
			
			
			//KETIGA
			
				new Pertanyaan{
                soal = "Enzim yang bertugas mengenali tempat tertentu pada rantai DNA yang menentukan mulainya transkripsi (duplikasi kode DNA) adalah?",
                jawaban = "Polimerase (Enzim)",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Kombinasi protein dan karbohidrat dalam jumlah besar adalah? ",
                jawaban = "Nukleoprotein",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Selubung virus yang tersusun atas dua lapis lipid (lemak) adalah?",
                jawaban = "Senyawa Lipid Dua Lapis",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Lapisan protein di bawah selubung lipid adalah?",
                jawaban = "Matriks Protein",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Karena ukurannya yang sangat kecil, virus hanya daat dilihat menggunakan? ",
                jawaban = "Mikroskop Elektron",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },


                        new Pertanyaan{
                soal = "Berapa tinggi burung unta? ",
                jawaban = "2.5 Meter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Berapa tinggi angsa? ",
                jawaban = "84 Centimeter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Apa alat yang digunakan oleh kapten kapal pada zaman dahulu untuk berkomunksi dengan ahli mesin?",
                jawaban = "Tabung Suara",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Perbedan tekanan di luar angkasa yang sangat dramatis dapat membuat tubuh manusia meledak karena?",
                jawaban = "Tubuh Terdifusi",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Berapa tinggi burung kolibri (hummingbird)",
                jawaban = "4.2 Centimeter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },




                new Pertanyaan{
                soal = "Bentuk apa yang akan terbentuk ketika bidang pengiris memotong kerucut tegak lurus sumbu kerucut?",
                jawaban = "Lingkaran",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Bentuk apa yang akan terbentuk ketika bidang pengiris memotong kerucut melalui alas kerucut?",
                jawaban = "Parabola",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Bentuk apa yang akan terbentuk ketika bidang pengiris memotong kerucut searah sumbu kerucut?",
                jawaban = "Hiperbola",
                petunjuk ="Lantai 2",
                museum_id = "MUSEUM PPIPTEK TMII"
            },new Pertanyaan{
                soal = "Berapa tinggi burung merpati? ",
                jawaban = "27 Centimeter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            },
            new Pertanyaan{
                soal = "Berapa tinggi ayam?",
                jawaban = "48 Centimeter",
                petunjuk ="Lantai 3",
                museum_id = "MUSEUM PPIPTEK TMII"
            }
        });
        
    }

    //read All list
	public IEnumerable<Pertanyaan> GetPertanyaan(){
		return _connection.Table<Pertanyaan>();
	}
    public IEnumerable<Museum> GetMuseumList()
    {
        return _connection.Table<Museum>();
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