using System.Web.Helpers;

namespace JimApp
{
    internal class Program
    {

        private static bool infosGeändert = false;
        private static bool nochmalÄndern = true;
        private static double kalorienBedarfFrau = 0;
        private static double kalorienBedarfMann = 0;

        static void Main(string[] args)
        {
            // Alle Infos des Benutzers erfassen
            InfosErfasst();

            // Grundumsatz berechnen
            GrundumsatzBerechnung();

            // Einnahme Ernährung
            EinnahmeNährstoffe();

            // Kalorien Verbrennt
            KalorienVerbrennung();

            // Essen tracken
            EssenTracken();

            // Infos ändern, falls der Benutzer dies möchte
            do
            {
                InfosÄndern();
                if (infosGeändert == true)
                {
                    Console.WriteLine("");
                    GrundumsatzBerechnung();
                    EinnahmeNährstoffe();
                }
                if (nochmalÄndern == false)
                {
                    break;
                }
            } while (true);

        }

        static void EinnahmeNährstoffe()
        {
            Console.WriteLine("");
            CarbsEinnahme();
            ProteinEinnahme();
            FettEinnahme();
            CreatineEinnahme();
        }

        static void BulkOrCut()
        {
            do
            {
                try
                {
                    Console.WriteLine("Wollen Sie Bulken oder Cutten? [bulk|cut]: ");
                    string Entscheidung_bulk_cut = Console.ReadLine();
                    if (Entscheidung_bulk_cut == "bulk")
                    {
                        File.WriteAllText("bulk_cut.txt", "bulk");
                        break;
                    }
                    else if (Entscheidung_bulk_cut == "cut")
                    {
                        File.WriteAllText("bulk_cut.txt", "cut");
                        break;
                    }

                }
                catch (System.IO.FileNotFoundException)
                {
                    File.WriteAllText("bulk_cut.txt", "");
                }
            } while (true);
        }

        static void InfosÄndern()
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nWillst du deine persönlichen Daten ändern? [ja|nein]: ");
                Console.ForegroundColor = ConsoleColor.Gray;

