namespace TheSnakeRemake
{
    public class GameSettings : IGameSettings
    {
        public GameSettings()
        {
            MapWidth = 30;
            MapHeight = 20;
        }

        public int MapWidth {  get; private set; }
        public int MapHeight { get; private set; }
        public int Speed { get; private set; }
        public bool Mode { get; private set; }
        public bool Menu { get; private set; } = true;

        public void ReductionSpeed()
        {
            Speed--;
        }

        public bool SetMenu(string? userInput)
        {
            Menu = userInput switch
            {
                "1" => true,
                _ => false,
            };
            return Menu;
        }

        public bool SetMode(string? userInput)
        {
            Mode = userInput switch
            {
                "0" => false,
                "1" => true,
                _ => true,
            };
            return Mode;
        }

        public int SetSpeed(string? userInput)
        {
            Speed = userInput switch
            {
                "0" => 190,
                "1" => 140,
                "2" => 92,
                _ => 140,
            };
            return Speed;
        }
    }
}
