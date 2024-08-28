using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var transactions = DataStorage.LoadTransactions();  
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Harcama Takip Uygulaması");
            Console.WriteLine("1. Gelir Ekle");
            Console.WriteLine("2. Gider Ekle");
            Console.WriteLine("3. Raporla");
            Console.WriteLine("4. Gelir ve Gideri Hesapla");
            Console.WriteLine("5. Çıkış");
            Console.Write("Bir seçenek girin: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                AddTransaction(transactions, true);
            }
            else if (choice == "2")
            {
                AddTransaction(transactions, false);
            }
            else if (choice == "3")
            {
                Report(transactions);
            }
            else if (choice == "4")
            {
                CalculateTotal(transactions);
            }
            else if (choice == "5")
            {
                DataStorage.SaveTransactions(transactions);  // Program sonlandığında verileri kaydet
                Console.WriteLine("Programdan çıkılıyor, veriler kaydedildi.");
                return;  // Programı sonlandırır
            }
            else
            {
                Console.WriteLine("Geçersiz seçenek. Tekrar deneyin.");
            }
        }
    }

    static void AddTransaction(List<Transaction> transactions, bool isIncome)
    {
        Console.Write("Açıklama: ");
        var description = Console.ReadLine();
        Console.Write("Tutar: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Geçersiz tutar. İşlem eklenemedi.");
            return;
        }

        transactions.Add(new Transaction
        {
             id = transactions.Count > 0 ? transactions.Max(t => t.id) + 1 : 1,
            tarih = DateTime.Now,
            icerik = description,
            miktar = amount,
            gelir = isIncome
        });
        Console.WriteLine("İşlem eklendi.");
        Console.ReadKey();
    }

    static void Report(List<Transaction> transactions)
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("Henüz bir işlem eklenmemiş.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Tarih\t\tAçıklama\t\tTutar\t\tTür");
        Console.WriteLine("----------------------------------------------------------------");

        foreach (var transaction in transactions)
        {
            string type = transaction.gelir ? "Gelir" : "Gider";
            Console.WriteLine($"{transaction.tarih:yyyy-MM-dd HH:mm}\t{transaction.icerik}\t\t{transaction.miktar:C}\t\t{type}");
        }

        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("Rapor sonu. Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }


    static void CalculateTotal(List<Transaction> transactions)
    {
        var totalIncome = transactions.Where(t => t.gelir).Sum(t => t.miktar);
        var totalExpense = transactions.Where(t => !t.gelir).Sum(t => t.miktar);
        var netAmount = totalIncome - totalExpense;

        Console.WriteLine($"Toplam Gelir: {totalIncome:C}");
        Console.WriteLine($"Toplam Gider: {totalExpense:C}");
        Console.WriteLine($"Net Miktar: {netAmount:C}");
        Console.ReadKey();
    }
}
