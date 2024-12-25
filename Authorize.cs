using System.Security.Cryptography;
using System.Text;
public static class Authorize
    {

    // login: player1
    // password: 224423

    // login: player2
    // password: 454589

    //testuser
    //78324

    private static string MySalt = "MyVerySecretSaltForMySuperDuperPassword";
    public static bool AuthorizePlayer(string login, string password)  //ETurnPlayer player
    {
        //Console.WriteLine($"Авторизация {player} игрока:");

        // Console.WriteLine($"Введите логин: ");
        //var login = Console.ReadLine();

        //Console.WriteLine($"Введите пароль: ");
        //var password = Console.ReadLine();

        var textForMD5 = $"{login}{password}{MySalt}";
        byte[] hash;
        using (MD5 md5 = MD5.Create())
        {
            hash = md5.ComputeHash(Encoding.UTF8.GetBytes(textForMD5));
        }
        //var d = string.Join(",", hash);
        return PasswordStorageDB.iKnowThisHash(hash);
    }
}