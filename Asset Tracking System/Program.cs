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
            Console.WriteLine("3. Exit");

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

        assets.Add(asset);

        Console.WriteLine("\nAsset added successfully.");
    }
    
    public void ShowAssets()
    {
        Console.WriteLine("\n=== ASSETS ===\n");

        foreach (Asset asset in assets)
        {
            Console.WriteLine(
                $"{asset.Type,-15} | " +
                $"{asset.Brand,-15} | " +
                $"{asset.Model,-20} | " +
                $"{asset.Office,-10} | " +
                $"{asset.PriceLocal,-10} | " +
                $"{asset.PriceUSD,-10} USD | " +
                $"{asset.PurchaseDate.ToShortDateString(),-12}"
            );
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