using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace OSTask2
{

    class Program
    {
        static readonly int nHash = 3;
        static int nFoundPasswords;
        static Dictionary<string, string> dictPasswords;
        static char[] alphabet;
        static DateTime dateTimeBegin;
        
        static string GetHash(string str)
        {
            StringBuilder hash = new StringBuilder();
            using (SHA256 sha256 = SHA256Managed.Create())
            {
                Encoding enc = Encoding.ASCII;
                byte[] baResult = sha256.ComputeHash(enc.GetBytes(str));
                foreach (byte b in baResult)
                    hash.Append(b.ToString("x2"));
            }
            return hash.ToString();
        }

        static bool CompareHash(string password)
        {
            string hash = GetHash(password);
            if (dictPasswords.ContainsKey(hash))
            {
                dictPasswords[hash] = password.ToString();
                nFoundPasswords++;
                Console.WriteLine("Хэш-значение: " + hash + "\nПароль: " + dictPasswords[hash]);
                return true;
            }
            else
                return false;
        }

        static void NextSequence(object beginEnd)
        {
            var temp = (char[])beginEnd;
            string sequence;
            for (char i1 = temp[0]; i1 <= temp[1]; i1++)
                foreach (var i2 in alphabet)
                    foreach (var i3 in alphabet)
                        foreach (var i4 in alphabet)
                            foreach (var i5 in alphabet)
                            {
                                sequence = string.Concat(i1, i2, i3, i4, i5);
                                if (CompareHash(sequence))
                                    Console.WriteLine("На подбор пароля затрачено " + Math.Round((DateTime.Now - dateTimeBegin).TotalSeconds) + " сек");                                
                            }
            Console.WriteLine("Поток " + temp[2] + " завершил работу");
        }

        static void Main(string[] args)
        {
            nFoundPasswords = 0;
            alphabet = new char[26];
            for (char i = 'a'; i <= 'z'; i++)
                alphabet[i - 'a'] = i;
            dictPasswords = new Dictionary<string, string>();
            dictPasswords.Add("1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", "");
            dictPasswords.Add("3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", "");
            dictPasswords.Add("74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f", "");
            dateTimeBegin = DateTime.Now;
            Console.WriteLine("Введите количество потоков:");
            string s = Console.ReadLine();
            switch (s)
            {
                case "1":
                    Thread threadBruteForcePart1 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart1.Start(new char[] { 'a', 'z', '1' });
                    break;

                case "2":
                    Thread threadBruteForcePart12 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart22 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart12.Start(new char[] { 'a', 'm', '1' });
                    threadBruteForcePart22.Start(new char[] { 'n', 'z', '2' });
                    break;

                case "3":
                    Thread threadBruteForcePart13 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart23 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart33 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart13.Start(new char[] { 'a', 'i', '1' });
                    threadBruteForcePart23.Start(new char[] { 'j', 'r', '2' });
                    threadBruteForcePart33.Start(new char[] { 's', 'z', '3' });
                    break;

                case "4":
                    Thread threadBruteForcePart14 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart24 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart34 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart44 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart14.Start(new char[] { 'a', 'g', '1' });
                    threadBruteForcePart24.Start(new char[] { 'h', 'n', '2' });
                    threadBruteForcePart34.Start(new char[] { 'o', 't', '3' });
                    threadBruteForcePart44.Start(new char[] { 'u', 'z', '4' });
                    break;

                case "5":
                    Thread threadBruteForcePart15 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart25 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart35 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart45 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart55 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart15.Start(new char[] { 'a', 'e', '1' });
                    threadBruteForcePart25.Start(new char[] { 'f', 'j', '2' });
                    threadBruteForcePart35.Start(new char[] { 'k', 'o', '3' });
                    threadBruteForcePart45.Start(new char[] { 'p', 'u', '4' });
                    threadBruteForcePart55.Start(new char[] { 'v', 'z', '5' });
                    break;

                case "6":
                    Thread threadBruteForcePart16 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart26 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart36 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart46 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart56 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart66 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart16.Start(new char[] { 'a', 'e', '1' });
                    threadBruteForcePart26.Start(new char[] { 'f', 'j', '2' });
                    threadBruteForcePart36.Start(new char[] { 'k', 'n', '3' });
                    threadBruteForcePart46.Start(new char[] { 'o', 'r', '4' });
                    threadBruteForcePart56.Start(new char[] { 's', 'v', '5' });
                    threadBruteForcePart66.Start(new char[] { 'w', 'z', '6' });
                    break;

                case "7":
                    Thread threadBruteForcePart17 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart27 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart37 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart47 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart57 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart67 = new Thread(new ParameterizedThreadStart(NextSequence));
                    Thread threadBruteForcePart77 = new Thread(new ParameterizedThreadStart(NextSequence));
                    threadBruteForcePart17.Start(new char[] { 'a', 'd', '1' });
                    threadBruteForcePart27.Start(new char[] { 'e', 'h', '2' });
                    threadBruteForcePart37.Start(new char[] { 'i', 'l', '3' });
                    threadBruteForcePart47.Start(new char[] { 'm', 'p', '4' });
                    threadBruteForcePart57.Start(new char[] { 'q', 't', '5' });
                    threadBruteForcePart67.Start(new char[] { 'u', 'w', '6' });
                    threadBruteForcePart77.Start(new char[] { 'x', 'z', '7' });
                    break;
            }
            Console.WriteLine("Запущено " + s + " потоков, ожидайте выполнения");
            Console.ReadKey();
        }
    }
}
