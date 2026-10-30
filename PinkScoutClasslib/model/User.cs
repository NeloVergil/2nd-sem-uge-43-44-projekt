namespace PinkScoutClasslib;

public class User
{
    private int _id;
    private string _name;
    private string _passwordHash;
    private bool _isLeader;
    private int _age;

    public User(int id, string name, string passwordHash, bool isLeader, int age)
    {
        _id = id;
        _name = name;
        _passwordHash = passwordHash;
        _isLeader = isLeader;
        _age = age;
    }

    public User(int id, string name, bool isLeader, int age)
    {
        _id = id;
        _name = name;
        _isLeader = isLeader;
        _age = age;
    }


    public int Id 
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name 
    {
        get { return _name; }
        set { _name = value; }
    }

    public string PasswordHash 
    {
        get { return _passwordHash; }
        set { _passwordHash = value; }
    }

    public bool IsLeader 
    {
        get { return _isLeader; }
        set { _isLeader = value; }
    }

    public int Age 
    {
        get { return _age; }
        set { _age = value; }
    }

    // public override string ToString()
    // {
    //     return $"id {Id} navn: {_name}";
    // }
}
