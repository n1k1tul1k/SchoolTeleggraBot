using System;
using System.IO;

namespace LessonsAutoFill
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = string.Empty;
            var localI = 0;
            data += "\"Id\",\"Name\",\"DateOfWeek\",\"LessonNumber\",\"ClassName\"\r\n";
            var pathToLessons = Path.Combine(@"C:\Users\bitddeveloper\OneDrive\Рабочий стол", "Lessons");
            foreach (var file in Directory.GetFiles(pathToLessons))
            {
                var dateOfWeek = Path.GetFileName(file).Split(' ')[0];
                var group = Path.GetFileName(file).Split(' ')[1].Split('.')[0];
                int i = 1;
                foreach (var lessonName in File.ReadAllLines(file))
                {
                    data += $"{localI++},{lessonName},{dateOfWeek},{i++},{group}\r\n";
                }
            }

            File.AppendAllText("test.csv", data);
        }
    }
}