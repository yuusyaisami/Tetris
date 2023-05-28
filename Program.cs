using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;

namespace Tetris
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        public struct SCENE
        {
            public static int GameScene;
            public static int MenuScene;
            public static int GameOverScene;
            public static int PoseScene;
        }
        static int[][] map = 
        {
            new int[16]{1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            new int[16]{1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
        };

        static int Timer, GameTimer,MainTimer,ClearLineValue, Score,FirstMinoCount,NowMinoIndex, Finishmino,NowMinoDirection,NextMinoIndex;
        static int[] FirstMino = new int[7];
        struct Player
        {
            public int X;
            public int Y;
        }
        static Player player = new Player();
        static void Main(string[] args)
        {
            
            init();
            for(; ; )
            {
                update();
                controll();
                //FPS60
                Thread.Sleep(16);
                MainTimer++;
                draw();
            }
        }
        //イニシャライズの処理
        static void init()
        {
            SCENE.GameScene = 0;
            SCENE.MenuScene = 1;
            Timer = -1;
            GameTimer = 0;
            MainTimer = 0;
            Score = 0;
            FirstMinoCount = 0;
            NowMinoIndex = 0;
            Finishmino = 0;
            NowMinoDirection = 0;
            NextMinoIndex = 0;
            resetgame();
        }
        static void resetgame()
        {
            SCENE.GameScene = 0;
            SCENE.MenuScene = 1;
            Timer = -1;
            GameTimer = 0;
            player.X = 0;
            player.Y = 0;
            FirstMinoCount = 0;
            NowMinoDirection = 0;
            NextMinoIndex = 0;
            Random r = new Random();
            int s;
            s = r.Next();
            for (int i = 0; i < 7; i++)
            {
                FirstMino[i] = (s + i) % 7;
            }
            NowMinoIndex = 0;
            Finishmino = 0;
        }
        //ゲームの処理
        static void update()
        {
            if (SCENE.GameScene == 1)
            {
                if (Timer == 20 || Finishmino == 1)
                {
                    Finishmino = 0;
                    if (CheckBlockOnPlayer())
                    {

                        //プレイヤーブロックの上に壁があったら
                        if (CheckBlockUnderPlayer())
                        {
                            GameOver();
                        }
                        else
                        {
                            PlayerToBlock();
                            //lineがそろっているか
                            LineAlignedValue();
                            Score += ClearLineValue;
                            player.X = 4; player.Y = 4;
                            //新しくブロックを生成する
                            CreateNewPlayer(FirstMino[0]);
                            arrayShift();
                            Random r = new Random();
                            FirstMino[6] = r.Next() % 7;
                        
                        }
                    }
                    else
                    {
                        ShiftDown();
                    }
                }
                if(Timer == 20)
                {
                    Timer = 0;
                }
                //ゲーム開始時の初期設定
                if(Timer == -1)
                {
                    CreateNewPlayer(FirstMino[0]);
                    FirstMinoCount++;
                    player.X = 4; player.Y = 4;
                    Score = 0;
                }
                Timer++;
                GameTimer++;
            }
            else if(SCENE.MenuScene == 1) 
            {

            }
        }
        static void GameOver()
        {
            resetgame();
            for(int i  = 0; i < 24; i++)
            {
                for(int j = 0; j < 16; j++)
                {
                    if (map[i][j] != 1)
                    {
                        map[i][j] = 0;
                    }
                }
            }
        }
        static void arrayShift()
        {
            for( int i = 0;i < 6; i++)
            {
                FirstMino[i] = FirstMino[i + 1];
            }

        }
        //ゲームの表示
        static void draw()
        {
            string linestring = "";
            if (SCENE.GameScene == 1)
            {
                Console.Clear();
                for (int i = 0; i < 24; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if (map[i][j] == 0)
                        {
                            linestring += "  ";
                        }
                        else if (map[i][j] == 1)
                        {
                            linestring += "##";
                        }
                        else if ((map[i][j] == 2))
                        {
                            linestring += "[]";
                        }
                        else if (((map[i][j] == 3)))
                        {
                            linestring += "ZZ";
                        }

                    }
                    if (i == 2)
                    {
                        if (FirstMino[0] == 1)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0]== 2)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[0] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0] == 5)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[0] == 6)
                        {
                            linestring += "    []";
                        }

                    }
                    if (i == 3)
                    {
                        if (FirstMino[0] == 0)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[0] == 1)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[0] == 2)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[0] == 3)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0] == 5)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[0] == 6)
                        {
                            linestring += "    []";
                        }
                    }
                    if (i == 4)
                    {
                        if (FirstMino[0] == 0)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[0] == 1)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[0] == 2)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0] == 3)
                        {
                            linestring += "  [][][]";
                        }
                        if (FirstMino[0] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[0] == 5)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[0] == 6)
                        {
                            linestring += "  [][]";
                        }

                    }
                    if (i == 5)
                    {
                        if (FirstMino[0] == 4)
                        {
                            linestring += "    []";
                        }
                    }
                    if (i == 6)
                    {
                        if (FirstMino[1] == 1)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1]== 2)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[1] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1] == 5)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[1] == 6)
                        {
                            linestring += "    []";
                        }

                    }
                    if (i == 7)
                    {
                        if (FirstMino[1] == 0)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[1] == 1)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[1] == 2)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[1] == 3)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1] == 5)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[1] == 6)
                        {
                            linestring += "    []";
                        }
                    }
                    if (i == 8)
                    {
                        if (FirstMino[1] == 0)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[1] == 1)
                        {
                            linestring += "  []";
                        }
                        if (FirstMino[1] == 2)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1] == 3)
                        {
                            linestring += "  [][][]";
                        }
                        if (FirstMino[1] == 4)
                        {
                            linestring += "    []";
                        }
                        if (FirstMino[1] == 5)
                        {
                            linestring += "  [][]";
                        }
                        if (FirstMino[1] == 6)
                        {
                            linestring += "  [][]";
                        }

                    }
                    if (i == 9)
                    {
                        if (FirstMino[1] == 4)
                        {
                            linestring += "    []";
                        }
                    }
                    if (i == 12)
                    {
                        linestring += "    Score : " + Score;
                    }
                    else if (i == 14)
                    {
                        linestring += "     Time : " + GameTimer / 80 + "s";
                    }
                    else if (i == 1)
                    {
                        linestring += " NextMino    ";
                    }
                    Console.WriteLine(linestring);
                    linestring = "";
                }
            }
            if (SCENE.MenuScene == 1)
            {
                Console.Clear();
                linestring = "Score " + Score;
                Console.WriteLine(linestring);
                linestring = "Press enter to start the game";
                Console.WriteLine(linestring);
            }
        }

        static int Keyconfig = 0;
        static void controll()
        {

            if (SCENE.GameScene == 1)
            {
                if ((GetAsyncKeyState((int)ConsoleKey.W) & 0x8000) != 0)
                {

                        Finishmino = 1;
                }
                else if ((GetAsyncKeyState((int)ConsoleKey.A) & 0x8000) != 0)
                {
                    if (Keyconfig != 2)
                    {
                        ShiftLeft();
                    }
                    Keyconfig = 2;
                }
                else if ((GetAsyncKeyState((int)ConsoleKey.D) & 0x8000) != 0)
                {
                    if (Keyconfig != 3)
                    {
                        ShiftRight();
                    }
                    Keyconfig = 3;
                }
                else if ((GetAsyncKeyState((int)ConsoleKey.S) & 0x8000) != 0)
                {
                    Timer += 1;
                }
                else if ((GetAsyncKeyState((int)ConsoleKey.Spacebar) & 0x8000) != 0)
                {
                    if (Keyconfig != 4)
                    {
                        NowMinoDirection++;
                        if (NowMinoDirection == 4)
                        {
                            NowMinoDirection = 0;
                        }
                        RotateRight();
                    }
                    Keyconfig = 4;
                }
                else
                {
                    Keyconfig = 0;
                }
            }
            if (SCENE.MenuScene == 1)
            {
                if ((GetAsyncKeyState((int)ConsoleKey.Enter) & 0x8000) != 0)
                {
                    if (Keyconfig != 4)
                    {
                        SCENE.GameScene = 1;
                        SCENE.MenuScene = 0;
                    }
                    Keyconfig = 4;
                }
            }

        }
        static void RotateRight()
        {
            int Height1 = 0, Height2 = 0, Height3 = 0, Height4 = 0;
            int Width1 = 0, Width2 = 0, Width3 = 0, Width4 = 0;
            if(NowMinoIndex == 1)
            {
                if (NowMinoDirection == 0)
                {
                    Height4 = -2;
                }
                if (NowMinoDirection == 1)
                {
                    Width3 = 2;
                }
                if (NowMinoDirection == 2)
                {
                    Height4 = -2;
                }
                if (NowMinoDirection == 3)
                {
                    Width3 = 2;
                }
            }
            else if(NowMinoIndex == 2)
            {
                if (NowMinoDirection == 0)
                {
                    Height3 = -2;
                }
                if (NowMinoDirection == 1)
                {
                    Width1 = 2;
                }
                if (NowMinoDirection == 2)
                {
                    Height3 = -2;
                }
                if (NowMinoDirection == 3)
                {
                    Width1 = 2;
                }
            }
            else if(NowMinoIndex == 3)
            {
                if(NowMinoDirection == 0)
                {
                    Height2 = 1; Width2 =  -2;
                }
                if (NowMinoDirection == 1)
                {
                    Height2 = 2; Width2 =  -1;
                }
                if (NowMinoDirection == 2)
                {
                    Height2 = 2; Width2 = -1;
                    Height1 = 1; Width1 = -1;
                }
                if (NowMinoDirection == 3)
                {
                    Height2 = 1; Width2 =  -2;
                    Height4 = 1; Width4 = -1;
                }
            }
            else if(NowMinoIndex == 4)
            {
                if(NowMinoDirection == 0)
                {
                    Height1 =  -1; Width1 = 1;
                    Height3 = 1; Width3 = 1;
                }
                if(NowMinoDirection == 1)
                {
                    Height1 = 1; Width1 = -1;
                    Height2 = 1; Width2 =  1;
                }
                if (NowMinoDirection == 2)
                {
                    Height2 = -1; Width2 = -1;
                    Height4 =  1; Width4 = -1;
                }
                if (NowMinoDirection == 3)
                {
                    Height3 = -1; Width3 = -1;
                    Height4 = -1; Width4 =  1;
                }
            }
            else if(NowMinoIndex == 5)
            {
                if(NowMinoDirection == 0)
                {
                    Height2 = -1; Width2 = -1;
                }
                if(NowMinoDirection == 1)
                {
                    Height4 = -1; Width4 = 1;
                }
                if(NowMinoDirection == 2)
                {
                    Height3 = 1; Width3 = 1;
                }
                if(NowMinoDirection == 3)
                {
                    Height1 = 1; Width1 = -1;
                }
            }
            else if(NowMinoIndex == 6)
            {
                if(NowMinoDirection == 0)
                {
                    Height1 = -1; Width1 = 1;
                }
                if(NowMinoDirection == 1)
                {
                    Height2 = 1; Width2 = 1;
                }
                if(NowMinoDirection == 2)
                {
                    Height4 = 1;Width4 = -1;
                }
                if(NowMinoDirection == 3)
                {
                    Height3 = -1; Width3 = -1;
                }
            }
            int px = player.X, py = player.Y;
            int pcount = 0;
            
            for (; ; )
             {
                if (map[player.Y + Height1 - 3][player.X + Width1] != 1 && map[player.Y + Height2 - 3][player.X + Width2 + 1] != 1 && map[player.Y + Height3 - 2][player.X + Width3] != 1 && map[player.Y + Height4 - 2][player.X + Width4 + 1] != 1 &&
                    map[player.Y + Height1 - 3][player.X + Width1] != 3 && map[player.Y + Height2 - 3][player.X + Width2 + 1] != 3 && map[player.Y + Height3 - 2][player.X + Width3] != 3 && map[player.Y + Height4 - 2][player.X + Width4 + 1] != 3)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = 0; j < 11; j++)
                        {
                            if (map[i][j] == 2)
                            {
                                map[i][j] = 0;
                            }
                        }
                    }
                    map[player.Y + Height1 - 3][player.X + Width1] = 2;
                    map[player.Y + Height2 - 3][player.X + Width2 + 1] = 2;
                    map[player.Y + Height3 - 2][player.X + Width3] = 2;
                    map[player.Y + Height4 - 2][player.X + Width4 + 1] = 2;
                    break;
                }
                if (px > 4)
                {
                    player.X -= 1;
                    pcount++;
                }
                else
                {
                    player.X += 1;
                }
                if (pcount == 3)
                {
                    player.X = px;
                    player.Y = py;

                    break;
                }
            }
        }
        static void CreateNewPlayer(int Index)
        {
            NowMinoDirection = 0;
            if (Index == 0)
            {//OMino
                map[1][4] = map[1][5] = 2; //H1 W1, H2 W2 init px 4 py 4
                map[2][4] = map[2][5] = 2; //H3 W3, H4 W4
                NowMinoIndex = 0;
            }
            if (Index == 1)
            { //Zmino f
                            map[1][5] = 2;
                map[2][4] = map[2][5] = 2;
                map[3][4] = 2;
                NowMinoIndex = 1;
            }
            if (Index == 2)
            {//zmino b
                map[1][4] = 2;
                map[2][4] = map[2][5] = 2;
                            map[3][5] = 2;
                NowMinoIndex = 2;
            }
            if (Index == 3)
            { //Tmino
                            map[1][4] = 2;
                map[2][3] = map[2][4] = map[2][5] = 2;
                NowMinoIndex = 3;
            }
            if (Index == 4)
            {//Imino
                map[1][5] = map[2][5] = map[3][5] = map[4][5] = 2;
                NowMinoIndex = 4;
            }
            if (Index == 5)
            {//L
                map[1][4] = 2;
                map[2][4] = 2;
                map[3][4] = map[3][5] = 2;
                NowMinoIndex = 5;
            }
            if (Index == 6)
            {//J
                            map[1][5] = 2;
                            map[2][5] = 2;
                map[3][4] = map[3][5] = 2;
                NowMinoIndex = 6;
            }
        }
        /// <summary>
        /// true : 存在する
        /// </summary>
        /// <returns></returns>
        static bool CheckBlockOnPlayer()
        {
            for(int i = 0; i < 24; i++)
            {
                for(int j = 0; j < 16; j++)
                {
                    if (map[i][j] == 2)
                    {
                        if (map[i + 1][j] == 3 || map[i + 1][j] == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static bool CheckBlockUnderPlayer()
        {
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (map[i][j] == 2)
                    {
                        if (map[i - 1][j] == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static void PlayerToBlock()
        {
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (map[i][j] == 2)
                    {
                        map[i][j] = 3;
                    }
                }
            }
        }
        static void LineAlignedValue()
        {
            PlayerToBlock();
            ClearLineValue = 0;
            int line = 0,ALY = 0;
            for(int i = 1; i < 24;i++)
            {
                for(int j = 1; j < 11; j++)
                {
                    if (map[i][j] == 3 )
                    {
                        line++;
                    }
                    if(line == 10)
                    {
                        ClearLineValue++;
                        ALY = i;
                    }
                }
                line = 0;
            }
            for (int a = ClearLineValue; a > 0; a--)
            {
                for (int i = ALY; i > 1; i--)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        map[i][j] = map[i - 1][j];
                    }
                }
            }
        }
        static void ShiftDown()
        {
            for (int i = 23; i > 0; i--)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (map[i][j] == 2)
                    {
                        map[i + 1][j] = map[i][j];
                        map[i][j] = 0;
                    }
                }
            }
            player.Y++;
        }
        static void ShiftRight()
        {
            if (collisionRight() == false)
            {
                for (int i = 23; i > 0; i--)
                {
                    for (int j = 10; j > 0; j--)
                    {
                        if (map[i][j] == 2)
                        {
                            map[i][j + 1] = map[i][j];
                            map[i][j] = 0;
                         
                        }
                    }
                }
                player.X++;
            }
        }
        static void ShiftLeft()
        {
            if (collisionLeft() == false)
            {
                for (int i = 23; i > 0; i--)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (map[i][j] == 2)
                        {
                            map[i][j - 1] = map[i][j];
                            map[i][j] = 0;
                        }
                    }
                }
                player.X--;
            }
        }
        static bool collisionLeft()
        {
            for (int i = 23; i > 0; i--)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (map[i][j] == 2)
                    {
                        if(j == 1 || map[i][j - 1] == 3)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static bool collisionRight()
        {
            for (int i = 23; i > 0; i--)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (map[i][j] == 2)
                    {
                        if (j == 10 || map[i][j + 1] == 3)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
