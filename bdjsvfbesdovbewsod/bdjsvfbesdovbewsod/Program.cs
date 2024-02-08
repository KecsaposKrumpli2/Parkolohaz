using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;








namespace MyApp // Note: actual namespace depends on the project name.
{
    class Emelet
    {
        public string ParkoloNev { get; set; }
        public List<int> ParkoloSzamok { get; set; }

        public Emelet(string v)
        {
            var tomb = v.Split(";");
            ParkoloNev = tomb[0];
            ParkoloSzamok = new List<int>();
            for (int i = 1; i < tomb.Length; i++)
            {
                ParkoloSzamok.Add(int.Parse(tomb[i]));
            }
        }

        public override string ToString()
        {
            string kiiras = $"{ParkoloNev,8} ";
            for (int i = 0; i < ParkoloSzamok.Count; i++)
            {
                kiiras += $" {ParkoloSzamok[i]}";
            }
            return kiiras;
        }
    }

    class Program
    {
        static string legKevesebbAuto(List<Emelet> k)
        {
            int f8 = k.Min(y => y.ParkoloSzamok.Sum());


            string f8nev = string.Empty;

            //k.Where(x => x.ParkoloSzamok.Sum() == f8).Select(y => y.ParkoloNev)

            for (int i = 0; i < k.Count; i++)
            {
                if (k[i].ParkoloSzamok.Sum() == f8)
                {
                    f8nev = k[i].ParkoloNev;
                }
            }

            return f8nev;
        }


        static void VanEUres(List<Emelet> parkolok)
        {
            bool vanEures = false;
            for (int i = 0; i < parkolok.Count; i++)
            {
                for (int y = 0; y < parkolok[i].ParkoloSzamok.Count; y++)
                {
                    if (parkolok[i].ParkoloSzamok[y] == 0)
                    {
                        Console.WriteLine($"Van üres szektor az {i + 1}. emeleten a(z) {y + 1}. szektorban.");
                        vanEures = true;
                    }
                }
            }
            if (!vanEures)
            {
                Console.WriteLine("Nincs olyan szektor, ahol nincs autó.");
            }
        }

        static void Atlagszamitas(List<Emelet> k)
        {
            //int osszesAuto = k.Sum(x => x.ParkoloSzamok.Sum());

            //double atlag = (double)osszesAuto / k.Count;

            //int atlagos = k.Count(x => x.ParkoloSzamok.Average() == atlag);
            //int atlagAlatti = k.Count(x => x.ParkoloSzamok.Average() < atlag);
            //int atlagFeletti = k.Count(x => x.ParkoloSzamok.Average() > atlag);


            //Console.WriteLine($"Összesen {atlagAlatti} db átlag alatti, {atlagFeletti} db átlag feletti és {atlagos} db átlaggal egyenlő ({atlag}) szektor van.");

            int osszesAuto = k.Sum(x => x.ParkoloSzamok.Sum());

            double atlag = (double)osszesAuto / k.Count;

            int atlagAlatti = 0;
            int atlagFeletti = 0;
            int atlagos = 0;

            for (int i = 0; i < k.Count; i++)
            {
                for (int y = 0; y < k[i].ParkoloSzamok.Count; y++)
                {
                    if (k[i].ParkoloSzamok[y] < atlag)
                    {
                        atlagAlatti++;
                    }

                    else if (k[i].ParkoloSzamok[y] > atlag)
                    {
                        atlagFeletti++;
                    }

                    else
                    {
                        atlagos++;
                    }
                }

            }
            Console.WriteLine($"Összesen {atlagAlatti} db átlag alatti, {atlagFeletti} db átlag feletti és {atlagos} db átlaggal egyenlő ({atlag}) szektor van.");
        }

        static void Main(string[] args)
        {
            List<Emelet> parkolok = new List<Emelet>();

            foreach (var item in File.ReadAllLines(@"..\..\..\SRC\parkolohaz.txt"))
            {
                parkolok.Add(new Emelet(item));
            }

            int i = 0;
            Console.WriteLine($"{"Parkoló neve"} {"1.",2} {"2.",2} {"3.",2} {"4.",2} {"5.",2} {"6.",2}");
            foreach (var item in parkolok)
            {
                Console.WriteLine($"{i + 1}. {item}");
                i++;
            }


            //8.Melyik emeleten parkol a legkevesebb autó? Írd ki az emelet nevét a képernyőre.


            Console.WriteLine("\n8.feladat");
            Console.WriteLine($"A(z) {legKevesebbAuto(parkolok)}-ban/-ben parkolnak a legkevesebben. ");


            //9.Írd ki a képernyőre, hogy van-e olyan szektor, ahol nincs autó. Írd ki ennek a szintnek a számát
            //és a szektor sorszámát.Ha nincs ilyen, írj ki hibaüzenetet.

            Console.WriteLine("\n9.feladat");
            VanEUres(parkolok);

            //10.Írd ki a képernyőre, hogy hány szektorban van jelenleg átlagos mennyiségű autó? átlag alatti mennyiségű autó? o átlag fölötti mennyiségű autó?

            //Az átlagot a jelenlegi adatokból számold két tizedesjegy pontossággal.

            Console.WriteLine("\n10.feladat");
            Atlagszamitas(parkolok);

            //11.Melyik emeletek melyik szektoraiban van csak 1 - 1 autó? Soronként írd ki egy új fájlba az emelet
            //nevét, mellé kötőjellel elválasztva a szektorok sorszámát / sorszámait.




            //12.Írd ki a képernyőre: igaz - e, hogy a legfelső emeleten van a legtöbb autó? Ha nem igaz, akkor
            //írd ki, hogy melyik emeleten van több autó.




            //13.Írd ki a fenti fájlba, hogy hányas feladat megoldása következik, majd új sorban kezdve,
            //soronként írd ki az emeletek sorszámát, és az adott szinten levő szabad helyek számát.




            //14.Írd ki a képernyőre, hogy hány szabad hely van jelenleg a parkolóházban.
        }
    }
}














