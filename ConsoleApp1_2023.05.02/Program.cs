using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

/*static void Main(string[] args)
{ */
//Console.WriteLine("Kérem a kategóriát!");
Console.WriteLine("Kérem a gyártót!");
    string kategoria  = Console.ReadLine();

    MySqlConnection adatbaziskapcsolat = new MySqlConnection("datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;");
    adatbaziskapcsolat.Open();

string SQLSelect = " SELECT * FROM Termékek " +
            " WHERE kategória LIKE 'Winchester%' AND Ár > " +
            " (SELECT MAX(Ár) FROM termékek " +
            " WHERE kategória LIKE 'Winchester%' " +
            $" GROUP BY gyártó HAVING gyártó='{kategoria}' )" +
            " ORDER BY Ár;";

/* string SQLSelect = "SELECT gyártó," +
    "COUNT(*) AS darabSzám, " +
    "MAX(ár) AS maxÁr, " +
    "AVG(ár) AS Átlag FROM termékek" +
    $" WHERE kategória = '{kategoria}'" +
    " GROUP BY Gyártó;"; */

    MySqlCommand SQLparancs = new MySqlCommand(SQLSelect, adatbaziskapcsolat);

    MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();

    while (eredmenyOlvaso.Read())
    {
    Console.Write(eredmenyOlvaso.GetString("Kategória").PadRight(20,'.'));
    Console.Write(eredmenyOlvaso.GetString("Gyártó").PadRight(15,'_'));
    Console.Write(eredmenyOlvaso.GetString("Név").PadRight(30, ' '));
    Console.Write(eredmenyOlvaso.GetString("Ár").PadRight(10, ' '));
    Console.WriteLine(eredmenyOlvaso.GetString("Készlet").PadRight(15,' '));

        /* Console.Write(eredmenyOlvaso.GetString("Gyártó").PadRight(30,'.'));
        Console.Write(eredmenyOlvaso.GetString("darabSzám").PadLeft(4, '_') + " db");
        Console.Write(eredmenyOlvaso.GetString("Átlag").PadLeft(20) + " Ft");
        string atlagAr = $"{eredmenyOlvaso.GetDouble("Átlag"):f1}";
        Console.WriteLine(atlagAr.PadLeft(15) + "Ft"); */
    }

    eredmenyOlvaso.Close();
    adatbaziskapcsolat .Close();
//}
