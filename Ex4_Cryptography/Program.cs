using System.Security.Cryptography;
using System.Text;

// Задание 4. Подпишите отчет к работе №3 хэшем строки, состоящей из
// вашей фамилии, имени и отчества, написанных через запятые
// 1. Используйте алгоритм MD5 или (и) SHA256.
// 2. Подключите пространство имен using System.Security.Cryptography;
// 3.Преобразуйте строку ФИО в массив байтов (Encoding...);
// 4.Получите хэш в виде массива байтов (ComputeHash);
// 5.Преобразуйте хэш из массива в строку, состоящую из шестнадцатеричных
// символов (ToHexString());
// 6.Не забывайте про кодировку UTF8! C# использует Unicode!

namespace Ex4_Cryptography
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fullName = "Суфьянов Азиз Иззетович";
            Console.WriteLine(fullName);

            // Вычисление любого типа хеша для строки
            Console.WriteLine($"hash MD5: {HashingString(MD5.Create(), fullName)}");
            Console.WriteLine($"hash SHA256: {HashingString(SHA256.Create(), fullName)}");
            Console.WriteLine($"hash SHA1: {HashingString(SHA1.Create(), fullName)}");
            //Console.WriteLine($"hash 512: {HashingString(SHA512.Create(), fullName)}");

            Console.WriteLine();

            // Вычисление хеша без создания объекта MD5
            var h = MD5.HashData(Encoding.UTF8.GetBytes(fullName));
            Console.WriteLine($"hash: {Convert.ToHexString(h)}");
        }

        static string HashingString(HashAlgorithm hashAlgorithm, string str)
        {
            var stringBytes = Encoding.UTF8.GetBytes(str);
            var hash = hashAlgorithm.ComputeHash(stringBytes);
            var hashString = Convert.ToHexString(hash);
            return hashString;
        }
    }
}
