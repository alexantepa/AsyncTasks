using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncTasks
{
    public class Generators
    {
        public async static Task<string> GenerateText(int stringCount)
        {
            string saveText = string.Empty;

            for (int i = 0; i < stringCount; i++)
            {
                await Task.Delay(1);
                saveText += "[" + i + "] Тустовая строка\n";
            }
            return saveText;
        }

        public async static Task<string> ExportFromFile (string path)
        {
            if (!File.Exists(path)) 
                return string.Empty;

            return await Task.Run(() => File.ReadAllText(path));
        }

        public async Task ExportToFile (string path, string text)
        {
            if (!File.Exists(path))
                MessageBox.Show("Такого файла нет!");

            await Task.Run(() => File.WriteAllText(path, text));
        }
    }
}
