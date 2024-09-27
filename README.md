# NLP Number Converter - Çok Katmanlı Mimari ile Numara Dönüşümü Projesi
### Bu proje, doğal dilde yazılmış Türkçe sayıları metin içinde algılayarak sayısal forma dönüştüren bir NLP (Doğal Dil İşleme) algoritması geliştirmeyi amaçlar. Çok katmanlı mimari kullanılarak tasarlanan bu proje, kullanıcıdan gelen metin verisini analiz eder ve sayıları numerik değerlere çevirir. Bu proje kapsamında ASP.NET Core 7.0 ve MVC Web API teknolojileri kullanılmıştır.

### ✅ Proje Katmanları

- NLPNumberConverter.BusinessLayer

- NLPNumberConverter.CoreLayer

- NLPNumberConverter.WebApi

- NLPNumberConverter.WebUI

### 🎈 Proje Özeti

Bu proje, verilen bir metin içerisinde yer alan sayıları metinsel ifadelerden (örneğin, "yüz", "bin") sayısal ifadelere (100, 1000) dönüştüren bir API servisidir. Projede, isteğe bağlı olarak girilen metnin rakamlar ve yazıyla ifade edilen sayılarının karışık kullanıldığı senaryolar da desteklenmektedir.

Bu projede, herhangi bir NLP kütüphanesi kullanılmamıştır. Proje C# ve .NET Core 7.0 kullanılarak geliştirilen bir algoritma üzerinde çalışmaktadır.


### ✨ Kullanılan Teknolojiler

- ASP.NET Core 7.0: Projenin backend kısmını oluşturan framework.
  
- C#: Geliştirme için kullanılan dil.
  
- Entity Framework Core: Veri yönetimi ve modelleme.
  
- Newtonsoft.Json: JSON işlemleri için kullanılan kütüphane.
  
- Visual Studio 2022: Geliştirme ortamı.
  
- Postman: API test işlemleri için kullanılan araç.
  
- HttpClient: WebUI ile API arasındaki iletişimi sağlayan kütüphane.


### ➡️ Katmanlar

Bu proje, çok katmanlı mimari prensiplerine uygun olarak geliştirilmiştir. Katmanlar birbirinden bağımsız olarak çalışır ve işlevleri ayrılmıştır:

#### 💡CoreLayer

Entities: Bu katman, temel veri modellerini içerir. UserText (kullanıcıdan gelen metni temsil eden model) ve ConvertedText (çıktı olarak dönen dönüştürülmüş metin) gibi sınıflar burada tanımlıdır.

#### 💡BusinessLayer

Services: Bu katmanda, asıl iş mantığı ve algoritmalar yer alır. TextConverterService sınıfı, metni analiz eder ve sayıları numerik değerlere çevirir.

- ConvertWordsToNumbers: Bu metod, metindeki sayı kelimelerini sayısal değerlere dönüştürmek için kullanılır.
  
- SeparateCompoundNumbers: Birleşik sayı kelimelerini ayırır ve işlemeye hazır hale getirir.


#### 💡WebApi


Controllers: API katmanıdır. TextConverterController sınıfı, kullanıcıdan gelen POST isteklerini alır ve işleyerek sonuçları geri döner.

- /api/TextConverter/convert: POST metodunu kullanarak, JSON formatındaki metni alır ve sayısal veriye dönüştürerek yanıt verir.


#### 💡WebUI

UI Kontrolcüsü: Bu katman, basit bir kullanıcı arayüzü sağlar. Kullanıcılar metni arayüz üzerinden girip sonuçları görüntüleyebilir. Web arayüzü ile API arasındaki iletişim HttpClient kullanılarak sağlanmıştır.


### 🎈 Proje Yapısı

![image](https://github.com/user-attachments/assets/0b4b552a-ed77-4315-b782-b01357974bae)


### 🎈 Proje Görselleri

![image](https://github.com/user-attachments/assets/e6c39387-1680-442e-97a6-b03e04c6e5d6)

![image](https://github.com/user-attachments/assets/e9486ff1-f70e-4808-9027-fd7aa87ce277)

![image](https://github.com/user-attachments/assets/0f678673-6da9-4826-b047-5a5d5c19f88d)

![image](https://github.com/user-attachments/assets/48ed5eee-f706-45ba-9f64-0f0af867f253)

![image](https://github.com/user-attachments/assets/4a2b04fb-6d79-4366-a31d-4536b4a92efe)

![image](https://github.com/user-attachments/assets/c3a98aca-ba8c-4f6a-a771-404a262fa0b4)






