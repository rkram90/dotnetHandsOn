namespace MyDotnetAPI.Models
{

    public class Computer
    {
        public int ComputerId { get; set; }
        public string Motherboard { get; set; }
        public int CPUCores { get; set; }
        public bool HasWife { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; }

        public Computer()
        {
            if (Motherboard == null)
            {
                Motherboard = "";
            }
            if (VideoCard == null)
            {
                VideoCard = "";
            }
        }

        public void showDetails()
        {
            Console.WriteLine($"Computer ID is ${ComputerId}");
            Console.WriteLine($"Motherboad is ${Motherboard}");
            Console.WriteLine($"CPUCores is ${CPUCores}");
            Console.WriteLine($"HasWife is ${HasWife}");
            Console.WriteLine($"HasLTE is ${HasLTE}");
            Console.WriteLine($"Price is ${Price}");
        }
    }
}