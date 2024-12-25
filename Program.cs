using System.Numerics;
using System.Security.Cryptography;

Console.Clear();

GameSeaBattle CurrentGame = new GameSeaBattle();

bool p1 = false, p2 = false;

do
{
    Console.WriteLine($"Авторизация 1 игрока:");
    Console.WriteLine($"Введите логин: ");

    var login = Console.ReadLine();

    Console.WriteLine($"Введите пароль: ");
    var password = Console.ReadLine();

    if (!Authorize.AuthorizePlayer(login, password))         //ETurnPlayer.Player1
        Console.WriteLine($"Авторизация первого игрока провалена");
    else
        p1 = true;
} while (!p1);

do
{
    Console.WriteLine($"Авторизация 2 игрока:");

    Console.WriteLine($"Введите логин: ");
    var login = Console.ReadLine();

    Console.WriteLine($"Введите пароль: ");
    var password = Console.ReadLine();

    if (!Authorize.AuthorizePlayer(login, password))       //ETurnPlayer.Player2
        Console.WriteLine($"Авторизация второго игрока провалена");
    else
        p2 = true;
} while (!p2);


do {
    Console.WriteLine($"Выберите действие: 1-Начать новую игру; 2-Продолжить игру;");
    var InputChoice = Console.ReadLine();
    if (InputChoice == "1")
    {
        CurrentGame.Init();
        break;
    }
    else if (InputChoice == "2")
    {
        if(CurrentGame.LoadLastGame() == true)
            break;
        Console.WriteLine($"Предыдущая игра не найдена (файлы с игрой не прочитаны).");
    }
} while (true);
//CurrentGame.Init();
//CurrentGame.LoadLastGame();

Console.WriteLine($"\n\nexit - команда для выхода из игры. [Сохранение]\n\"<RowChar_ColumnChar\" - координаты, куда стреляем \"4b\"\n");

Console.WriteLine($"Сейчас ходит - {CurrentGame.CurrentTurnPlayer}");
CurrentGame.PrintForCurrentPlayer();

bool IsFinishGame = false;
while (!IsFinishGame)
{
    Console.Write($"Введите координаты стрельбы: ");
    var InputData = Console.ReadLine();
    if (InputData == "exit")
    {
        CurrentGame.ExitAndSaveGame();
        return;
    }
    CurrentGame.Fire(InputData);
    CurrentGame.Check50ProcentGameCompleteForCurrentPlayer();
    if (CurrentGame.CheckEndGameForCurrentPlayer()) IsFinishGame = true;
}
if (IsFinishGame) Console.WriteLine($"Игрок {CurrentGame.CurrentTurnPlayer} - победил");
