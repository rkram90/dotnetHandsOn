using HelloWorld.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDotnetAPI.Models;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DateTime now = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine($"DateTime Now is: {now}");

            Computer myComputer = new Computer()
            {
                Motherboard = "ASUS ROG",
                CPUCores = 8,
                HasWife = true,
                HasLTE = false,
                ReleaseDate = new DateTime(2022, 5, 1),
                Price = 1299.99M,
                VideoCard = "NVIDIA RTX 3080"
            };

            string sqlInsert = @"
                INSERT INTO TutorialAppSchema.Computer (
                    Motherboard, CPUCores, HasWife, HasLTE, ReleaseDate, Price, VideoCard
                ) VALUES (
                    @Motherboard, @CPUCores, @HasWife, @HasLTE, @ReleaseDate, @Price, @VideoCard
                );
            ";


            //doOperationInDapper(sqlInsert, myComputer, dapper);
            displayComputerList(dapper);
            //doOperationInEF(config);
        }

        public static void displayComputerList(DataContextDapper dp)
        {
            var computers = dp.LoadData<Computer>("SELECT * FROM  TutorialAppSchema.Computer");

            if (computers != null)
            {
                foreach (var computer in computers)
                {
                    computer.showDetails();
                }
            }
            else
            {
                Console.WriteLine("No computers found.");
            }
        }

        public static void doOperationInDapper(string sqlQuery, Computer myComputer, DataContextDapper dp)
        {
            int isInserted = dp.ExecuteSqlWithCount(sqlQuery, myComputer);
            Console.WriteLine($"Inserted row count is: {isInserted}");
        }

        public static void doOperationInEF(IConfiguration config)
        {
            DataContextEF entityFramework = new DataContextEF(config);

            //Insert
            Computer myComputer = new Computer()
            {
                Motherboard = "SAMSUNG NEW ROG",
                CPUCores = 12,
                HasWife = true,
                HasLTE = true,
                ReleaseDate = new DateTime(2022, 5, 1),
                Price = 1399.99M,
                VideoCard = "NVIDIA RTX 3085"
            };
            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            //list all the data
            Console.WriteLine("Data from Entity Frame work!!!.");
            IEnumerable<Computer>? computers = entityFramework.Computer?.ToList<Computer>();
            if (computers != null)
            {
                foreach (var computer in computers)
                {
                    computer.showDetails();
                }
            }
            else
            {
                Console.WriteLine("No computers found.");
            }
        }
    }
}