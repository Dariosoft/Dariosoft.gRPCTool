using Dariosoft.gRPCTool;

namespace iosoft.gRPCTool.Console.Utilities
{
    public class Menu(AssemblyRepository repository, IEngine engine)
    {
        public void Show()
        {
            while (true)
            {
                System.Console.Clear();
                DrawItems();
                var n = Input();
                if (n == -1) continue;
                if (n == 0) break;
                var filename = FindFullFileName(repository.Items[n - 1]);
                if (string.IsNullOrWhiteSpace(filename))
                    WarnFileNotFound(repository.Items[n - 1]);
                else
                {
                    engine.Start(filename);
                    InfoFinishGenerate(repository.Items[n - 1]);
                }
            }
        }

        private int Input()
        {
            return int.TryParse(System.Console.ReadLine(), out var num) && num <= repository.Items.Count
                ? num
                : -1;
        }

        private void DrawItems()
        {
            System.Console.Write("0. Exit");
            System.Console.WriteLine("-----------------------------------------");
            for (var i = 0; i < repository.Items.Count; i++)
            {
                System.Console.WriteLine("{0}. {1}", i + 1, repository.Items[i].AssemblyName);
            }

            System.Console.WriteLine("-----------------------------------------");
            System.Console.Write("Enter number of the assembly: ");
        }

        private void WarnFileNotFound(AssemblyRepositoryItem item)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No file is found for the assembly: {0}", item.AssemblyName);
            System.Console.WriteLine("\tat search directory: {0}", item.SearchPath);
            System.Console.ResetColor();
            System.Console.WriteLine("Press any key to skip this and continue...");
            System.Console.ReadKey();
        }

        private void InfoFinishGenerate(AssemblyRepositoryItem item)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("The process of the assembly '{0}' has been done.", item.AssemblyName);
            System.Console.ResetColor();
            System.Console.WriteLine("Press any key to skip this and continue...");
            System.Console.ReadKey();
        }

        private string? FindFullFileName(AssemblyRepositoryItem item)
        {
            try
            {
                return Directory.GetFiles(item.SearchPath, item.AssemblyName).FirstOrDefault();
            }
            catch
            {
                return null;
            }
            
        }
    }
}