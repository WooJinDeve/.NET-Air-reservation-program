using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        private Control con = Control.Instance;

        public bool Init()
        {
            return con.Init();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (MenuPrint())
                {
                    case ConsoleKey.F1: con.Parsing(); break;
                    case ConsoleKey.F2: con.PrintAll(); break;
                    case ConsoleKey.F3: con.AirPortSave(); break;
                    case ConsoleKey.F4: con.AirPortDelete(); break;
                    case ConsoleKey.Escape: return;
                    default: Console.WriteLine("잘못된 메뉴 입력"); break;
                }
                Pause();
            }
        }

        public static void Pause()
        {
            Console.WriteLine("\n아무키나 누르세요....");
            Console.ReadKey();
        }

        public void Exit()
        {
            con.Exit();
        }

        private ConsoleKey MenuPrint()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [ESC] 프로그램종료");
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [F1] xml 데이터 파싱(항공사정보)");
            Console.WriteLine(" [F2] 결과 출력하기");
            Console.WriteLine(" [F3] DB 저장");
            Console.WriteLine(" [F4] DB 삭제");
            Console.WriteLine("******************************************************");
            ConsoleKey key = Console.ReadKey().Key;
            Console.Write("\b");
            return key;
        }


        static void Main(string[] args)
        {

            Program program = new Program();
            if (program.Init() == true)
                program.Run();
            program.Exit();
        }
    }
}
