using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class DataStorage
{
    private static string filePath = "transactions.json";

    public static void SaveTransactions(List<Transaction> transactions)
    {
        try
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            Console.WriteLine("Veriler başarıyla kaydedildi.");  // Hata ayıklama mesajı
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veriler kaydedilirken bir hata oluştu: {ex.Message}");
        }
    }

    public static List<Transaction> LoadTransactions()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Veri dosyası bulunamadı, yeni bir dosya oluşturulacak.");
                return new List<Transaction>();
            }

            var json = File.ReadAllText(filePath);
            var transactions = JsonSerializer.Deserialize<List<Transaction>>(json);
            Console.WriteLine("Veriler başarıyla yüklendi.");  // Hata ayıklama mesajı
            return transactions ?? new List<Transaction>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veriler yüklenirken bir hata oluştu: {ex.Message}");
            return new List<Transaction>();
        }
    }
}


