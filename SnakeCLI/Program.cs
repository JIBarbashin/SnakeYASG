using SnakeCLI;
using SnakeGame;

internal class Program
{
    const int START_X = 2;
    const int START_Y = 1;

    private static void DrawHUD(int player)
    {
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (0 + (6 * player))); Console.WriteLine("--------------");
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (1 + (6 * player))); Console.WriteLine($"Player {player + 1}:");
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (2 + (6 * player))); Console.WriteLine($"{Game.Players[player].Name}");
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (4 + (6 * player))); Console.WriteLine($"Score: {Game.Players[player].Score}");
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (5 + (6 * player))); Console.WriteLine($"Speed: {Game.Players[player].Speed}");
        Console.SetCursorPosition(START_X + Game.Worlds[0].Width + 3, START_Y + (6 + (6 * player))); Console.WriteLine("--------------");
    }

    private static void DrawWorld(int world)
    {
        int dist = Game.Worlds[world].Width + 18;

        //Console.SetCursorPosition(world * dist, 0);
        for (int i = 0; i <= Game.Worlds[world].Width + 1; i++)
        {
            Console.SetCursorPosition(START_X + (i + (world * dist)), START_Y);
            Console.Write('#');

            Console.SetCursorPosition(START_X + (i + (world * dist)), START_Y + Game.Worlds[world].Height + 1);
            Console.Write('#');
        }

        for (int y = 0; y < Game.Worlds[world].Height; y++)
        {
            Console.SetCursorPosition(START_X + (world * dist), START_Y + y + 1);
            Console.Write('#');
            for (int x = 0; x < Game.Worlds[world].Width; x++)
            {
                Console.SetCursorPosition(START_X + (world * dist) + (x + 1), START_Y + y + 1);
                Console.Write(Game.Worlds[world].Map[y, x]);
            }
            Console.SetCursorPosition(START_X + (world * dist) + Game.Worlds[world].Width + 1, START_Y + y + 1);
            Console.Write('#');
            Console.WriteLine();
        }
    }

    private static void Main(string[] args)
    {
        Console.Title = "YASG Snake, v. 0.1";
        Console.WriteLine("YASG Snake, v. 0.1");
        Console.WriteLine("(c) 2025, Created by Jegor I. Barbashin");
        Console.WriteLine("Press any button to start...");
        Console.ReadKey();
        Console.WriteLine();

        Info info = new();

    PlayerCount:
        Console.Write($"Player count (1 to {Game.MAX_PLAYERS}): ");
        int ans1;
        if (!int.TryParse(Console.ReadLine(), out ans1))
        {
        goto PlayerCount;
        }
        if (ans1 > 0 & ans1 <= Game.MAX_PLAYERS)
        {
            info.PlayersCount = ans1;
            info.Names = new string[info.PlayersCount];
            info.Types = new PlayerType[info.PlayersCount];

            for (int i = 0; i < info.PlayersCount; i++)
            {
            PlayerName:
                Console.Write($"Name of Player {i+1}: ");
                string? ans2 = Console.ReadLine();
                if (ans2 != null & ans2.Length != 0)
                {
                    info.Names[i] = ans2;
                TypeChoose:
                    Console.Write("Type of Player (1 - human, 2 - bot): ");
                    int ans3;
                    if (!int.TryParse(Console.ReadLine(), out ans3))
                    {
                        goto TypeChoose;
                    }
                    if (ans3 == 1)
                        info.Types[i] = PlayerType.Human;
                    else if (ans3 == 2)
                        info.Types[i] = PlayerType.Bot;
                    else 
                        goto TypeChoose;
                }
                else
                {
                    goto PlayerName;
                }
            }
        StartSpeed:
            Console.Write($"Start speed ({Game.MIN_SPEED} to {Game.MAX_SPEED}): ");
            float ans4;
            if (!float.TryParse(Console.ReadLine(), out ans4))
            {
                goto StartSpeed;
            }
            if (ans4 >= Game.MIN_SPEED & ans4 <= Game.MAX_SPEED)
            {
                info.StartSpeed = ans4;
            }
            else goto StartSpeed;
        }
        else goto PlayerCount;

        Console.Clear();
        Console.CursorVisible = false;

        Game.Init();
        Game.Input = new Input();
        Game.Start(info);

        while (Game.IsRunning)
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Game.CurrentPlayers; i++)
            {
                Game.Players[i].Controller.Update();
                DrawWorld(i);
                DrawHUD(i);
            }
        }

        Console.SetCursorPosition(START_X, START_Y + Game.Worlds[0].Height + 3); Console.WriteLine("Game over!");
        if (Game.CurrentPlayers > 1)
        {
            Console.SetCursorPosition(START_X, START_Y + Game.Worlds[0].Height + 4); Console.WriteLine($"Winner is: {Game.GetWinner()}");
        }
        Thread.Sleep(3000);
        Console.SetCursorPosition(START_X, START_Y + Game.Worlds[0].Height + 5); Console.WriteLine("Press any button to exit...");
        Console.ReadKey();
        Console.Clear();
    }
}