                string entscheidung = Console.ReadLine();
                if (entscheidung.ToLower() == "ja")
                {
                    Console.WriteLine("");
                    Geschlecht();
                    Gewicht();
                    Grösse();
                    Alter();
                    BulkOrCut();
                    infosGeändert = true;
                    break;
                }
                else if (entscheidung.ToLower() == "nein")
                {
                    nochmalÄndern = false;
                    break;
                }
            } while (true);
        }

        static void GrundumsatzBerechnung()
        {
            if (File.ReadAllText("geschlecht.txt") == "w")
            {                
                kalorienBedarfFrau = 655.1
                    + (9.6 * Convert.ToDouble(File.ReadAllText("gewicht.txt")))
                    + (1.8 * Convert.ToDouble(File.ReadAllText("grösse.txt")))
                    - (4.7 * Convert.ToDouble(File.ReadAllText("Alter.txt")));
                kalorienBedarfFrau = Math.Round(kalorienBedarfFrau);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Du darfst so viele Kalorien konsumieren: " + kalorienBedarfFrau);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                kalorienBedarfMann = 66.47
                    + (13.7 * Convert.ToDouble(File.ReadAllText("gewicht.txt")))
                    + (5 * Convert.ToDouble(File.ReadAllText("grösse.txt")))
                    - (6.8 * Convert.ToDouble(File.ReadAllText("alter.txt")));
                kalorienBedarfMann = Math.Round(kalorienBedarfMann);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Du darfst so viele Kalorien konsumieren: " + kalorienBedarfMann);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        static void Geschlecht()
        {
            do
            {
                try
                {
                    Console.WriteLine("Gib dein Geschlecht ein [m|w]: ");
                    string geschlecht = Console.ReadLine();
                    if (geschlecht != "w" && geschlecht != "m")
                    {
                        Console.WriteLine("Fehlerhafte Eingabe");
                    }
                    else
                    {
                        File.WriteAllText("geschlecht.txt", geschlecht);
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    FehlEingabe();
                }
            } while (true);
        }

        static void Gewicht()
        {
            do
            {
                try
                {
                    Console.WriteLine("Gib dein aktuelles Gewicht ein [Bsp. '75']: ");
                    int gewicht = Convert.ToInt32(Console.ReadLine());
                    if (gewicht > 300)
                    {
                        Console.WriteLine("Nur Gewicht unter 300 kg möglich!");
                    }
                    else if (gewicht < 20)
                    {
                        Console.WriteLine("Nur Gewicht über 20 kg möglich!");
                    }
                    else
                    {
                        File.WriteAllText("gewicht.txt", Convert.ToString(gewicht));
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    FehlEingabe();
                }
            } while (true);
        }

        static void Grösse()
        {
            do
            {
                try
                {
                    Console.WriteLine("Gib deine aktuelle Grösse ein [Bsp. '182']: ");
                    int grösse = Convert.ToInt32(Console.ReadLine());
                    if (grösse > 250)
                    {
                        Console.WriteLine("Nur Grössen unter 250 cm möglich!");
                    }
                    else if (grösse < 100)
                    {
                        Console.WriteLine("Nur Grössen über 100 cm möglich!");
                    }
                    else
                    {
                        File.WriteAllText("grösse.txt", Convert.ToString(grösse));
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    FehlEingabe();
                }
            } while (true);
        }

        static void Alter()
        {
            do
            {
                try
                {
                    Console.WriteLine("Gib dein Alter ein: ");
                    int alter = Convert.ToInt32(Console.ReadLine());
                    if (alter > 130)
                    {
                        Console.WriteLine("Das Alter muss unter 130 sein!");
                    }
                    else if (alter < 5)
                    {
                        Console.WriteLine("Sie müssen mindestens 5 Jahre alt sein!");
                    }
                    else
                    {
                        File.WriteAllText("alter.txt", Convert.ToString(alter));
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    FehlEingabe();
                }
            } while (true);
        }

        static void InfosErfasst()
        {
            bool geschlechtErfasst = false;
            bool gewichtErfasst = false;
            bool grösseErfasst = false;
            bool alterErfasst = false;
            bool bocErfasst = false;

            do
            {
                try
                {
                    // Geschlecht erfassen
                    if (File.ReadAllText("geschlecht.txt") == "")
                    {
                        Geschlecht();
                    }
                    else
                    {
                        geschlechtErfasst = true;
                    }

                    // Gewicht erfassen
                    if (File.ReadAllText("gewicht.txt") == "")
                    {
                        Gewicht();
                    }
                    else
                    {
                        gewichtErfasst = true;
                    }

                    // Grösse erfassen
                    if (File.ReadAllText("grösse.txt") == "")
                    {
                        Grösse();
                    }
                    else
                    {
                        grösseErfasst = true;
                    }

                    // Alter erfassen
                    if (File.ReadAllText("alter.txt") == "")
                    {
                        Alter();
                    }
                    else
                    {
                        alterErfasst = true;
                    }

                    // Bulk oder Cut
                    if (File.ReadAllText("bulk_cut.txt") == "")
                    {
                        BulkOrCut();
                    }
                    else
                    {
                        bocErfasst = true;
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    if (geschlechtErfasst == false)
                    {
                        File.WriteAllText("geschlecht.txt", "");
                        if (gewichtErfasst == false)
                        {
                            File.WriteAllText("gewicht.txt", "");
                            if (grösseErfasst == false)
                            {
                                File.WriteAllText("grösse.txt", "");
                                if (alterErfasst == false)
                                {
                                    File.WriteAllText("alter.txt", "");
                                    if (bocErfasst == false)
                                    {
                                        File.WriteAllText("bulk_cut.txt", "");
                                    }
                                }
                            }
                        }
                    }
                }

                if (geschlechtErfasst == true)
                {
                    if (gewichtErfasst == true)
                    {
                        if (grösseErfasst == true)
                        {
                            if (bocErfasst == true)
                            {
                                break;
                            }
                        }
                    }
                }
            } while (true);
        }

        static void ProteinEinnahme()
        {
            int gewicht = Convert.ToInt32(File.ReadAllText("gewicht.txt"));
            double protein = 0;
            if (File.ReadAllText("bulk_cut.txt") == "cut")
            {
                protein = gewicht * 2;
            }
            else if (File.ReadAllText("bulk_cut.txt") == "bulk")
            {
                protein = gewicht * 2.5;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Du sollst " + protein + " g Protein pro Tag einnehmen");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void CreatineEinnahme()
        {
            int gewicht = Convert.ToInt32(File.ReadAllText("gewicht.txt"));
            int creatine = gewicht / 10;
            Console.WriteLine("Du sollst " + creatine + " g Creatine pro Tag einnehmen");
        }

        static void CarbsEinnahme()
        {
            int gewicht = Convert.ToInt32(File.ReadAllText("gewicht.txt"));
            int carbs = 0;
            if (File.ReadAllText("bulk_cut.txt") == "cut")
            {
                carbs = gewicht * 3;
            }
            else if (File.ReadAllText("bulk_cut.txt") == "bulk")
            {
                carbs = gewicht * 5;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Du sollst " + carbs + " g Kohlenhydrate pro Tag einnehmen");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void FettEinnahme()
        {
            int gewicht = Convert.ToInt32(File.ReadAllText("gewicht.txt"));
            double fett = 0;
            if (File.ReadAllText("bulk_cut.txt") == "cut")
            {
                fett = gewicht * 0.6;
            }
            else if (File.ReadAllText("bulk_cut.txt") == "bulk")
            {
                fett = gewicht * 1;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Du sollst " + fett + " g Fett pro Tag einnehmen");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void EssenTracken()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nWollen sie Ihre konsumierte Nahrung tracken? [ja|nein]: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string seineNahrung = Console.ReadLine();

            if (seineNahrung.ToLower() == "ja")
            {
                double fehlendeKalorien = 0;
                double gesamtKalorien = 0;
                do
                {
                    string inPath = @"C:\Users\filip\Desktop\LebensmittelUndKalorien.csv";
                    string text = File.ReadAllText(inPath);

                    string[] lines = text.Split("\r\n");

                    int words = lines.Length;
                    string[] Lebensmittel = new string[words];
                    int[] Kalorien = new int[words];

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] items = lines[i].Split(',');
                        Lebensmittel[i] = items[0];
                        Kalorien[i] = Convert.ToInt32(items[1]);
                    }

                    Console.WriteLine("\nAnzahl Zeilen: " + Lebensmittel.Length);

                    Console.WriteLine("\nWas hast du heute gegessen?: ");
                    string entscheidungEssen = Console.ReadLine();
                    string schlaufeBeenden = "";

                    for (int i = 0; i < Lebensmittel.Length; i++)
                    {
                        if (entscheidungEssen.ToLower() == Lebensmittel[i])
                        {
                            Console.WriteLine("Wie viel Gramm " + entscheidungEssen + " hast du gegessen? [z.B '150']");
                            double gramm = Convert.ToDouble(Console.ReadLine());
                                                  
                            // GesamtKalorien
                            gesamtKalorien = gesamtKalorien + ((Convert.ToDouble(Kalorien[i]) / 100) * gramm);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                            Console.WriteLine("So viele Kalorien hast du zu dir genommen: " + ((Convert.ToDouble(Kalorien[i]) / 100) * gramm));
                            Console.ForegroundColor = ConsoleColor.Green;
                            // noch fehlende Kalorien
                            
                            if (File.ReadAllText("geschlecht.txt") == "m")
                            {
                                fehlendeKalorien = kalorienBedarfMann - gesamtKalorien;
                            }
                            else
                            {
                                fehlendeKalorien = kalorienBedarfFrau - gesamtKalorien;
                            }

                            Console.WriteLine("\nDu hast heute schon " + gesamtKalorien + " kcal zu dir genommen");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Du darfst noch " + fehlendeKalorien + " kcal zu dir nehmen\n");                                
                            Console.ForegroundColor = ConsoleColor.Gray;

                            do
                            {
                                
                                Console.WriteLine("Willst du noch mehr eintragen? [ja|nein]: ");
                                schlaufeBeenden = Convert.ToString(Console.ReadLine());
                                if (schlaufeBeenden.ToLower() == "ja")
                                {
                                    break;
                                }
                                else if(schlaufeBeenden.ToLower() == "nein")
                                {
                                    break;
                                }
                            } while (true);
                            break;
                        }
                    }
                    if (schlaufeBeenden.ToLower() == "nein")
                    {
                        break;
                    }
                } while (true);
                /*
                do
                {
                    string[,] Essen = new string[2, 9] { { "banane", "apfel", "lachs", "cheeseburger", "hamburger", "spaghetti", "kekse", "pommes", "haferflocken"},
                                                         { "88", "52", "208", "303", "295", "158", "353", "312", "350" } };

                    Console.WriteLine("\nWas hast du heute gegessen?: ");
                    string entscheidungEssen = Console.ReadLine();
                    string schlaufeBeenden = "";

                    for (int i = 0; i < Essen.Length / 2; i++)
                    {

                        if (entscheidungEssen.ToLower() == Essen[0, i])
                        {
                            Console.WriteLine("Wie viel Gramm " + entscheidungEssen + " hast du gegessen? [z.B '150']");
                            double gramm = Convert.ToDouble(Console.ReadLine());

                            // GesamtKalorien
                            gesamtKalorien = gesamtKalorien + ((Convert.ToDouble(Essen[1, i]) / 100) * gramm);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                            Console.WriteLine("So viele Kalorien hast du zu dir genommen: " + ((Convert.ToDouble(Essen[1, i]) / 100) * gramm));
                            Console.ForegroundColor = ConsoleColor.Green;
                            // noch fehlende Kalorien

                            if (File.ReadAllText("geschlecht.txt") == "m")
                            {
                                fehlendeKalorien = kalorienBedarfMann - gesamtKalorien;
                            }
                            else
                            {
                                fehlendeKalorien = kalorienBedarfFrau - gesamtKalorien;
                            }

                            Console.WriteLine("\nDu hast heute schon " + gesamtKalorien + " kcal zu dir genommen");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Du darfst noch " + fehlendeKalorien + " kcal zu dir nehmen\n");
                            Console.ForegroundColor = ConsoleColor.Gray;

                            do
                            {

                                Console.WriteLine("Willst du noch mehr eintragen? [ja|nein]: ");
                                schlaufeBeenden = Convert.ToString(Console.ReadLine());
                                if (schlaufeBeenden.ToLower() == "ja")
                                {
                                    break;
                                }
                                else if (schlaufeBeenden.ToLower() == "nein")
                                {
                                    break;
                                }
                            } while (true);
                            break;
                        }
                    }
                    if (schlaufeBeenden.ToLower() == "nein")
                    {
                        break;
                    }
                } while (true);
                */
            }
        }

        static void KalorienVerbrennung()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nHast du heute Sport getrieben?: [ja|nein]");
            Console.ForegroundColor = ConsoleColor.Gray;

            string sportGetrieben = Console.ReadLine();
            if (sportGetrieben == "ja")
            {
                string weitereAktivitäten = "";
                double gesamtKalorienVerbrennt = 0;
                double kalorienVerbrennt = 0;
                int gewichtMomentan = Convert.ToInt32(File.ReadAllText("gewicht.txt"));

                string[,] Aktivitäten = new string[11, 6] { { "basketball","114", "135", "153", "176", "197"},
                                                            { "fussball" ,"114", "134", "154", "176", "195" },
                                                            { "jogging schnell","165", "210", "241", "270", "300" },
                                                            { "radfahren" ,"83", "98", "113", "128", "145" },
                                                            { "schwimmen","131", "155", "179", "201", "227"},
                                                            { "spazierengehen" ,"54", "62", "71", "78", "86"},
                                                            { "tennis","90", "107", "124", "140", "156"},
                                                            { "volleyball","42", "50", "56", "64", "72"},
                                                            { "walken leicht", "62", "73", "84", "96", "108"},
                                                            { "walken power","80", "95", "111", "126", "139"},
                                                            { "jogging langsam","113","132","152","165","195",} };

                Console.Write("\n");

                for (int i = 0; i < Aktivitäten.Length / 6; i++)
                {
                    Console.WriteLine(Aktivitäten[i, 0]);
                }

                do
                {
                    int[,] Gewicht_Kalorien = new int[5, 2] { {65, 1},
                                                              {75, 2},
                                                              {85, 3},
                                                              {95, 4},
                                                              {105, 5} };

                    bool aktivitätGefunden = false;

                    do
                    {
                        aktivitätGefunden = false;

                        Console.WriteLine("\nWas hast du heute gemacht?: ");
                        string entscheidungAktivität = Console.ReadLine();

                        for (int i = 0; i < Aktivitäten.Length / 6; i++)
                        {
                            if (entscheidungAktivität.ToLower() == Aktivitäten[i, 0])
                            {
                                for (int z = 0; z < Gewicht_Kalorien.Length / 2; z++)
                                {
                                    if (gewichtMomentan < Gewicht_Kalorien[z, 0])
                                    {
                                        do
                                        {
                                            try
                                            {
                                                Console.WriteLine("Wie lange haben Sie heute " + entscheidungAktivität + " gemacht/gespielt?: ");
                                                int zeit = Convert.ToInt32(Console.ReadLine());

                                                int welcheSpalte = Convert.ToInt32(Gewicht_Kalorien[z, 1]);
                                                // Kalorien der Sportart auf 1 min rechnen
                                                kalorienVerbrennt = Convert.ToDouble(Aktivitäten[i, welcheSpalte]) / 15; // 15 weil die kcal auf 15 min sind beim array Aktivitäten
                                                kalorienVerbrennt = kalorienVerbrennt * zeit;
                                              
                                                Console.WriteLine("Du hast mit dieser Aktivität " + kalorienVerbrennt + " Kcal verbennt");
                                                // Gesamt Kalorien des Tages berechnen
                                                gesamtKalorienVerbrennt = gesamtKalorienVerbrennt + kalorienVerbrennt;

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("\nDu hast heute " + gesamtKalorienVerbrennt + " Kcal verbrennt!");
                                                Console.ForegroundColor = ConsoleColor.Gray;

                                                aktivitätGefunden = true;
                                                break;
                                            }
                                            catch (System.FormatException)
                                            {
                                                FehlEingabe();
                                            }
                                        } while (true);
                                        break;
                                    }
                                }
                            }
                            else if (i == 11)
                            {
                                aktivitätGefunden = false;
                            }
                        }
                        break;
                    } while (aktivitätGefunden == true);
                    Console.WriteLine("Willst du noch mehr Aktivitäten eintragen: [ja|nein]");
                    weitereAktivitäten = Console.ReadLine();
                    if (weitereAktivitäten == "nein")
                    {
                        break;
                    }
                } while (true);
            }
        }

        static void Ernährungsplan()
        {
            Console.WriteLine("Brauchen Sie einen Ernährungsplan?: [ja|nein]");
            string Ernährungsplan = Convert.ToString(Console.ReadLine());
            if (Ernährungsplan.ToLower() == "ja")
            {
                Console.WriteLine("Möchten sie Zunehmen oder Abnehmen?");
                string BulkCut = Convert.ToString(Console.ReadLine());

                if (BulkCut.ToLower() == "zunehmen")
                {
                    Console.WriteLine("Hier findest du einen guten Ernährungspläne zum zunehmen!");
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine("Montag");
                    Console.WriteLine("Frühstück:  Speisequark, Magerstufe (0,3% Fett)\r\n gemahlene Mandeln\r\n Milch (1,5% Fett)\r\n Eier, mittelgroß\r\n Proteinpulver Vanille\r\n½ Pck. Backpulver");
                    Console.WriteLine("Nährwerte (3 Waffeln): Kalorien: 527 kcal\r\nFett: 15,1 g\r\nKohlenhydrate: 16,5 g\r\nEiweiß: 73,3 g ");
                    Console.WriteLine("Mittag:  Nudeln, Hähnchenbrust, gemischtes Gemüse, Curry-Erdnussbutter Sauce ");
                    Console.WriteLine("Nährwerte(wenn von allem 100g): 1117 kcal\r\n Kohlenhydrate: 64.46g\r\n  Fett: 70.52g\r\n Eiweiss: 57.33g\r\n  ");
                    Console.WriteLine("Snack: 400 ml Milch oder Milchalternative.\r\n100 g Sojaflocken.\r\n150 g Magerquark.\r\n1 reife Banane (auch lecker: Beeren aller Art)\r\n1 EL Nussmus (Erdnuss / Mandel)\r\n40 g Whey (z.B. Geschmack nach Belieben)");
                    Console.WriteLine("Nährwerte: 1108.3 kcal400.\r\n Eiweiss: 92.88g.\r\n Kohlenhydrate: 68.85g.\r\n Fett: 50.11g\r\n");
                    Console.WriteLine("Abendessen: Hühnchen.\r\n Reis.\r\n Brokkoli.\r\n ");
                    Console.WriteLine("Nährwerte(wenn alles 100g): 481 kcal.\r\n Eiweiss: 33g.\r\n Kohlenhydrate: 80.5g.\r\n Fett: 2.7g ");
                    Console.WriteLine("Snack: (Je nach bedarf) Milch.\r\n und (Je nach Bedarf) Whey Protein.\r\n");
                    Console.WriteLine("Nährwerte(wenn Milch 100 ml und Whey 100g): 428 kcal.\r\n Kohlenhydrate: 12.2g.\r\n Eiweiss: 82.2g.\r\n Fett: 6.3g.\r\n  ");

                    Console.WriteLine("Dienstag");
                    Console.WriteLine("Mittwoch");
                    Console.WriteLine("Donnerstag");
                    Console.WriteLine("Freitag");
                    Console.WriteLine("Samstag");
                    Console.WriteLine("Sonntag");

                    Console.WriteLine("-----------------------------------------------------------------------------");

                }
                else
                {
                    Console.WriteLine("Hier findest du einen guten Ernährungspläne zum Abnehmen!");
                }
            }
        }

        static void FehlEingabe()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fehlerhafte Eingabe");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}