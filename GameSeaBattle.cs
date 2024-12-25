public class GameSeaBattle
{
    public int TurnNumber = 1;
    public string[,] AreaPlayer1 { get; set; }
    public string[,] AreaPlayer2 { get; set; }

    public List<(int Row, int Column)> MovesPlayer1 = new List<(int Row, int Column)>();
    public List<(int Row, int Column)> MovesPlayer2 = new List<(int Row, int Column)>();

    public ETurnPlayer CurrentTurnPlayer = ETurnPlayer.Player1;

    public void Init()
    {
        var MatrixFirstPlayer = StaticHelper.GetMatrixFromFile("/Users/zebra/Desktop/Архив/laba3/SeaBattle/newgame_player_1.json");
        var MatrixSecondPlayer = StaticHelper.GetMatrixFromFile("/Users/zebra/Desktop/Архив/laba3/SeaBattle/newgame_player_2.json");
        AreaPlayer1 = MatrixFirstPlayer;
        AreaPlayer2 = MatrixSecondPlayer;
    }

    public void IncrementTurnNumber()
    {
        TurnNumber = TurnNumber + 1;
    }
    public void TooglePlayer()
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1) CurrentTurnPlayer = ETurnPlayer.Player2;
        else if (CurrentTurnPlayer == ETurnPlayer.Player2) CurrentTurnPlayer = ETurnPlayer.Player1;

        Console.WriteLine($"Сейчас ходит - {CurrentTurnPlayer}");
        PrintForCurrentPlayer();
    }
    public void PrintForPlayer1()
    {
        Console.WriteLine("Информация для игрока - 1");

        StaticHelper.PrintSeaArea(AreaPlayer1, MovesPlayer1, AreaPlayer2, MovesPlayer2);
    }
    public void PrintForPlayer2()
    {
        Console.WriteLine("Информация для игрока - 2");

        StaticHelper.PrintSeaArea(AreaPlayer2, MovesPlayer2, AreaPlayer1, MovesPlayer1);
    }
    public void PrintForCurrentPlayer()
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1) PrintForPlayer1();
        if (CurrentTurnPlayer == ETurnPlayer.Player2) PrintForPlayer2();
    }
    public void AddPointForCurrentPlayer((int Row, int Column) point)
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1) MovesPlayer1.Add(point);
        if (CurrentTurnPlayer == ETurnPlayer.Player2) MovesPlayer2.Add(point);
    }
    public bool Check50ProcentGameCompleteForCurrentPlayer()
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            return StaticHelper.Is50ProcentGameComplete(AreaPlayer2, MovesPlayer1);
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            return StaticHelper.Is50ProcentGameComplete(AreaPlayer1, MovesPlayer2);
        }
        return false;
    }
    public bool CheckEndGameForCurrentPlayer()
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            return StaticHelper.IsEndGame(AreaPlayer2, MovesPlayer1);
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            return StaticHelper.IsEndGame(AreaPlayer1, MovesPlayer2);
        }
        return false;
    }
    public bool IsNewPointForCuurrentPlayer((int Row, int Column) point)
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            return StaticHelper.IsNewPoint(MovesPlayer1, point);
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            return StaticHelper.IsNewPoint(MovesPlayer2, point);
        }
        return false;
    }
    public bool CheckPointHitForCurrentPlayer((int Row, int Column) point)
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            return StaticHelper.CheckPointHit(AreaPlayer2, point);
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            return StaticHelper.CheckPointHit(AreaPlayer1, point);
        }
        return false;
    }
    public bool CheckPointKillForCurrentPlayer((int Row, int Column) point)
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            return StaticHelper.CheckPointKill(AreaPlayer2, MovesPlayer1, point);
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            return StaticHelper.CheckPointKill(AreaPlayer1, MovesPlayer2, point);
        }
        return false;
    }
    public void AddPointsAroundFigure((int Row, int Column) point)
    {
        if (CurrentTurnPlayer == ETurnPlayer.Player1)
        {
            var Points = StaticHelper.GetPointArounFigure(AreaPlayer2, MovesPlayer1, point);
            var CloneMovesPlayer1 = new List<(int Row, int Column)>(MovesPlayer1);

            Points.ForEach(Point =>
            {
                var IsExists = CloneMovesPlayer1.Exists(Move => Move.Row == Point.Row && Move.Column == Point.Column);
                if (!IsExists) MovesPlayer1.Add(Point);
            });
        }
        if (CurrentTurnPlayer == ETurnPlayer.Player2)
        {
            var Points = StaticHelper.GetPointArounFigure(AreaPlayer1, MovesPlayer2, point);

            var CloneMovesPlayer2 = new List<(int Row, int Column)>(MovesPlayer2);

            Points.ForEach(Point =>
            {
                var IsExists = CloneMovesPlayer2.Exists(Move => Move.Row == Point.Row && Move.Column == Point.Column);
                if (!IsExists) MovesPlayer2.Add(Point);
            });
        }
    }

    public ETypeResponceAfterFire Fire(string inputText)
    {

        var (RowInt, ColumnInt, isPointOk) = StaticHelper.InfoAboutInputPoint(inputText);
        var point = (RowInt, ColumnInt);

        if (!isPointOk)
        {
            Console.WriteLine($"Введенные координаты {inputText} - не валидны");
            return ETypeResponceAfterFire.NotValidPoint;
        }

        var isNewPoint = IsNewPointForCuurrentPlayer(point);
        if (!isNewPoint)
        {
            Console.WriteLine($"Введенные координаты {inputText} - уже использовались");
            return ETypeResponceAfterFire.Used;
        }

        AddPointForCurrentPlayer(point);

        var IsHit = CheckPointHitForCurrentPlayer(point);

        if (!IsHit)
        {
            Console.WriteLine("Промах");
            TooglePlayer();
            return ETypeResponceAfterFire.NotHit;
        }

        var IsKill = CheckPointKillForCurrentPlayer(point);

        if (IsKill)
        {
            Console.WriteLine("Убит");
            AddPointsAroundFigure(point);
        }
        else
        {
            Console.WriteLine("Попал");
        }
        PrintForCurrentPlayer();
        IncrementTurnNumber();

        return IsKill ? ETypeResponceAfterFire.Kill : ETypeResponceAfterFire.Hit;
    }


    public void ExitAndSaveGame()
    {
        StaticHelper.SaveGameInFile(this);
    }
    public bool LoadLastGame()
    {
        return StaticHelper.LoadGameFromFile(this);
    }
}