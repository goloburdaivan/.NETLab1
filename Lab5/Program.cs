using System;
using Lab5.Decorators;
using Lab5.Products;


public class Program
{
    public static void Main(string[] args)
    {
        Product processor = new Processor
        {
            Name = "Intel Core i7",
            SKU = "INTEL-I7",
            Price = 300.00m,
            ClockSpeed = 3.8f,
            CoreCount = 8
        };

        Product videoCard = new VideoCard
        {
            Name = "NVIDIA RTX 3080",
            SKU = "RTX-3080",
            Price = 700.00m,
            MemorySize = 10,
            MemoryType = "GDDR6X"
        };

        Product ram = new RAM
        {
            Name = "Corsair Vengeance LPX",
            SKU = "COR-VLPX",
            Price = 150.00m,
            Capacity = 16,
            Speed = 3200
        };

        Product motherboard = new Motherboard
        {
            Name = "ASUS ROG STRIX Z490-E",
            SKU = "ASUS-Z490E",
            Price = 250.00m,
            FormFactor = "ATX",
            Chipset = "Z490"
        };

        Product assembledSystemUnit = new AssembledSystemUnit(
            new List<Product> { processor, motherboard, ram, videoCard }
        );

        Product fullComputer = new FullComputer(assembledSystemUnit);

        Console.WriteLine($"Product: {processor.GetDescription()} - Price: ${processor.GetPrice()}");
        Console.WriteLine($"Product: {videoCard.GetDescription()} - Price: ${videoCard.GetPrice()}");
        Console.WriteLine($"Product: {ram.GetDescription()} - Price: ${ram.GetPrice()}");
        Console.WriteLine($"Product: {motherboard.GetDescription()} - Price: ${motherboard.GetPrice()}");
        Console.WriteLine($"Assembled System Unit: {assembledSystemUnit.GetDescription()} - Price: ${assembledSystemUnit.GetPrice()}");
        Console.WriteLine($"Full Computer: {fullComputer.GetDescription()} - Price: ${fullComputer.GetPrice()}");
    }
}
        