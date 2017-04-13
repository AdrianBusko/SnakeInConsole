using System;
using System.Threading;

namespace SnakeTheGame_v2
{
    class Program : Dzialania
    {
        static void Main(string[] args)
        {
            Dzialania wykonanie = new Dzialania();
            wykonanie.RysujPlansze();
            wykonanie.UzupelnienieWeza();
            wykonanie.RysujJablko();
            wykonanie.RysowanieWeza();
            wykonanie.Interfejs(0);


            var klawisz = Console.ReadKey(true);  

            for (; ; )
            {
                Thread.Sleep(100);
                if(Console.KeyAvailable == true)
                {
                    klawisz = Console.ReadKey(true);
                }
                wykonanie.poruszanie(klawisz.Key);
                wykonanie.RysowanieWeza();
            }
        }
    }
}
