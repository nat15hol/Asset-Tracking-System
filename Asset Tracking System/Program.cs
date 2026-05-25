using System;
using System.Collections.Generic;
using WeeklyMiniProject3;

class Program
{
    static void Main()
    {
        LiveCurrency.FetchRates();
        AssetManager manager = new AssetManager();

        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("=== ASSET TRACKING SYSTEM ===");
            Console.WriteLine("1. Add Asset");
            Console.WriteLine("2. Show Assets");
            Console.WriteLine("3. Show Assets (Sorted by date)");
            Console.WriteLine("4. Exit");

            Console.Write("\nSelect option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.AddAsset();
                    break;

                case "2":
                    manager.ShowAssets();
                    break;

                case "3":
                    manager.ShowSortedAssets();
                    break;

                case "4":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}

class Asset
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public double PriceLocal { get; set; }
    public double PriceUSD { get; set; }
    public string Office { get; set; }
    public string Status { get; set; }
}
// double result = LiveCurrency.Convert(50, "SEK", "USD");
class AssetManager
{
    private List<Asset> assets = new List<Asset>();

    public void AddAsset()
    {
        Asset asset = new Asset();

        Console.Write("Type: ");
        asset.Type = Console.ReadLine();

        Console.Write("Brand: ");
        asset.Brand = Console.ReadLine();

        Console.Write("Model: ");
        asset.Model = Console.ReadLine();

        Console.Write("Office: ");
        asset.Office = Console.ReadLine();

        Console.Write("Purchase Date (yyyy-mm-dd): ");
        asset.PurchaseDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Price in SEK: ");
        asset.PriceLocal = double.Parse(Console.ReadLine());

        asset.PriceUSD = Math.Round(
            LiveCurrency.Convert(asset.PriceLocal, "SEK", "USD"),
            2
        );

        // asset.Status = DateTime.Parse();

        assets.Add(asset);

        Console.WriteLine("\nAsset added successfully.");
    }

    public void ShowAssets()
    {
        Console.WriteLine("\n=== ASSETS ===\n");

        foreach (Asset asset in assets)
        {
            DateTime endOfLife = asset.PurchaseDate.AddYears(3);
            TimeSpan remaining = endOfLife - DateTime.Now;
            if (remaining.TotalDays < 90)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                asset.Status = "Critical";
            }
            else if (remaining.TotalDays < 180)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                asset.Status = "Approaching";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                asset.Status = "OK";
            }
            Console.WriteLine(
                $"{asset.Type,-15} | " +
                $"{asset.Brand,-15} | " +
                $"{asset.Model,-20} | " +
                $"{asset.Office,-10} | " +
                $"{asset.PriceLocal,-10} | " +
                $"{asset.PriceUSD,-10} USD | " +
                $"{asset.PurchaseDate.ToShortDateString(),-12} | " +
                $"{asset.Status,-10}"
            );
            Console.ResetColor();
        }
    }
    public void ShowSortedAssets()
    {
        Console.WriteLine("\n=== SORTED ASSETS ===\n");

        var sortedAssets = assets
            .OrderBy(a => a.PurchaseDate)
            .ToList();

        foreach (Asset asset in sortedAssets)
        {
            DateTime endOfLife = asset.PurchaseDate.AddYears(3);
            TimeSpan remaining = endOfLife - DateTime.Now;
            if (remaining.TotalDays < 90)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (remaining.TotalDays < 180)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine(
                $"{asset.Type,-15} | " +
                $"{asset.Brand,-15} | " +
                $"{asset.Model,-20} | " +
                $"{asset.Office,-10} | " +
                $"{asset.PriceLocal,-10} | " +
                $"{asset.PriceUSD,-10} USD | " +
                $"{asset.PurchaseDate.ToShortDateString(),-12}"
            );
            Console.ResetColor();
        }
    }
}




class CurrencyObj
{
    public string CurrencyCode { get; set; }
    public double ExchangeRateFromEUR { get; set; }

    public CurrencyObj(string currencyCode, double exchangeRateFromEUR)
    {
        CurrencyCode = currencyCode;
        ExchangeRateFromEUR = exchangeRateFromEUR;
    }
}