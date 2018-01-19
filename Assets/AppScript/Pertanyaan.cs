using SQLite4Unity3d;

public class Pertanyaan  {

	[PrimaryKey, AutoIncrement]
	public int id { get; set; }
	public string soal { get; set; }
	public string jawaban { get; set; }
	public string museum_id { get; set; }
    public bool telah_dijawab { get; set; }
    public string petunjuk { get; set; }

    public override string ToString ()
	{
		return string.Format ("[Person: Id={0}, Name={1},  Surname={2}, Age={3}]", id, soal, jawaban, museum_id);
	}
}