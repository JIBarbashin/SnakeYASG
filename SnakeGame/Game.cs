namespace SnakeYASG
{
    public enum PlayerType
    {
        Human, Bot
    }

    public struct Player
    {
        public int ID;
        public string Name;
        public PlayerType Type;
        public bool IsAlive;
        public int Score;
        public float Speed;
        public IController Controller;

        public Player()
        {
            Reset();
        }

        public void Reset()
        {
            IsAlive = true;
            Score = 0;
        }

        public void IncreaseScore()
        {
            Score++;
        }
    }

    public struct Info
    {
        public int PlayersCount;
        public PlayerType[] Types;
        public string[] Names;
        public float StartSpeed;
    }

    public class Game
    {
        public delegate void GameIsOverHandle();
        static public event GameIsOverHandle OnGameIsOver;

        public const char SNAKE_HEAD_CHAR = 'O';
        public const char SNAKE_PART_CHAR = 'o';
        public const char APPLE_CHAR = '*';

        public const float MIN_SPEED = 1.0f;
        public const float MAX_SPEED = 20.0f;
        public const int MAX_PLAYERS = 2;
        static public int CurrentPlayers { get; set; }

        static public IInput Input { get; set; }

        static public World[] Worlds { get; private set; } = new World[MAX_PLAYERS];
        static public Player[] Players { get; private set; } = new Player[MAX_PLAYERS];

        static public bool IsRunning = false;

        static private Thread[] thread = new Thread[MAX_PLAYERS];
        static private ManualResetEvent mre = new ManualResetEvent(true);

        static public void Init()
        {
            
        }

        static public void IncreaseSpeed(int player, float value)
        {
            if (Players[player].Speed < MAX_SPEED)
            {
                Players[player].Speed += value;
            }
        }

        static public string GetWinner()
        {
            List<string> alives = new();
            if (!IsRunning)
            {
                for (int i = 0; i < CurrentPlayers; i++)
                {
                    if (Players[i].IsAlive)
                        alives.Add(Players[i].Name);
                }

                if (alives.Count == 1)
                {
                    return alives[0];
                }
                else
                {
                    return "Nothing";
                }
            }
            return "Nothing";
        }

        static public void Start(Info info)
        {
            if (!IsRunning)
            {
                IsRunning = true;
            }

            CurrentPlayers = info.PlayersCount;

            for (int i = 0; i < CurrentPlayers; i++)
            {
                Worlds[i] = new World();
                Players[i] = new Player();
                thread[i] = new Thread(Update);

                Worlds[i].Create();

                Players[i].ID = i;
                Players[i].Type = info.Types[i];
                Players[i].Name = info.Names[i];
                Players[i].Speed = info.StartSpeed;

                if (Players[i].Type == PlayerType.Human)
                {
                    Players[i].Controller = new HumanController(i, Worlds[i].Snake, Input);
                }
                else if (Players[i].Type == PlayerType.Bot)
                {
                    Players[i].Controller = new AiController(Worlds[i].Snake, Worlds[i].Apple, Worlds[i].Width, Worlds[i].Height);
                }

                thread[i].Start(i);
            }
        }

        static public void SetPause(bool value)
        {
            if (value)
                mre.Reset();
            else
                mre.Set();
        }

        static public void Update(object? obj)
        {
            int appleTarget = 5;
            while (IsRunning)
            {
                mre.WaitOne();
                if (Players[(int)obj].IsAlive)
                {
                    Worlds[(int)obj].Snake.Move();
                    Worlds[(int)obj].CollideSnake();

                    Players[(int)obj].Score = Worlds[(int)obj].Apple.Eaten;
                    if (Players[(int)obj].Score >= appleTarget)
                    {
                        IncreaseSpeed((int)obj, 0.5f);
                        appleTarget += 5;
                    }

                    Players[(int)obj].IsAlive = Worlds[(int)obj].Snake.IsAlive;
                }
                else
                {
                    Stop();
                }

                Worlds[(int)obj].Draw();
                Thread.Sleep((int)(300 / Players[(int)obj].Speed));
            }
        }

        static public void Stop()
        {
            OnGameIsOver?.Invoke();
            IsRunning = false;
        }
    }
}
