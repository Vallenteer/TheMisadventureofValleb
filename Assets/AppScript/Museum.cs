using SQLite4Unity3d;

public class Museum
{

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string museum_name { get; set; }

    public override string ToString()
    {
        return string.Format("[Museum: Id={0}, Name={1}]", id, museum_name);
    }
}