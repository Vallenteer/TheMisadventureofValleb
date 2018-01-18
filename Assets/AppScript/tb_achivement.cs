using SQLite4Unity3d;

public class tb_achivement
{

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string nama_achivement { get; set; }
    public string sudah_tercapai { get; set; }
  
   
    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1},  Surname={2}]", id, nama_achivement,sudah_tercapai);
    }
}