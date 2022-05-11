using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.IO.Compression;
namespace OC_LAB06
{
    class Program
    {
        static string kb1 = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS";
        static string path = @"D:\Documents";
        static void Main(string[] args)
        {
        mark:
            Console.WriteLine("Выбирите действие(1-3): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    First();
                    goto mark;
                case "2":
                    Second();
                    goto mark;
                case "3":
                    Third();
                    goto mark;
                default:
                    break;

            }
        }
        static void First()
        {
            // запись в файл
            using (FileStream fstream = new FileStream($"{path}\\note1.txt", FileMode.OpenOrCreate)) { }
            using (FileStream fstream = new FileStream($"{path}\\note2.txt", FileMode.OpenOrCreate)) { }
            using (FileStream fstream = new FileStream($"{path}\\buff.txt", FileMode.OpenOrCreate)) { }
        }
        static void Second()
        {

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            Console.WriteLine("Введите колл-во кб для записи:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(n);
            string data ="s" ;
            for (int i = 0; i < n; i++)
            {
                data = data + kb1;
            }
            
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "buff.txt"), true))
            {
                

                outputFile.WriteLine(data);
                
            }
            
            FileInfo fileInfo1 = new FileInfo($"{path}\\buff.txt");
            FileInfo fileInfo2 = new FileInfo($"{path}\\note1.txt");
            FileInfo fileInfo3 = new FileInfo($"{path}\\note2.txt");
            int len1 = (int)fileInfo1.Length;
            int len2 = (int)fileInfo2.Length;
            int len3 = (int)fileInfo3.Length;
            if ((len1 + len2 + len3) <= 65535)
            {
                if ((len1 + len2) >= 32768)
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "note2.txt"), true))
                    {
                        outputFile.WriteLine(data);
                        Console.WriteLine("Текст записан в файл note2.txt");

                    }

                }
                else
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "note1.txt"), true))
                    {
                        outputFile.WriteLine(data);
                        Console.WriteLine("Текст записан в файл note1.txt");


                    }
                }
                // удаление файла
            }
            else
            {
                Console.WriteLine("Память заполнена");
            }
            Console.WriteLine("Нажмите для продолжения");
            Console.ReadLine();

        }
        static void Third()
        {
            FileInfo fileInfo1 = new FileInfo($"{path}\\note1.txt");
            FileInfo fileInfo2 = new FileInfo($"{path}\\note2.txt");
            FileInfo fileInfo3 = new FileInfo($"{path}\\buff.txt");
            if (fileInfo1.Exists)
            {
                Console.WriteLine($"Имя файла: {fileInfo1.Name}");
                Console.WriteLine($"Размер: {fileInfo1.Length}");
            }
            if (fileInfo2.Exists)
            {
                Console.WriteLine($"Имя файла: {fileInfo2.Name}");
                Console.WriteLine($"Размер: {fileInfo2.Length}");
            }
            if (fileInfo3.Exists)
            {
                Console.WriteLine($"Имя файла: {fileInfo3.Name}");
                Console.WriteLine($"Размер: {fileInfo3.Length}");
            }
        }
    }

}
