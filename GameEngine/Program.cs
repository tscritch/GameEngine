using System;
namespace GameEngine
{
    public class ThisEngine : GameEngine
    {
        public ThisEngine()
        {
            m_sAppName = "My Demo";
        }

        public override bool OnUserCreate()
        {
            System.Console.Write("Does this work");
            return true;
        }

        public override bool OnUserUpdate(float fElapsedTime)
        {
            Fill(0, 0, ScreenWidth(), ScreenHeight());
            return true;
        }
    }

    public class Program
    {

        static void Main(string[] args) {

            ThisEngine gameEngine = new ThisEngine();
            if(gameEngine.ConstructConsole(256, 240, 4, 4) == 1) {
                gameEngine.Start();
            }
        }
    }
}
