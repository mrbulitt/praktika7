using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace практика7
{
    internal class Program
    {
        static Dictionary<string, List<double>> transact = new Dictionary<string, List<double>>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Добро пожаловать в программу для управления финансами");
                Console.WriteLine("1. Добавить доход/расход");
                Console.WriteLine("2. Показать отчет");
                Console.WriteLine("3. Рассчитать баланс");
                Console.WriteLine("4. Рассчитать средние траты");
                Console.WriteLine("5. Прогноз на следующий месяц");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransaction();
                        break;

                    case "2":
                        PrintFinanceReport();
                        break;

                    case "3":
                        CalculateBalance();
                        break;

                    case "4":
                        GetAverageExpense();
                        break;

                    case "5":
                        PredictNextMonthExpenses();
                        break;

                    case "6":
                        PrintStatistics();
                        break;

                    case "7":
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        public static void AddTransaction()
        {
            Console.Write("Введите категорию (Доход, Продукты, Транспорт, Развлечения): ");
            string category = Console.ReadLine();

            Console.Write("Введите сумму: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (!transact.ContainsKey(category))
                {
                    transact[category] = new List<double>();
                }
                transact[category].Add(amount);
                Console.WriteLine($"Запись добавлена: Категория - {category}, Сумма - {amount}");
            }
            else
            {
                Console.WriteLine("Неверный формат суммы.");
            }

        }
        public static void PrintFinanceReport()
        {
            Console.WriteLine("\nОтчет о доходах и расходах:");
            foreach (var entry in transact)
            {
                Console.Write(entry.Key + ": ");
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    Console.Write(entry.Value[i]);
                    if (i < entry.Value.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void CalculateBalance()
        {
            double income = 0, expenses = 0;

            foreach (var entry in transact)
            {
                if (entry.Key.Equals("Доход"))
                {
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        income += entry.Value[i];
                    }
                }
                else
                {
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        expenses += entry.Value[i];
                    }
                }
            }

            double balance = income - expenses;
            Console.WriteLine($"\nТекущий баланс: {balance}");
        }
        public static void GetAverageExpense()
        {
            Console.Write("Введите категорию для расчета средних трат: ");
            string category = Console.ReadLine();

            if (transact.ContainsKey(category) && transact[category].Count > 0)
            {
                double sum = 0;
                int count = transact[category].Count;

                for (int i = 0; i < count; i++)
                {
                    sum += transact[category][i];
                }

                double average = sum / count;
                Console.WriteLine($"\nСредние траты в категории \"{category}\": {average}");
            }
            else
            {
                Console.WriteLine($"\nНет данных для категории \"{category}\".");
            }
        }
        public static void PredictNextMonthExpenses()
        {
            Console.WriteLine("\nПрогноз расходов на следующий месяц:");

            foreach (var entry in transact)
            {
                if (!entry.Key.Equals("Доход") && entry.Value.Count > 0)
                {
                    double sum = 0;
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        sum += entry.Value[i];
                    }

                    double average = sum / entry.Value.Count;
                    Console.WriteLine($"{entry.Key}: {average * 4} (предполагая 4 недели)");
                }
            }
        }
        public static void PrintStatistics()
        {
            Console.WriteLine("\nСтатистика расходов:");

            double totalExpenses = 0;
            string mostExpensiveCategory = "";
            double maxExpenseAmount = 0;
            string mostFrequentCategory = "";
            int maxFrequency = 0;

            foreach (var entry in transact)
            {
                if (!entry.Key.Equals("Доход"))
                {
                    double categoryTotal = 0;
                    for (int i = 0; i < entry.Value.Count; i++)
                    {
                        categoryTotal += entry.Value[i];
                    }

                    totalExpenses += categoryTotal;

                    if (categoryTotal > maxExpenseAmount)
                    {
                        maxExpenseAmount = categoryTotal;
                        mostExpensiveCategory = entry.Key;
                    }

                    if (entry.Value.Count > maxFrequency)
                    {
                        maxFrequency = entry.Value.Count;
                        mostFrequentCategory = entry.Key;
                    }

                    double percentage = (categoryTotal / totalExpenses) * 100;
                    Console.WriteLine($"{entry.Key}: {categoryTotal} ({percentage:F2}%)");
                }
            }
            Console.WriteLine($"\nОбщая сумма расходов: {totalExpenses}");
            Console.WriteLine($"Самая затратная категория: {mostExpensiveCategory}");
            Console.WriteLine($"Самая популярная категория: {mostFrequentCategory}");
        }
        
    }
}
