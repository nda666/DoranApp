namespace DoranApp.Utils;

public class Session
{
    protected static Masteruser _User;

    public static void SetUser(Masteruser user)
    {
        _User = user;
    }

    public static Masteruser GetUser()
    {
        return _User;
    }
}