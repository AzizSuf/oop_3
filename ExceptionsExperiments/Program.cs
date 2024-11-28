// Используя приведенный ниже готовый код, создайте консольное приложение и
// осуществите несколько экспериментов.
// 1. Запустите программу, введите значение i=0. Убедитесь, что программа работает, но
// во внешнем блоке не выводятся параметры исключения. Почему?
// 2. Закомментируйте блок catch (System.DivideByZeroException e).
// Убедитесь, что параметры исключения выводятся на печать. Опишите все приведенные
// параметры исключения.
// 3. Поставьте на первое место блок catch с общим исключением. Что произойдет?
// 4. Можно убрать все внутренние блоки catch? Можно ли оставить только блок Try?
// 5. Если исключение в блоке Try – Catch не поймано, означает ли это крах программы.
// Можно ли написать программу, которая никогда аварийно не завершается?

namespace ExceptionsExperiments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number: >");

            int i = 0;
            var input = Console.ReadLine();
            if (!int.TryParse(input, out i))
            {
                Console.WriteLine("Incorrect input format!");
            }

            try
            {
                try
                {
                    int x = 5;
                    int y = x / i;
                    Console.WriteLine("x={0}, y= {1}", x, y);
                }
                //catch (DivideByZeroException e)
                //{
                //    Console.WriteLine("Попытка деления на ноль", e.ToString());
                //}
                catch (FormatException e)
                {
                    Console.WriteLine("Введено не целое число! Исключение", e.ToString());
                }
                catch
                {
                    Console.WriteLine("Неизвестная ошибка. Перезапустите программу");
                    throw;
                }
                finally
                {
                    Console.WriteLine("Выполнили блок finally");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("external try-catch");
                Console.WriteLine($"0 {ex.Message}");
                Console.WriteLine($"1 {ex.StackTrace}");
                Console.WriteLine($"2 {ex.TargetSite}");
                Console.WriteLine($"3 {ex.InnerException}");
                Console.WriteLine($"4 {ex.Source}");
                Console.WriteLine($"5 {ex.Data}");
                Console.WriteLine($"6 {ex.HelpLink}");
            }
        }
    }
}
