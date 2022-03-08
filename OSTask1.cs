using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;


namespace OSTask1
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Task 1:");
            Console.ReadLine();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }


            Console.Write("Task 2:");
            Console.Read();
            string path = @"C:\OS1";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                Console.WriteLine("Папка успешно создана");
            }
            else
            {
                Console.WriteLine("Папка уже существует");
            }
            Console.Read();
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();
            using (FileStream fstream = new FileStream($@"{path}\test.txt", FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                await fstream.WriteAsync(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
            using (FileStream fstream = File.OpenRead($@"{path}\test.txt"))
            {
                byte[] array = new byte[fstream.Length];
                await fstream.ReadAsync(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
            File.Delete($@"{path}\test.txt");
            Console.WriteLine("Файл test.txt удален");
            Console.WriteLine();


            Console.Write("Task 3:");
            Console.Read();
            using (FileStream fstream = new FileStream($@"{path}\user.json", FileMode.OpenOrCreate))
            {
                Person Ben = new Person() { Name = "Tom", Age = 35 };
                await JsonSerializer.SerializeAsync<Person>(fstream, Ben);
                Console.WriteLine("Файл создан и содержит данные");
            }
            using (FileStream fstream = File.OpenRead($@"{path}\user.json"))
            {
                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fstream);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }
            File.Delete($@"{path}\user.json");
            Console.WriteLine("Файл user.json удален");
            Console.WriteLine();
            Console.Read();


            Console.Write("Task 4:");
            Console.Read();
            Console.WriteLine();
            Console.Read();
            XDocument xdoc = new XDocument(new XElement("people",
                new XElement("person",
                    new XAttribute("name", "Thomas"),
                    new XElement("company", "Apple"),
                    new XElement("age", 41)),
                new XElement("person",
                    new XAttribute("name", "Brad"),
                    new XElement("company", "Sony"),
                    new XElement("age", 27))));
            xdoc.Save($@"{path}\people.xml");
            Console.WriteLine("people.xml created");
            Console.WriteLine("Введите имя для добавления в файл:");
            string tempname = Console.ReadLine();
            Console.WriteLine("Введите компанию для добавления в файл:");
            string tempcompany = Console.ReadLine();
            Console.WriteLine("Введите возраст для добавления в файл:");
            string tempage = Console.ReadLine();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($@"{path}\people.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement personElem = xDoc.CreateElement("people");
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            XmlElement companyElem = xDoc.CreateElement("company");
            XmlElement ageElem = xDoc.CreateElement("age");
            XmlText nameText = xDoc.CreateTextNode(tempname);
            XmlText companyText = xDoc.CreateTextNode(tempcompany);
            XmlText ageText = xDoc.CreateTextNode(tempage);
            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            ageElem.AppendChild(ageText);
            personElem.Attributes.Append(nameAttr);
            personElem.AppendChild(companyElem);
            personElem.AppendChild(ageElem);
            xRoot.AppendChild(personElem);
            xDoc.Save($@"{path}\people.xml");
            Console.WriteLine("people.xml edited\n");
            XmlDocument xxDoc = new XmlDocument();
            xDoc.Load($@"{path}\people.xml");
            XmlElement xxRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    if (attr != null)
                        Console.WriteLine(attr.Value);
                }
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "company")
                    {
                        Console.WriteLine($"Компания: {childnode.InnerText}");
                    }
                    if (childnode.Name == "age")
                    {
                        Console.WriteLine($"Возраст: {childnode.InnerText}");
                    }
                }
                Console.WriteLine();
            }
            File.Delete($@"{path}\people.xml");
            Console.WriteLine("Файл people.xml удален");
            Console.WriteLine();


            Console.Write("Task 5:");
            Console.Read();
            Console.WriteLine();
            string somepath = @"C:\OS1\zip";
            DirectoryInfo dirInfoo = new DirectoryInfo(somepath);
            if (!dirInfoo.Exists)
            {
                dirInfoo.Create();
            }
            Console.Read();
            using (FileStream fstream = new FileStream($@"{path}\test2.txt", FileMode.CreateNew)) { }
            string sourceFolder = @"C:\OS1\zip\";
            string zipFile = @"C:\OS1\zip.zip";
            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
            Console.WriteLine($"Папка {sourceFolder} создана и конвертирована в архив {zipFile}");
            Console.Read();
            using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Update))
            {
                zipArchive.CreateEntryFromFile(@"C:\OS1\test2.txt", "test2.txt");
            }
            Console.Read();
            Console.Write($"test2.txt добавлен в архив {zipFile}\n");
            Console.Read();
            ZipFile.ExtractToDirectory(zipFile, sourceFolder);
            Console.WriteLine($"Архив {zipFile} распакован в папку {sourceFolder}");
            Console.WriteLine();
            Console.Read();
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
            Directory.Delete(sourceFolder, true);
            Console.WriteLine("Файлы из Task 5 удалены");
            Console.Read();
            Directory.Delete(path, true);
            Console.WriteLine("OS1 удалена");
        }
    }
}
