using System;
using System.IO;

namespace TEC_Stadion_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TEC Stadion";
            string valg = "", temp = "", kundedata = "";
            double rabat = 10, max = 0, voksen = 0, antal = 0, børn = 0, prisbørn = 0, prisvoksen = 0, specialrabat = 0, pris = 0, indtægt = 0, usd = 625.45;
            int zone = 0;
            Console.SetWindowSize(80, 25);
            Console.BackgroundColor = ConsoleColor.Blue; Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            do
            {
                Console.Clear();
                Console.SetCursorPosition(50, 0); Console.Write(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"));
                string billetinfo = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "billetinfo.dat"));
                string[] data = billetinfo.Split(';');
                indtægt = Convert.ToDouble(data[4]);
                rabat = Convert.ToDouble(data[5]);
                prisbørn = Convert.ToDouble(data[6]);
                prisvoksen = Convert.ToDouble(data[7]);

                Console.BackgroundColor = ConsoleColor.Yellow; Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(25, 2); Console.Write("Fodboldklubben TEC Ballerup");
                Console.BackgroundColor = ConsoleColor.Blue; Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(46, 4); Console.WriteLine("Indtægt      :  {0,4}kr", data[4]);
                Console.SetCursorPosition(46, 5); Console.WriteLine("Medlemsrabat :{0,4}%", data[5]);
                Console.SetCursorPosition(46, 6); Console.Write("Billetter solgt i alt : {0,4} ", 500 - Convert.ToInt32(data[0]) - Convert.ToInt32(data[1]) - Convert.ToInt32(data[2]) - Convert.ToInt32(data[3]));
                Console.SetCursorPosition(3, 4); Console.Write("Stadion           Pladser");
                Console.BackgroundColor = ConsoleColor.Blue; Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(3, 5); Console.Write("Tribune  A     :  {0}", data[0]);
                Console.SetCursorPosition(3, 6); Console.Write("Tribune  B     :  {0}", data[1]);
                Console.SetCursorPosition(3, 7); Console.Write("Tribune  C     :  {0}", data[2]);
                Console.SetCursorPosition(3, 8); Console.Write("Tribune  D     :  {0}", data[3]);
                Console.SetCursorPosition(3, 9); Console.Write("Vælg Tribune   :  ");


                do
                {
                    valg = Convert.ToString(Console.ReadKey().KeyChar);
                }
                while (!"abcdABCD".Contains(valg));


                if (valg == "a")
                {
                    zone = 0; max = Convert.ToInt32(data[zone]);
                }
                if (valg == "b")
                {
                    zone = 1; max = Convert.ToInt32(data[zone]);
                }
                if (valg == "c")
                {
                    zone = 2; max = Convert.ToInt32(data[zone]); specialrabat = 15;
                }
                if (valg == "d")
                {
                    zone = 3; max = Convert.ToInt32(data[zone]); specialrabat = 15;
                }

                do
                {
                    Console.SetCursorPosition(3, 11); Console.Write("Antal børn (MAX 10!) ? : ");
                    temp = Console.ReadLine();
                    if (temp == "")
                        børn = 0;
                    else
                        børn = Convert.ToInt32(temp);
                }
                while (børn > 10);
                prisbørn = børn * Convert.ToDouble(data[6]);
                Console.SetCursorPosition(46, 7); Console.Write("Pris børn        : {0:N2}kr", prisbørn);

                do
                {

                    Console.SetCursorPosition(3, 11); Console.Write("Antal voksne (MAX 10!) ? : ");
                    temp = Console.ReadLine();
                    if (temp == "")
                        voksen = 0;
                    else
                        voksen = Convert.ToInt32(temp);
                }
                while (voksen > 10);
                prisvoksen = voksen * Convert.ToDouble(data[7]);
                Console.SetCursorPosition(46, 8); Console.Write("Pris voksne      : {0:N2}kr", prisvoksen);
                pris = prisbørn + prisvoksen;
                Console.SetCursorPosition(46, 9); Console.Write("Pris ialt        : {0:N2}kr", pris);
                Console.SetCursorPosition(46, 10); Console.Write("Pris USD        : {0}USD", Math.Floor(pris * 100 / usd));

                if (specialrabat > 0)
                {
                    Console.SetCursorPosition(46, 12); Console.Write("Rabat pga. område");
                    pris = pris - (pris / 100 * specialrabat);
                    Console.SetCursorPosition(46, 13); Console.Write("Rabatpris        : {0:N2}kr", pris);
                }

                Console.SetCursorPosition(3, 13); Console.Write("Medlem af foreningen - j/n ? : ");
                valg = Convert.ToString(Console.ReadKey().KeyChar);
                if (valg == "j")
                {
                    Console.SetCursorPosition(46, 14); Console.Write("Medlemsrabat     : {0:N2}kr", pris / 100 * rabat);
                    pris = pris - (pris / 100 * rabat);
                    Console.SetCursorPosition(46, 15); Console.Write("Pris med rabat   : {0:N2}kr", pris);
                    Console.SetCursorPosition(46, 16); Console.Write("Pris med rabat USD   : {0}USD", Math.Floor(pris * 100 / usd));
                }

                Console.SetCursorPosition(3, 15); Console.Write("Telefonnummer : "); kundedata = Console.ReadLine();
                kundedata = kundedata + " " + "  Børn: " + børn.ToString() + "  Voksen: " + voksen.ToString() + "  Pris: " + pris.ToString("N2") + "  " + DateTime.Now.ToString() + Environment.NewLine;

                Console.SetCursorPosition(3, 17); Console.Write("Du har købt {0} billetter ialt", børn + voksen);


                data[zone] = Convert.ToString(max - (børn + voksen));

                indtægt += pris;

                data[4] = indtægt.ToString("N2");
                billetinfo = data[0] + ";" + data[1] + ";" + data[2] + ";" + data[3] + ";" + data[4] + ";" + data[5] + ";" + data[6] + ";" + data[7] + Environment.NewLine;

                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "billetinfo.dat"), billetinfo);
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "billetsalg.dat"), kundedata);




                Console.SetCursorPosition(3, 23); Console.Write("ENTER FOR AT STARTE OM!"); valg = Convert.ToString(Console.ReadKey().KeyChar);
            } while (valg != "q");


        }
    }
}