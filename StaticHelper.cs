using System.Data;
using System.Globalization;
using System.Text.Json;
public static class StaticHelper
{
    public static string[,] ConvertToMatrix(string[][] jaggedArray)
    {
        // Определяем размеры нового двумерного массива
        int rows = jaggedArray.Length;
        int cols = 0;

        // Находим максимальное количество столбцов
        for (int i = 0; i < rows; i++)
        {
            if (jaggedArray[i].Length > cols)
            {
                cols = jaggedArray[i].Length;
            }
        }

        // Создаем двумерный массив
        string[,] matrix = new string[rows, cols];

        // Копируем элементы из jaggedArray в matrix
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                matrix[i, j] = jaggedArray[i][j];
            }
        }

        return matrix;
    }
    public static string[][] ConvertToJaggedArray(string[,] multiDimensionalArray)
    {
        int rows = multiDimensionalArray.GetLength(0);
        int cols = multiDimensionalArray.GetLength(1);

        // Создаем jagged array с количеством строк, равным количеству строк в многомерном массиве
        string[][] jaggedArray = new string[rows][];

        for (int i = 0; i < rows; i++)
        {
            // Создаем массив для каждой строки
            jaggedArray[i] = new string[cols];

            for (int j = 0; j < cols; j++)
            {
                // Копируем элементы из многомерного массива в jagged array
                jaggedArray[i][j] = multiDimensionalArray[i, j];
            }
        }

        return jaggedArray;
    }




    public static int[][] ConvertToJaggedArray(List<(int Row, int Column)> list)
    {
        // Создаем зубчатый массив с количеством строк, равным maxRows
        int[][] jaggedArray = new int[list.Count][];
        int i = 0;
        list.ForEach(action =>
        {
            var temp = new int[2];
            temp[0] = action.Row;
            temp[1] = action.Column;
            jaggedArray[i] = temp;
            i++;
        });

        return jaggedArray;
    }
    public static List<(int Row, int Column)> ConvertToListMoves(int[][] array)
    {
        List<(int Row, int Column)> list = new List<(int Row, int Column)>();

        for (int i = 0; i < array.Length; i++)
        {
            list.Add((array[i][0], array[i][1]));
        }

        return list;
    }
    public static void PrintMatrices(string[,] matrix1, string[,] matrix2)
    {
        Console.WriteLine("");
        Console.WriteLine("    a b c d e f g h i j        a b c d e f g h i j");
        Console.WriteLine("    -----------------------------------------------");

        for (int i = 0; i < 10; i++)
        {
            // Печать строки с координатами
            Console.Write(i + " | ");

            // Печать первой матрицы
            for (int j = 0; j < 10; j++)
            {
                if (matrix1[i, j] == "1" || matrix1[i, j] == "X")
                {
                    Console.BackgroundColor = ConsoleColor.Blue; // Цветной квадрат
                }
                if (matrix1[i, j] == "0" || matrix1[i, j] == "x")
                {
                    Console.BackgroundColor = ConsoleColor.White; // Белый квадрат
                }

                if (matrix1[i, j] == "x" || matrix1[i, j] == "X")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("XX"); // Печатаем XX 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); // Печатаем пробелы для создания квадрата

                }
                Console.ResetColor(); // Сбрасываем цвет
            }


            Console.Write($" | {i} | "); // Разделитель между матрицами
            // Печать второй матрицы
            for (int j = 0; j < 10; j++)
            {
                if (matrix2[i, j] == "1" || matrix2[i, j] == "X")
                {
                    Console.BackgroundColor = ConsoleColor.Green; // Цветной квадрат
                }
                if (matrix2[i, j] == "0" || matrix2[i, j] == "x")
                {
                    Console.BackgroundColor = ConsoleColor.White; // Белый квадрат
                }

                if (matrix2[i, j] == "x" || matrix2[i, j] == "X")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("XX"); // Печатаем XX 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); // Печатаем пробелы для создания квадрата

                }
                Console.ResetColor(); // Сбрасываем цвет
            }

            Console.WriteLine(); // Переход на новую строку
        }
        Console.ResetColor(); // Сбрасываем цвет после завершения
        Console.WriteLine("");

    }
    public static void PrintSeaArea(
        string[,] myMatrix,
        List<(int Row, int Column)> myMoves,
        string[,] anotherMatrix,
        List<(int Row, int Column)> anotherMovies
        )
    {
        Console.WriteLine("");
        Console.WriteLine("    a b c d e f g h i j        a b c d e f g h i j");
        Console.WriteLine("    -----------------------------------------------");

        for (int row = 0; row < 10; row++)
        {
            // Печать строки с координатами
            Console.Write(row + " | ");

            // Печать первой матрицы
            for (int column = 0; column < 10; column++)
            {
                var isEnemyShootThisPoint = anotherMovies.Exists(list => row == list.Row && column == list.Column);
                if (myMatrix[row, column] == "1")
                {
                    Console.BackgroundColor = ConsoleColor.Blue; // Цветной квадрат
                }
                if (myMatrix[row, column] == "0")
                {
                    Console.BackgroundColor = ConsoleColor.White; // Белый квадрат
                }

                if (isEnemyShootThisPoint)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("XX"); // Печатаем XX 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); // Печатаем пробелы для создания квадрата

                }
                Console.ResetColor(); // Сбрасываем цвет
            }


            Console.Write($" | {row} | "); // Разделитель между матрицами
            // Печать второй матрицы
            for (int column = 0; column < 10; column++)
            {
                var isMeShootThisPoint = myMoves.Exists(list => row == list.Row && column == list.Column);
                Console.BackgroundColor = ConsoleColor.White; // Белый квадрат
                if (anotherMatrix[row, column] == "1" && isMeShootThisPoint)
                {
                    Console.BackgroundColor = ConsoleColor.Green; // Цветной квадрат
                }


                if (isMeShootThisPoint)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("XX"); // Печатаем XX 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("  "); // Печатаем пробелы для создания квадрата

                }
                Console.ResetColor(); // Сбрасываем цвет
            }

            Console.WriteLine(); // Переход на новую строку
        }
        Console.ResetColor(); // Сбрасываем цвет после завершения
        Console.WriteLine("");

    }

    public static string[,] GetMatrixFromFile(string fileName)
    {
        string JsonString = File.ReadAllText(fileName);
        JsonPlayerArea JsonArea = JsonSerializer.Deserialize<JsonPlayerArea>(JsonString)!;
        var matrix = ConvertToMatrix(JsonArea.Area);

        return matrix;
    }
    public static int TransformColumnLetterToNumber(string input)
    {
        if (input == "a") return 0;
        if (input == "b") return 1;
        if (input == "c") return 2;
        if (input == "d") return 3;
        if (input == "e") return 4;
        if (input == "f") return 5;
        if (input == "g") return 6;
        if (input == "h") return 7;
        if (input == "i") return 8;
        if (input == "j") return 9;
        return -1;
    }

    public static int GetCountHintsInArea(string[,] matrix, List<(int Row, int Column)> moves)
    {
        int HitСount = 0;
        for (int row = 0; row < 10; row++)
        {
            for (int column = 0; column < 10; column++)
            {
                if (matrix[row, column] == "1")
                {
                    var isFire = moves.Exists(point => point.Column == column && point.Row == row);
                    if (isFire) HitСount++;
                }
            }
        }
        return HitСount;
    }
    public static bool IsEndGame(string[,] matrix, List<(int Row, int Column)> moves)
    {
        var HitСount = GetCountHintsInArea(matrix, moves);
        return HitСount == 20;
    }
    public static bool Is50ProcentGameComplete(string[,] matrix, List<(int Row, int Column)> moves)
    {
        var HitСount = GetCountHintsInArea(matrix, moves);
        return HitСount > 10;
    }

    public static bool IsNewPoint(List<(int Row, int Column)> moves, (int Row, int Column) point)
    {
        var isExists = moves.Exists(Point => Point.Column == point.Column && Point.Row == point.Row);
        return !isExists;
    }
    public static bool CheckPointHit(string[,] matrix, (int Row, int Column) point)
    {
        return matrix[point.Row, point.Column] == "1";
    }

    private static void PointForFigure(string[,] matrix, List<(int Row, int Column)> figure, (int Row, int Column) point)
    {
        var isOk = point.Row >= 0 && point.Row < 10 && point.Column >= 0 && point.Column < 10;
        if (!isOk) return;

        if (matrix[point.Row, point.Column] == "1")
        {
            var isExists = figure.Exists(fig => fig.Row == point.Row && fig.Column == point.Column);
            if (!isExists) figure.Add((point.Row, point.Column));
        }

    }
    static List<(int Row, int Column)> GetFigure(string[,] matrix, (int Row, int Column) point)
    {
        List<(int Row, int Column)> Figure = new List<(int Row, int Column)> { (point.Row, point.Column) };
        for (int i = 0; i < 6; i++)
        {
            List<(int Row, int Column)> FigureClone = new List<(int Row, int Column)>(Figure);
            FigureClone.ForEach(figurePoint =>
            {

                var (TopRow, TopColumn) = (figurePoint.Row - 1, figurePoint.Column);
                var (BottomRow, BottomColumn) = (figurePoint.Row + 1, figurePoint.Column);
                var (RightRow, RightColumn) = (figurePoint.Row, figurePoint.Column + 1);
                var (LeftRow, LeftColumn) = (figurePoint.Row, figurePoint.Column - 1);

                PointForFigure(matrix, Figure, (TopRow, TopColumn));
                PointForFigure(matrix, Figure, (BottomRow, BottomColumn));
                PointForFigure(matrix, Figure, (RightRow, RightColumn));
                PointForFigure(matrix, Figure, (LeftRow, LeftColumn));

            });
        }

        return Figure;
    }
    public static bool CheckPointKill(string[,] matrix, List<(int Row, int Column)> moves, (int Row, int Column) point)
    {
        List<(int Row, int Column)> Figure = GetFigure(matrix, point);

        bool IsKill = true;

        Figure.ForEach(figure =>
        {
            var isExists = moves.Exists(move => move.Row == figure.Row && move.Column == figure.Column);
            IsKill = IsKill && isExists;
        });

        return IsKill;
    }
    public static List<(int Row, int Column)> GetPointArounFigure(string[,] matrix, List<(int Row, int Column)> moves, (int Row, int Column) point)
    {
        List<(int Row, int Column)> Points = new List<(int Row, int Column)>();
        List<(int Row, int Column)> Figure = GetFigure(matrix, point);

        Figure.ForEach(figure =>
        {
            Points.Add(figure);

            var p1 = (figure.Row - 1, figure.Column - 1);
            var p2 = (figure.Row, figure.Column - 1);
            var p3 = (figure.Row + 1, figure.Column - 1);

            var p4 = (figure.Row - 1, figure.Column);
            var p5 = (figure.Row, figure.Column);
            var p6 = (figure.Row + 1, figure.Column);

            var p7 = (figure.Row - 1, figure.Column + 1);
            var p8 = (figure.Row, figure.Column + 1);
            var p9 = (figure.Row + 1, figure.Column + 1);

            if (p1.Item1 >= 0 && p1.Item1 < 10 && p1.Item2 >= 0 && p1.Item2 < 10 &&
                !Points.Exists(point => point.Row == p1.Item1 && point.Column == p1.Item2)
            ) Points.Add(p1);

            if (p2.Item1 >= 0 && p2.Item1 < 10 && p2.Item2 >= 0 && p2.Item2 < 10 &&
                !Points.Exists(point => point.Row == p2.Item1 && point.Column == p2.Item2)
            ) Points.Add(p2);

            if (p3.Item1 >= 0 && p3.Item1 < 10 && p3.Item2 >= 0 && p3.Item2 < 10 &&
                !Points.Exists(point => point.Row == p3.Item1 && point.Column == p3.Item2)
            ) Points.Add(p3);

            if (p4.Item1 >= 0 && p4.Item1 < 10 && p4.Item2 >= 0 && p4.Item2 < 10 &&
                !Points.Exists(point => point.Row == p4.Item1 && point.Column == p4.Item2)
            ) Points.Add(p4);

            if (p5.Item1 >= 0 && p5.Item1 < 10 && p5.Item2 >= 0 && p5.Item2 < 10 &&
                !Points.Exists(point => point.Row == p5.Item1 && point.Column == p5.Item2)
            ) Points.Add(p5);

            if (p6.Item1 >= 0 && p6.Item1 < 10 && p6.Item2 >= 0 && p6.Item2 < 10 &&
                !Points.Exists(point => point.Row == p6.Item1 && point.Column == p6.Item2)
            ) Points.Add(p6);

            if (p7.Item1 >= 0 && p7.Item1 < 10 && p7.Item2 >= 0 && p7.Item2 < 10 &&
                !Points.Exists(point => point.Row == p7.Item1 && point.Column == p7.Item2)
            ) Points.Add(p7);

            if (p8.Item1 >= 0 && p8.Item1 < 10 && p8.Item2 >= 0 && p8.Item2 < 10 &&
                !Points.Exists(point => point.Row == p8.Item1 && point.Column == p8.Item2)
            ) Points.Add(p8);

            if (p9.Item1 >= 0 && p9.Item1 < 10 && p9.Item2 >= 0 && p9.Item2 < 10 &&
                !Points.Exists(point => point.Row == p9.Item1 && point.Column == p9.Item2)
            ) Points.Add(p9);
        });


        return Points;
    }
    public static (int, int, bool) InfoAboutInputPoint(string input)
    {
        if (input.Length != 2) return (0, 0, false);
        var Row = input[0];
        var isRowInt = int.TryParse(Row.ToString(), out _);
        var RowInt = isRowInt ? int.Parse(Row.ToString()) : 0;
        var isRowOk = isRowInt && RowInt >= 0 && RowInt <= 9;


        var Column = input[1];
        var isColumnInt = int.TryParse(Column.ToString(), out _);


        var ColumnInt = isColumnInt ? int.Parse(Column.ToString()) : TransformColumnLetterToNumber(Column.ToString());

        var isColumnOk = ColumnInt != -1 && ColumnInt >= 0 && ColumnInt <= 9;
        var isPointOk = isColumnOk && isRowOk;

        return (RowInt, ColumnInt, isPointOk);
    }

    public static void SaveGameInFile(GameSeaBattle Game)
    {
        JsonSaveGame JsonSaveGame = new JsonSaveGame();
        JsonSaveGame.TurnNumber = Game.TurnNumber;
        JsonSaveGame.CurrentTurnPlayer = Game.CurrentTurnPlayer;

        JsonSavePlayer JsonSavePlayer1 = new JsonSavePlayer();
        JsonSavePlayer1.Area = ConvertToJaggedArray(Game.AreaPlayer1);
        JsonSavePlayer1.MovesPlayer = ConvertToJaggedArray(Game.MovesPlayer1);

        JsonSavePlayer JsonSavePlayer2 = new JsonSavePlayer();
        JsonSavePlayer2.Area = ConvertToJaggedArray(Game.AreaPlayer2);
        JsonSavePlayer2.MovesPlayer = ConvertToJaggedArray(Game.MovesPlayer2);


        string JsonSaveGameString = JsonSerializer.Serialize(JsonSaveGame);
        File.WriteAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame.json", JsonSaveGameString);

        string JsonSavePlayer1String = JsonSerializer.Serialize(JsonSavePlayer1);
        File.WriteAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame_player_1.json", JsonSavePlayer1String);
        string JsonSavePlayer2String = JsonSerializer.Serialize(JsonSavePlayer2);
        File.WriteAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame_player_2.json", JsonSavePlayer2String);
    }

    public static bool LoadGameFromFile(GameSeaBattle Game)
    {
        if (File.Exists("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame.json") != true ||
            File.Exists("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame_player_2.json") != true)
            return false;

        string Lastgame_JsonString = File.ReadAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame.json");
        JsonSaveGame Lastgame = JsonSerializer.Deserialize<JsonSaveGame>(Lastgame_JsonString)!;

        Game.CurrentTurnPlayer = Lastgame.CurrentTurnPlayer;
        Game.TurnNumber = Lastgame.TurnNumber;

        string SavePlayer1JsonString = File.ReadAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame_player_1.json");
        JsonSavePlayer SavePlayer1 = JsonSerializer.Deserialize<JsonSavePlayer>(SavePlayer1JsonString)!;
        Game.MovesPlayer1 = ConvertToListMoves(SavePlayer1.MovesPlayer);
        Game.AreaPlayer1 = ConvertToMatrix(SavePlayer1.Area);

        string SavePlayer2JsonString = File.ReadAllText("/Users/zebra/Desktop/Архив/laba3/SeaBattle/lastgame_player_2.json");
        JsonSavePlayer SavePlayer2 = JsonSerializer.Deserialize<JsonSavePlayer>(SavePlayer2JsonString)!;
        Game.MovesPlayer2 = ConvertToListMoves(SavePlayer2.MovesPlayer);
        Game.AreaPlayer2 = ConvertToMatrix(SavePlayer2.Area);

        return true;
    }
}