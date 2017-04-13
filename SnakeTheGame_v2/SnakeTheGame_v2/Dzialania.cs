using System;
using System.Collections;

namespace SnakeTheGame_v2
{
    class Dzialania
    {
        int aktualna_pozycja_x = 12;
        int aktualna_pozycja_y = 10;
        public char[,] plansza = new char[20,40];
        public int sciana = 177;
        public int waz = 216;
        public int jablko = 15;
        public int przestrzen = 0;
        public int pozycja_jablka_x;
        public int pozycja_jablka_y;
        Random LosowaLiczba = new Random();

        public void RysujJablko()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            pozycja_jablka_x = LosowaLiczba.Next(1, 39);
            pozycja_jablka_y = LosowaLiczba.Next(1, 19);
            if (plansza[pozycja_jablka_y, pozycja_jablka_x] == (char)waz)
                RysujJablko();
            plansza[pozycja_jablka_y, pozycja_jablka_x] = (char)jablko;
            Console.SetCursorPosition(pozycja_jablka_x, pozycja_jablka_y);
            Console.Write((char)jablko);
        }

        public void RysujPlansze()
        {
            Console.SetWindowSize(58, 20);
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < 20; i++)    //uzupelnianie tla
                for (int j = 0; j < 40; j++)
                {
                    plansza[i, j] = (char)przestrzen;
                }

            for (int i = 0; i < 40; i++)    //uzupelnianie scian
                for (int j = 0; j < 20; j++)
                {
                    plansza[j, 0] = (char)sciana;
                    plansza[j, 39] = (char)sciana;
                    plansza[0, i] = (char)sciana;
                    plansza[19, i] = (char)sciana;
                }

            for (int i = 0; i < 20; i++)    //drukowanie na ekran
            {
                for (int j = 0; j < 40; j++)
                {
                    Console.Write(plansza[i, j]);
                }
                //Console.WriteLine();
                Console.Write("\n");
            }
        }
    
    //metody róchu
        public void poruszanie(ConsoleKey button)
        {
            switch (button)
            {
                case ConsoleKey.W:   //góra
                    SystemZderzen(-1, 0);
                    Interfejs(1);
                    break;
                case ConsoleKey.S:   //dół
                    SystemZderzen(1, 0);
                    Interfejs(3);
                    break;
                case ConsoleKey.A:   //lewo
                    SystemZderzen(0, -1);
                    Interfejs(2);
                    break;
                case ConsoleKey.D:   //prawo
                    SystemZderzen(0, 1);
                    Interfejs(4);
                    break;
                case ConsoleKey.K:   //koniec
                    //Console.Write("Koniec");
                    break;
                default:
                    //Console.Write("Zly klawisz");
                    break;
            }
        }

        void SystemZderzen(int OY, int OX)
        {
            char punkt = plansza[aktualna_pozycja_y + OY, aktualna_pozycja_x + OX];
            if (punkt == (char)przestrzen)
            {
                ZerowanieOgona();
                KolejkaY.Enqueue(aktualna_pozycja_y + OY);
                KolejkaX.Enqueue(aktualna_pozycja_x + OX);
                KolejkaY.Dequeue();
                KolejkaX.Dequeue();
                aktualna_pozycja_x = aktualna_pozycja_x + OX;
                aktualna_pozycja_y = aktualna_pozycja_y + OY;

            }else if((punkt == (char)sciana) && (punkt == (char)waz))
            {
                //koniec gry
            }else if (punkt == (char)jablko)
            {
                plansza[aktualna_pozycja_y + OY, aktualna_pozycja_x + OX] = (char)przestrzen;
                KolejkaY.Enqueue(aktualna_pozycja_y + OY);
                KolejkaX.Enqueue(aktualna_pozycja_x + OX);
                aktualna_pozycja_x = aktualna_pozycja_x + OX;
                aktualna_pozycja_y = aktualna_pozycja_y + OY;
                RysujJablko();
            }
        }
    //metody węża
        
        
            Queue KolejkaX = new Queue();
            Queue KolejkaY = new Queue();

            public void UzupelnienieWeza()
            {
                KolejkaX.Enqueue(10);
                KolejkaY.Enqueue(10);
                KolejkaX.Enqueue(11);
                KolejkaY.Enqueue(10);
                KolejkaX.Enqueue(12);
                KolejkaY.Enqueue(10);
            }

            public void RysowanieWeza()
            {
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 1; i <= KolejkaY.Count; i++)
                {
                    int tmp_y = (int)KolejkaY.Peek();
                    int tmp_x = (int)KolejkaX.Peek();
                    KolejkaY.Dequeue();
                    KolejkaX.Dequeue();
                    KolejkaY.Enqueue(tmp_y);
                    KolejkaX.Enqueue(tmp_x);
                    Console.SetCursorPosition(tmp_x, tmp_y);
                    Console.Write((char)waz);
                }
            }

            public void ZerowanieOgona()
            {

                for (int i = 1; i <= KolejkaY.Count; i++)   //służy do wymazywania ostatniego elementu węża
                {
                    int tmp_y = (int)KolejkaY.Peek();
                    int tmp_x = (int)KolejkaX.Peek();
                    KolejkaY.Dequeue();
                    KolejkaX.Dequeue();
                    KolejkaY.Enqueue(tmp_y);
                    KolejkaX.Enqueue(tmp_x);
                    Console.SetCursorPosition(tmp_x, tmp_y);
                    Console.Write((char)przestrzen);
                }

            }
            public void Interfejs(int kierunek)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(42, 0);
                Console.Write("SNAKE THE GAME");
                Console.SetCursorPosition(42, 2);   
                Console.Write("klawisze:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(47, 4);
                Console.Write("W");
                Console.SetCursorPosition(46, 5);
                Console.Write("A");
                Console.SetCursorPosition(47, 5);
                Console.Write("S");
                Console.SetCursorPosition(48, 5);
                Console.Write("D");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(42, 8);
                int Liczba = (int)KolejkaX.Count - 3;
                Console.Write("Punkty: " + Liczba);

                switch (kierunek)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(47, 4);
                        Console.Write("W");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(46, 5);
                        Console.Write("A");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(47, 5);
                        Console.Write("S");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(48, 5);
                        Console.Write("D");
                        break;
                    default:
                        break;
                }
                
            }

    }
}
