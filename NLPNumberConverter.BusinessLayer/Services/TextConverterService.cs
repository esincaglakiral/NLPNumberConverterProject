using NLPNumberConverter.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPNumberConverter.BusinessLayer.Services
{
    public class TextConverterService : ITextConverterService
    {
        public ConvertedText ConvertText(UserText userText)
        {
            // Giriş kontrolü
            if (string.IsNullOrEmpty(userText.InputText))
            {
                return new ConvertedText { OutputText = "Geçerli bir metin giriniz." };
            }

            // Metni küçük harfe çevirme ve işlem yapma
            string inputText = userText.InputText.ToLower();
            string outputText = ConvertWordsToNumbers(inputText);

            return new ConvertedText { OutputText = outputText };
        }


        private string ConvertWordsToNumbers(string text)
        {
            // Sayı kelimeleri ile eşleşen sayısal değerler
            Dictionary<string, int> numberWords = new Dictionary<string, int>
    {
        { "sıfır", 0 }, { "bir", 1 }, { "iki", 2 }, { "üç", 3 }, { "dört", 4 },
        { "beş", 5 }, { "altı", 6 }, { "yedi", 7 }, { "sekiz", 8 }, { "dokuz", 9 },
        { "on", 10 }, { "yirmi", 20 }, { "otuz", 30 }, { "kırk", 40 },
        { "elli", 50 }, { "altmış", 60 }, { "yetmiş", 70 }, { "seksen", 80 },
        { "doksan", 90 }, { "yüz", 100 }, { "bin", 1000 }, { "milyon", 1000000 }
    };

            // Birleşik kelimeleri ayıralım
            text = SeparateCompoundNumbers(text, numberWords.Keys.ToList());

            int total = 0;  // Toplam sayıyı tutacak
            int currentNumber = 0;  // O anki sayıyı biriktirmek için
            int multiplier = 1;  // Bin veya milyon gibi çarpanları takip edecek
            bool foundNumber = false;  // Sayı kelimesi bulunduğunda takip etmek için
            string processedText = "";

            // Cümleyi kelimelerine ayır
            var words = text.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string cleanWord = word.ToLower();  // Küçük harfe çeviriyoruz

                if (numberWords.ContainsKey(cleanWord))
                {
                    int numberValue = numberWords[cleanWord];

                    // "yüz", "bin", "milyon" gibi çarpan kelimeler için işlem yapıyoruz
                    if (cleanWord == "yüz")
                    {
                        if (currentNumber == 0) currentNumber = 1;  // "yüz" kelimesi tek başına ise 100 olarak kabul ediliyor
                        currentNumber *= numberValue;  // Çarpıyoruz
                    }
                    else if (cleanWord == "bin" || cleanWord == "milyon")
                    {
                        if (currentNumber == 0) currentNumber = 1;  // Öncesinde bir sayı yoksa 1 olarak kabul ediyoruz
                        total += currentNumber * numberValue;  // Çarpan eklenir
                        currentNumber = 0;  // Sayıyı sıfırla
                    }
                    else
                    {
                        currentNumber += numberValue;  // Diğer durumlarda sayıyı topluyoruz
                    }

                    foundNumber = true;  // Sayı bulundu
                }
                else
                {
                    // Eğer sayı kelimesi değilse, birikmiş sayıyı ekliyoruz
                    if (foundNumber)
                    {
                        total += currentNumber;
                        processedText += total + " ";  // Biriken sayıyı ekle
                        total = 0;  // Biriken sayıyı sıfırla
                        currentNumber = 0;
                        foundNumber = false;
                    }

                    // Sayı olmayan kelimeyi olduğu gibi ekle
                    processedText += word + " ";
                }
            }

            // Eğer döngü sonunda birikmiş sayılar varsa onları da ekle
            if (foundNumber)
            {
                total += currentNumber;
                processedText += total;
            }

            return processedText.Trim();
        }



        // Birleşik sayı kelimelerini ayıran metot
        private string SeparateCompoundNumbers(string text, List<string> numberWords)
        {
            foreach (var numberWord in numberWords.OrderByDescending(nw => nw.Length)) // Uzun kelimeler önce kontrol edilsin
            {
                // Eğer metinde birleşik sayı kelimesi varsa, onu ayır
                text = text.Replace(numberWord, " " + numberWord + " ");
            }
            return text;
        }


    }
}
