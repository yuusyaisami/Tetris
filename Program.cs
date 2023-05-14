using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Program
    {
        static int[][] Map = {  new int[]{ 999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999,   0,   0,   0,   0,   0,   0,   0,   0,   0, 999},
                                new int[]{ 999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999}};
        static int StartGame;
        static int CreateBlock;
        static void Main(string[] args)
        {
            initializ();
            for (; ; )
            {
                if (CreateBlock == 0)
                {
                    CreateBlock = 1;
                    CreateNewBlock();
                }
            }
            Console.Clear();
        }
        public static void initializ()
        {
            
        }
        public static void FallBlock()
        {

        }
        public static bool CheckDoMove()
        {
            return true;
        }
        public static void ControlBlock()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine("Key: " + keyInfo.Key + ", Char: " + keyInfo.KeyChar + ", Modifiers: " + keyInfo.Modifiers);
            if (keyInfo.Key == ConsoleKey.W)
            {

            }
        }
        public static void CreateNewBlock() 
        {

        }
        public static void ClearLineBlock()
        {

        }
    }
}
