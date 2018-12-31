using System;
using System.Threading;

namespace GameEngine
{
    public class GameEngine
    {
        public string m_sAppName;
        int m_nScreenWidth;
        int m_nScreenHeight;
        bool m_bAtomActive;

        int[] m_keyOldState = new int[256];
        int[] m_keyNewState = new int[256];
        bool[] m_mouseOldState = new bool[5];
        bool[] m_mouseNewState = new bool[5];

        struct sKeyState
        {
            public bool bPressed;
            public bool bReleased;
            public bool bHeld;
        }

        //sKeyState[] m_keys = new sKeyState[256];
        //sKeyState[] m_mouse = new sKeyState[5];


        char[] m_bufScreen;

        public GameEngine()
        {
            m_nScreenWidth = 80;
            m_nScreenHeight = 30;

            m_sAppName = "Default";
        }

        public virtual bool OnUserCreate() { return false; }
        public virtual bool OnUserUpdate(float fElapsedTime) { return false; }

        public int ConstructConsole(int width, int height, int fontw, int fonth)
        {
            m_nScreenWidth = width;
            m_nScreenHeight = height;

            Console.SetWindowSize(width, height);

            m_bufScreen = new char[m_nScreenWidth * m_nScreenHeight];

            return 1;
        }

        public int ScreenWidth()
        {
            return m_nScreenWidth;
        }

        public int ScreenHeight()
        {
            return m_nScreenHeight;
        }

        void Draw(int x, int y, char c = '\x2588')
        {
            if (x >= 0 && x < m_nScreenWidth && y >= 0 && y < m_nScreenHeight)
            {
                m_bufScreen[y * m_nScreenWidth + x] = c;
                //m_bufScreen[y * m_nScreenWidth + x].Attributes = col;
            }
        }

        public void Fill(int x1, int y1, int x2, int y2)
        {
            Clip(ref x1, ref y1);
            Clip(ref x2, ref y2);
            for (int x = x1; x < x2; x++)
                for (int y = y1; y < y2; y++)
                    Draw(x, y);
        }

        void Clip(ref int x, ref int y)
        {
            if (x < 0) x = 0;
            if (x >= m_nScreenWidth) x = m_nScreenWidth;
            if (y < 0) y = 0;
            if (y >= m_nScreenHeight) y = m_nScreenHeight;
        }

        public void Start() {
            m_bAtomActive = true;
            Thread t = new Thread(new ThreadStart(GameThread));
            t.Start();
        }

        void GameThread()
        {
            // Create user resources as part of this thread
            if (!OnUserCreate())
                m_bAtomActive = false;

            // Check if sound system should be enabled
            //if (m_bEnableSound)
            //{
            //    if (!CreateAudio())
            //    {
            //        m_bAtomActive = false; // Failed to create audio system         
            //        m_bEnableSound = false;
            //    }
            //}

            DateTime tp1 = DateTime.Now;
            DateTime tp2 = DateTime.Now;

            while (m_bAtomActive)
            {
                // Run as fast as possible
                while (m_bAtomActive)
                {
                    // Handle Timing
                    tp2 = DateTime.Now;
                    TimeSpan elapsedTime = tp2.Subtract(tp1);
                    tp1 = tp2;
                    float fElapsedTime = elapsedTime.Ticks;

                    // Handle Keyboard Input
                    //for (int i = 0; i < 256; i++)
                    //{
                    //    m_keyNewState[i] = GetAsyncKeyState(i);
                    //    //System.ConsoleKey.

                    //    m_keys[i].bPressed = false;
                    //    m_keys[i].bReleased = false;

                    //    if (m_keyNewState[i] != m_keyOldState[i])
                    //    {
                    //        if (m_keyNewState[i] & 0x8000)
                    //        {
                    //            m_keys[i].bPressed = !m_keys[i].bHeld;
                    //            m_keys[i].bHeld = true;
                    //        }
                    //        else
                    //        {
                    //            m_keys[i].bReleased = true;
                    //            m_keys[i].bHeld = false;
                    //        }
                    //    }

                    //    m_keyOldState[i] = m_keyNewState[i];
                    //}

                    //// Handle Mouse Input - Check for window events
                    //INPUT_RECORD inBuf[32];
                    //DWORD events = 0;
                    //GetNumberOfConsoleInputEvents(m_hConsoleIn, &events);
                    //if (events > 0)
                    //    ReadConsoleInput(m_hConsoleIn, inBuf, events, &events);

                    //// Handle events - we only care about mouse clicks and movement
                    //// for now
                    //for (DWORD i = 0; i < events; i++)
                    //{
                    //    switch (inBuf[i].EventType)
                    //    {
                    //        case FOCUS_EVENT:
                    //            {
                    //                m_bConsoleInFocus = inBuf[i].Event.FocusEvent.bSetFocus;
                    //            }
                    //            break;

                    //        case MOUSE_EVENT:
                    //            {
                    //                switch (inBuf[i].Event.MouseEvent.dwEventFlags)
                    //                {
                    //                    case MOUSE_MOVED:
                    //                        {
                    //                            m_mousePosX = inBuf[i].Event.MouseEvent.dwMousePosition.X;
                    //                            m_mousePosY = inBuf[i].Event.MouseEvent.dwMousePosition.Y;
                    //                        }
                    //                        break;

                    //                    case 0:
                    //                        {
                    //                            for (int m = 0; m < 5; m++)
                    //                                m_mouseNewState[m] = (inBuf[i].Event.MouseEvent.dwButtonState & (1 << m)) > 0;

                    //                        }
                    //                        break;

                    //                    default:
                    //                        break;
                    //                }
                    //            }
                    //            break;

                    //        default:
                    //            break;
                    //            // We don't care just at the moment
                    //    }
                    //}

                    //for (int m = 0; m < 5; m++)
                    //{
                    //    m_mouse[m].bPressed = false;
                    //    m_mouse[m].bReleased = false;

                    //    if (m_mouseNewState[m] != m_mouseOldState[m])
                    //    {
                    //        if (m_mouseNewState[m])
                    //        {
                    //            m_mouse[m].bPressed = true;
                    //            m_mouse[m].bHeld = true;
                    //        }
                    //        else
                    //        {
                    //            m_mouse[m].bReleased = true;
                    //            m_mouse[m].bHeld = false;
                    //        }
                    //    }

                    //    m_mouseOldState[m] = m_mouseNewState[m];
                    //}


                    // Handle Frame Update
                    if (!OnUserUpdate(fElapsedTime))
                        m_bAtomActive = false;

                    // Update Title & Present Screen Buffer
                    string s = $"Game Engine - {m_sAppName} - FPS: {(1.0f / fElapsedTime).ToString("0.00")}";
                    System.Console.Title = s;
                    System.Console.Write(m_bufScreen, 0, m_bufScreen.Length);
                }
            }

            //public void Draw(int x, int y, short c = 0x2588, short col = 0x000F)
            //{
            //    if (x >= 0 && x < m_nScreenWidth && y >= 0 && y < m_nScreenHeight)
            //    {
            //        m_bufScreen[y * m_nScreenWidth + x].Char.UnicodeChar = c;
            //        m_bufScreen[y * m_nScreenWidth + x].Attributes = col;
            //    }
            //}
        }
    }
}