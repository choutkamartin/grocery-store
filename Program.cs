using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Obchod
{
    class Program
    {

        // Definování tříd
        public class Produkt
        {
            public string ProduktNazev { get; set; }
            public int ProduktCena { get; set; }
            public double ProduktCenaBezDPH { get; set; }
        }

        // Definování proměnných
        public static int kredit = 100;
        public static int celkem;
        public static int celkemNakup;
        public static double celkemBezDPH;
        public static double celkemNakupBezDPH;

        // Definování listů
        public static List<Produkt> produkty = new List<Produkt>();
        public static List<Produkt> vybraneProdukty = new List<Produkt>();
        public static List<Produkt> zakoupeneProdukty = new List<Produkt>();

        // Administrace obchodu, slouží k přidávání a odebírání produktů z obchodu
        public static void AdministraceObchodu()
        {
            Console.Clear();
            Console.WriteLine("Administrace obchodu");
            Console.WriteLine("--------------------");
            Console.WriteLine("Přidat produkt:  (P)");
            Console.WriteLine("Odebrat produkt: (O)");
            Console.WriteLine("Vrátit se zpět:  (Z)");

            // Přečte vstup klávesy od uživatele a porovná s case
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.P:
                    {
                        PridatProdukt();
                        break;
                    }
                case ConsoleKey.O:
                    {
                        OdebratProdukt();
                        break;
                    }
                case ConsoleKey.Z:
                    {
                        Main();
                        break;
                    }
            }
        }

        // Umožňuje přidat produkt do obchodu
        public static void PridatProdukt()
        {
            Console.Clear();
            Console.WriteLine("Administrace obchodu");
            Console.WriteLine("--------------------");
            Console.WriteLine("Přidat produkt");
            Console.WriteLine("--------------------");

            // Přečte vstupy od uživatele a ukládá je do seznamu (list) jako produkt (class)
            try
            {
                Console.Write("Název produktu: ");
                string ProduktNazev = Console.ReadLine();
                Console.Write("Cena produktu s DPH: ");
                int ProduktCena = Convert.ToInt32(Console.ReadLine());
                produkty.Add(new Produkt { ProduktNazev = ProduktNazev, ProduktCena = ProduktCena, ProduktCenaBezDPH = ProduktCena - (ProduktCena * 0.21) });
            }

            // Zachytí chybné vstupy od uživatele a nedovolí "rozbít" program
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Zadali jste nesprávný formát");
                Console.WriteLine("Opravdu jste zadali název produktu?");
                Console.WriteLine("Zadali jste cenu jako číslo?");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                AdministraceObchodu();
            }

            // Pokud nenastane Exception, vypíše úspěšnou hlášku
            finally
            {
                Console.Clear();
                Console.WriteLine("Produkt přidán");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                AdministraceObchodu();
            }
        }

        // Umožňuje odebrat produkt z obchodu
        public static void OdebratProdukt()
        {
            Console.Clear();
            Console.WriteLine("Administrace obchodu");
            Console.WriteLine("--------------------");
            Console.WriteLine("Odebrat produkt");
            Console.WriteLine("--------------------");

            // Přečte vstup od uživatele (který by měl být index produktu) a odebere produkt z obchodu
            try
            {
                Console.Write("Číslo produktu, které chceš odebrat: ");
                int cislo = Convert.ToInt32(Console.ReadLine());
                produkty.RemoveAt(cislo);
            }

            // Zachytí chybný index, který neexistuje a nedovolí "rozbít" program
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Zadali jste číslo produktu, které neexistuje");
                Console.WriteLine("Zboží nebylo odebráno");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                AdministraceObchodu();
            }

            // Zachytí chybné vstupy od uživatele a nedovolí "rozbít" program
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Zadali jste nesprávný formát");
                Console.WriteLine("Opravdu jste zadali číslo?");
                Console.WriteLine("Zboží nebylo odebráno");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                AdministraceObchodu();
            }

            // Pokud nenastane Exception, vypíše úspěšnou hlášku
            finally
            {
                Console.Clear();
                Console.WriteLine("Zboží bylo odebráno z obchodu");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                AdministraceObchodu();
            }
        }

        // Vypíše produkty, které se nachází v obchodě
        public static void VypsatProdukt()
        {
            Console.Clear();
            Console.WriteLine("Výpis produktů");
            Console.WriteLine("--------------");

            // Vypíše každý produkt v obchodě, a zároveň s ním i ceny
            foreach (var produkt in produkty)
            {
                Console.WriteLine("Produkt: {0}, cena produktu: {1} Kč, cena produktu bez DPH: {2} Kč", produkt.ProduktNazev, produkt.ProduktCena, produkt.ProduktCenaBezDPH);
            }
            Console.WriteLine("--------------");
            Console.WriteLine("Zmáčkněte Enter");
            Console.ReadLine();
        }

        // Umožňuje přidat produkt do košíku
        public static void PridatKosik()
        {
            Console.Clear();
            Console.WriteLine("Přidat produkt do košíku");
            Console.WriteLine("------------------------");

            // Přečte vstup od uživatele, zkonvertuje ho na integer, odebere a přidá do seznamů
            try
            {
                Console.Write("Pořadové číslo produktu: ");
                string input = Console.ReadLine();
                Console.Clear();
                int cislo = Convert.ToInt32(input);
                Produkt vybranyProdukt = produkty[cislo];
                produkty.Remove(vybranyProdukt);
                vybraneProdukty.Add(vybranyProdukt);
            }

            // Zachytí chybný index, který neexistuje a nedovolí "rozbít" program
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Zadali jste index, který neexistuje");
                Console.WriteLine("Zboží nebylo přidáno do košíku");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                Main();
            }

            // Zachytí chybné vstupy od uživatele a nedovolí "rozbít" program
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Zadali jste nesprávný formát");
                Console.WriteLine("Opravdu zadáváte číslo?");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                Main();
            }

            // Pokud nenastane Exception, vypíše úspěšnou hlášku
            finally
            {
                Console.Clear();
                Console.WriteLine("Zboží přidáno do košíku");
                Console.WriteLine("Zmáčkněte Enter");
                Console.ReadLine();
                Main();
            }
        }

        // Vypíše produkty, které se nachází v košíku
        public static void ZobrazitKosik()
        {
            Console.Clear();
            Console.WriteLine("Zboží v košíku");
            Console.WriteLine("--------------");

            // Vypíše každý produkt v košíku, a zároveň s ním i cenu s DPH
            foreach (var vybranyProdukt in vybraneProdukty)
            {
                Console.WriteLine("Produkt: {0}, cena produktu: {1}", vybranyProdukt.ProduktNazev, vybranyProdukt.ProduktCena);
            }
            Console.WriteLine("--------------");
            Console.WriteLine("Zmáčkněte Enter");
            Console.ReadLine();
        }

        // Umožňuje zaplatit za produkty v košíku
        public static void Zaplatit()
        {
            Console.Clear();
            Console.WriteLine("Zaplatit za produkty");
            Console.WriteLine("--------------------");
            Console.WriteLine("Váš kredit: {0}", kredit);

            // Sečte cenu všech produktů v košíku jak s DPH tak bez DPH
            celkem = vybraneProdukty.Sum(item => item.ProduktCena);
            celkemBezDPH = vybraneProdukty.Sum(item => item.ProduktCenaBezDPH);

            Console.WriteLine("Cena vašeho nákupu s DPH je: {0}", celkem);
            Console.WriteLine("Zaplatit: (P)");
            Console.WriteLine("Zpět:     (Z)");

            // Přečti vstup stisku klávesy od uživatele a porovnej s case
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.P:
                    {

                        // Pokud košík obsahuje alespoň jeden produkt
                        if (vybraneProdukty.Any())
                        {

                            // Pokud je kredit větší nebo rovno než celková cena za produkty
                            if (kredit >= celkem)
                            {
                                kredit -= celkem;
                                celkemNakup = celkem;
                                celkemNakupBezDPH = celkemBezDPH;
                                zakoupeneProdukty.AddRange(vybraneProdukty);
                                vybraneProdukty.Clear();
                                Console.Clear();
                                Console.WriteLine("Zboží zakoupeno");
                            }

                            // Pokud není kredit větší nebo rovno než celková cena za produkty
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Nemáš dostatek kreditů na nákup");
                            }
                            Console.WriteLine("Zmáčkněte Enter");
                            Console.ReadLine();
                            return;
                        }

                        // Pokud košík nic neobsahuje
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("V košíku nic nemáte");
                            Console.WriteLine("Zmáčkněte Enter");
                            Console.ReadLine();
                            return;
                        }
                    }
                case ConsoleKey.Z:
                    {
                        return;
                    }
            }
        }

        // Vypíše produkty, které zákazník zakoupil a spolu s tím i ceny celkem s DPH i bez DPH
        public static void Uctenka()
        {
            Console.Clear();
            Console.WriteLine("Zakoupené zboží");
            Console.WriteLine("--------------------");
            celkemBezDPH = vybraneProdukty.Sum(item => item.ProduktCenaBezDPH);

            // Vypíše každý zakoupený produkt, a zároveň s ním i ceny
            foreach (var zakoupenyProdukt in zakoupeneProdukty)
            {
                Console.WriteLine("Produkt: {0}, cena s DPH: {1}, cena bez DPH: {2}", zakoupenyProdukt.ProduktNazev, zakoupenyProdukt.ProduktCena, zakoupenyProdukt.ProduktCenaBezDPH);
            }

            Console.WriteLine("--------------------");
            Console.WriteLine("Celkem s DPH:    {0}", celkemNakup);
            Console.WriteLine("Celkem bez DPH:  {0}", celkemNakupBezDPH);
            Console.ReadLine();
        }

        // Uloží produkty, které se nachází v obchodě do souboru XML
        public static void UlozitXML()
        {
            Console.Clear();

            // Vytvoří proměnnou, do které uloží produkty s jejich cenami
            var xmlProduktyUlozit = new XElement("Produkty",
                from produkt in produkty
                select new XElement("Produkt",
                new XAttribute("Název", produkt.ProduktNazev),
                new XAttribute("Cena", produkt.ProduktCena),
                new XAttribute("CenaBezDPH", produkt.ProduktCenaBezDPH)));

            // Uloží proměnnou, do které se načetli předchozí hodnoty do souboru XML
            xmlProduktyUlozit.Save("produkty.xml");
            Console.WriteLine("Uloženo do XML");
            Console.Read();
        }

        // Vypíše základní nápovědu, která se nenachází na ostatních obrazovkách pro uživatele
        public static void Napoveda()
        {
            Console.Clear();
            Console.WriteLine("Nápověda k administraci");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Heslo do administrace: heslo");
            Console.WriteLine();
            Console.WriteLine("Nápověda k placení");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Každý uživatel má kredit 100.");
            Console.WriteLine("Nelze zaplatit, pokud v košíku nic není.");
            Console.WriteLine("Cena produktu se odečte od kreditu.");
            Console.WriteLine("Zakoupené zboží z obchodu zmizí.");
            Console.ReadLine();
        }

        // Hlavní metoda, která se načítá po spuštění programu
        public static void Main()
        {
            // Zajišťuje nekonečnou podmínku
            do
            {
                Console.Clear();
                Console.WriteLine("Obchod U tří jestřábů");
                Console.WriteLine("---------------------");
                Console.WriteLine("Administrace:     (A)");
                Console.WriteLine("XML ukládání:     (X)");
                Console.WriteLine("---------------------");
                Console.WriteLine("Vypsat produkty:  (V)");
                Console.WriteLine("---------------------");
                Console.WriteLine("Přidat do košíku: (P)");
                Console.WriteLine("Košík:            (K)");
                Console.WriteLine("Zaplatit:         (Z)");
                Console.WriteLine("Účtenka:          (U)");
                Console.WriteLine("---------------------");
                Console.WriteLine("Nápověda:         (N)");

                // Přečti vstup stisku klávesy od uživatele a porovnej s case
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.A:
                        {
                            Console.Clear();
                            Console.WriteLine("Přihlásit se");
                            Console.WriteLine("------------");
                            Console.Write("Zadej heslo: ");

                            // Uloží vstup do uživatele do proměnné, kterou poté zkontroluje s nastaveným heslem
                            string heslo = Console.ReadLine();

                            // Pokud se hesla shodují, pustí uživatele do administrace obchodu
                            if (heslo == "heslo")
                            {
                                AdministraceObchodu();
                            }

                            // Pokud se hesla neshodují, vypíše chybovou hlášku
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Zadáno špatné heslo");
                                Console.WriteLine("Zmáčkněte Enter");
                                Console.ReadLine();
                            }
                            break;
                        }
                    case ConsoleKey.V:
                        {
                            VypsatProdukt();
                            break;
                        }
                    case ConsoleKey.P:
                        {
                            PridatKosik();
                            break;
                        }
                    case ConsoleKey.K:
                        {
                            ZobrazitKosik();
                            break;
                        }
                    case ConsoleKey.Z:
                        {
                            Zaplatit();
                            break;
                        }
                    case ConsoleKey.U:
                        {
                            Uctenka();
                            break;
                        }
                    case ConsoleKey.X:
                        {
                            UlozitXML();
                            break;
                        }
                    case ConsoleKey.N:
                        {
                            Napoveda();
                            break;
                        }
                }
            }
            while
            ('a' != 'b');
        }
    }
}

