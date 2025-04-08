using SnakeGame;
using SnakeWin;
using System.Collections.Generic;
using ZeroElectric.Vinculum;

internal class Program
{
    static int screenWidth = 1280;
    static int screenHeight = 720;

    static bool showWinnerBox = false;
    private static void ShowWinner()
    {
        showWinnerBox = true;
        Game.OnGameIsOver -= ShowWinner;
    }

    private static void DrawWorld(int world)
    {
        int fieldWidth = screenWidth / 2;
        int fieldHeight = screenHeight - 75;

        int cellWidth = fieldWidth / Game.Worlds[world].Width;
        int cellHeight = fieldHeight / Game.Worlds[world].Height;

        int startX = world * fieldWidth;
        int startY = screenHeight - fieldHeight;

        if (world == 0)
        {
            Raylib.DrawRectangle(startX, startY, fieldWidth, fieldHeight, Raylib.BLUE);
        }
        if (world == 1)
        {
            Raylib.DrawRectangle(startX, startY, fieldWidth, fieldHeight, Raylib.GRAY);
        }

        for (int y = 0; y < Game.Worlds[world].Height; y++)
        {
            //Raylib.DrawLine(startX, y * cellWidth, startX + (fieldWidth), y * cellWidth, Raylib.BLACK);
            for (int x = 0; x < Game.Worlds[world].Width; x++)
            {
                //Raylib.DrawLine(startX + (x * cellWidth), 0, startX + (x * cellWidth), fieldHeight, Raylib.BLACK);

                switch (Game.Worlds[world].Map[y, x])
                {
                    case Game.APPLE_CHAR:
                        Raylib.DrawCircle(startX + (x * cellWidth + (cellWidth / 2)), startY + (y * cellHeight + (cellHeight / 2)), cellWidth / 2, Raylib.RED);
                        break;
                    case Game.SNAKE_PART_CHAR:
                        Raylib.DrawCircle(startX + (x * cellWidth + (cellWidth / 2)), startY + (y * cellHeight + (cellHeight / 2)), cellWidth / 2, Raylib.GREEN);
                        break;
                    case Game.SNAKE_HEAD_CHAR:
                        Raylib.DrawCircle(startX + (x * cellWidth + (cellWidth / 2)), startY + (y * cellHeight + (cellHeight / 2)), cellWidth / 2, Raylib.DARKGREEN);
                        break;
                }
            }
        }
    }

    static bool showMessageBox = false;
    private static void DrawHUD()
    {
        int fieldWidth = screenWidth / 2;
        int fieldHeight = screenHeight - 75;

        int buttonWidth = 128;
        int buttonHeight = screenHeight - fieldHeight;

        int startY = screenHeight - fieldHeight;
        int startX = buttonWidth;

        for (int i = 0; i < Game.CurrentPlayers; i++)
        {
            int cellWidth = fieldWidth / Game.Worlds[i].Width;
            int cellHeight = fieldHeight / Game.Worlds[i].Height;

            if (i > 0)
            {
                startX = (i * fieldWidth) + buttonWidth * 2;
            }


            Raylib.DrawText(
                $"{Game.Players[i].Name}: {Game.Players[i].Score} / {Game.Players[i].Speed}",
                startX,
                startY / 4,
                36,
                Raylib.WHITE
            );
        }
        
        if (RayGui.GuiButton(new(fieldWidth - buttonWidth / 2 - buttonWidth, 0, buttonWidth, buttonHeight), "About") == 1)
        {
            showMessageBox = true;
            Game.SetPause(true);
        }

        if (showMessageBox)
        {
            int res = RayGui.GuiMessageBox(new(screenWidth / 2 - (250 / 2), screenHeight / 2 - (100 / 2), 250, 100), "About", "DASdsdsds", "Ok");
            if (res >= 0)
            {
                showMessageBox = false;
                Game.SetPause(false);
            }
        }

        if (showWinnerBox)
        {
            int res = RayGui.GuiMessageBox(new(screenWidth / 2 - (250 / 2), screenHeight / 2 - (100 / 2), 250, 100), "Game over", $"{Game.GetWinner()} is winner!", "Ok");
            if (res >= 0)
            {
                showWinnerBox = false;
            }
        }

        string gameBtnTxt;
        if (Game.IsRunning)
        {
            gameBtnTxt = "Stop";
        }
        else
        {
            gameBtnTxt = "Start";
        }

        if (RayGui.GuiButton(new(fieldWidth - buttonWidth / 2, 0, buttonWidth, buttonHeight), gameBtnTxt) == 1)
        {
            if (Game.IsRunning)
            {
                Game.Stop();
            }
            else
            {
                Game.Stop();
                Info info = new();
                info.PlayersCount = 2;
                info.StartSpeed = 2.0f;
                info.Names = new string[] { "Player", "Computer" };
                info.Types = new PlayerType[] { PlayerType.Human, PlayerType.Bot };

                Game.Start(info);
                Game.OnGameIsOver += ShowWinner;
            }
        }
        if (RayGui.GuiButton(new(fieldWidth - buttonWidth / 2 + buttonWidth, 0, buttonWidth, buttonHeight), "Exit") == 1)
        {
            //Game.Stop();
            //Raylib.C();
        }
    }

    private static void Main(string[] args)
    {   
        
        Raylib.InitWindow(screenWidth, screenHeight, "Snake");
        Raylib.SetTargetFPS(60);
        //Raylib.SetExitKey(KeyboardKey.KEY_NULL);

        Info info = new();
        info.PlayersCount = 2;
        info.StartSpeed = 2.0f;
        info.Names = new string[] { "Player", "Computer" };
        info.Types = new PlayerType[] { PlayerType.Human, PlayerType.Bot };

        Game.Init();
        Game.Input = new Input();    

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.RED);

            for (int i = 0; i < Game.CurrentPlayers; i++)
            {
                Game.Players[i].Controller.Update();
                DrawWorld(i);
            }

            DrawHUD();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
        Game.Stop();
    }
}