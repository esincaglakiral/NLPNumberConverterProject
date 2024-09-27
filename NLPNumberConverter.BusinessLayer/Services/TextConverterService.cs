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

            // Metni küçük harfe çevirme ve işlem yapma metotlarım
            string inputText = userText.InputText.ToLower();
            string outputText = ConvertWordsToNumbers(inputText);

            return new ConvertedText { OutputText = outputText };
        }


        private string ConvertWordsToNumbers(string text)
        {
            // Sayı kelimeleri ile eşleşen sayısal değerleri tanımladım burada
            Dictionary<string, int> numberWords = new Dictionary<string, int>
    {
        { "sıfır", 0 }, { "bir", 1 }, { "iki", 2 }, { "üç", 3 }, { "dört", 4 },
        { "beş", 5 }, { "altı", 6 }, { "yedi", 7 }, { "sekiz", 8 }, { "dokuz", 9 },
        { "on", 10 }, { "yirmi", 20 }, { "otuz", 30 }, { "kırk", 40 },
        { "elli", 50 }, { "altmış", 60 }, { "yetmiş", 70 }, { "seksen", 80 },
        { "doksan", 90 }, { "yüz", 100 }, { "bin", 1000 }, { "milyon", 1000000 }
    };

            //SeparateCompoundNumbers metodu ile birleşik kelimeleri ayırdım 
            text = SeparateCompoundNumbers(text, numberWords.Keys.ToList());

            int total = 0;  // Toplam sayıyı tutacak burası
            int currentNumber = 0;  // o anki sayıyı biriktirmek için burası
            int multiplier = 1;  // Bin veya milyon gibi çarpanları takip etmesi için tanımladım
            bool foundNumber = false;  // Sayı kelimesi bulunduğunda takip etmek içindir
            string processedText = "";

            // Cümleyi kelimelerine ayırıyor
            var words = text.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string cleanWord = word.ToLower();  // Küçük harfe çeviriyorum (büyük küçük harf duyarı için)

                if (numberWords.ContainsKey(cleanWord))
                {
                    int numberValue = numberWords[cleanWord];

                    // "yüz", "bin", "milyon" gibi çarpan kelimeler için işlem yapıyorum
                    if (cleanWord == "yüz")
                    {
                        if (currentNumber == 0) currentNumber = 1;  // "yüz" kelimesi tek başına ise 100 olarak kabul edilmesi için bu koşulu yazdım
                        currentNumber *= numberValue;  // Çarpıyorum
                    }
                    else if (cleanWord == "bin" || cleanWord == "milyon")
                    {
                        if (currentNumber == 0) currentNumber = 1;  // Öncesinde bir sayı yoksa 1 olarak kabul ediyorum
                        total += currentNumber * numberValue;  // Çarpan ekleniyor
                        currentNumber = 0;  // Sayıyı sıfırladı
                    }
                    else
                    {
                        currentNumber += numberValue;  // Diğer durumlarda sayıyı topluyor
                    }

                    foundNumber = true;  // Sayı bulundu
                }
                else
                {
                    // Eğer sayı kelimesi değilse, birikmiş sayıyı ekliyorum
                    if (foundNumber)
                    {
                        total += currentNumber;
                        processedText += total + " ";  // Biriken sayıyı ekliyor
                        total = 0;  // Biriken sayıyı sıfırlıyor
                        currentNumber = 0;
                        foundNumber = false;
                    }

                    // Sayı olmayan kelimeyi olduğu gibi ekliyor
                    processedText += word + " ";
                }
            }

            // Eğer döngü sonunda birikmiş sayılar varsa onlarıda ekliyor
            if (foundNumber)
            {
                total += currentNumber;
                processedText += total;
            }

            return processedText.Trim();
        }



        // Birleşik sayı kelimelerini ayıran metot burası yukarıda da belirttiğim gibi
        private string SeparateCompoundNumbers(string text, List<string> numberWords)
        {
            foreach (var numberWord in numberWords.OrderByDescending(nw => nw.Length)) // Uzun kelimeler önce kontrol ediliyor
            {
                // Eğer metinde birleşik sayı kelimesi varsa, onu ayırıyor
                text = text.Replace(numberWord, " " + numberWord + " ");
            }
            return text;
        }


    }
}