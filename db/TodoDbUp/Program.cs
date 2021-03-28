using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace TodoDbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = 
                args.FirstOrDefault() ?? 
                "Server=localhost;Port=3306;Database=todo;Uid=root;Pwd=mypassword;";

            EnsureDatabase.For.MySqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif                
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
