# InveonBootcamp

# Bitirme Ödevi

# AçıkAkademi: Online Kurs Satış Projesi

## Proje acikakademi.alperturkmen.com adresinde Docker ile canlıya alınmıştır. Docker ve Nginx dosyaları da bu repoda bulunmaktadır.
#### Bilinen Kısıtlamalar
- Hosting servisi kaynaklı olarak büyük dosyaların yüklenmesinde limitasyonlar bulunmaktadır. Lokal geliştirme ortamında bu kısıtlama bulunmamaktadır.
- Not: Dosya boyutu limitasyonu için alternatif çözümler üzerinde çalışılmaktadır.



Bu repo, Inveon Bootcamp kapsamında yapılan bitirme ödevi için oluşturulmuştur.
Bu ödevde, bir kurs satış sitesi yapılmıştır.

Sayfanın en alt kısmında uygulamanın görselleri bulunmaktadır.

# Özellikler

- Öğretmen ve öğrenci rolü bulunmaktadır.
- Öğretmenler kurs oluşturabilir, kurslarının içeriğini düzenleyebilir.
- **Kurslara video ekleyebilir, videoyu düzenleyebilir, silebilir.**
- **Kursların kapak fotoğrafı yüklenebilir**
- Başlık, açıklama, fiyat bilgileri bulunmaktadır.
- **Kurslardaki videoların öğrencide görünecek sıralamasını sürükleyip bırakarak değiştirebilir.**
- Öğretmenler kurslarına gelen siparişleri görebilir.
- Öğrenciler kursları satın alabilir.
- **Sepete ekleme ve çıkarma işlemleri yapılabilir. Sepet localstorage'da korunur**
- Satın alınan kurslar öğrencinin profilinde görünmektedir.
- **Öğrenciler kursları satın aldıktan sonra kurs içeriğine erişebilir.**
- **Öğrenciler kursları izleyebilir.**
- **Satın alınan kursları öğrenci tekrar satın alamaz.**
- **Kursları arama özelliği bulunmaktadır.**
- Kullanıcılar **profil fotoğraflarını güncelleyebilir**
- Kullanıcılar hakkında bilgilerini güncelleyebilir.
- Varsayılan olarak backend'de bazı hazır veriler gelmektedir. Bunlar güncellenebilir.
- Giriş Bilgileri:

  - Öğretmen :
    - mehmetdemir@akademi.com
    - ayseyilmaz@akademi.com
    - Şifreleri: Ogretmen123!
  - Öğrenci:
    - alicelik@akademi.com
    - elifkaya@akademi.com
    - Şifreleri: Ogrenci123!

#### Backend Kurulumu

- API/appsettings.json içinde PostgreSQL veritabanı bağlantı bilgileri gerekmektedir
- dotnet ef database update --project Infrastructure --startup-project API
  komutu ile veritabanı güncellenmelidir

# Görseller

# Öğrenci Anasayfası

![title](EkranGoruntuleri/1.png)

# Kullanıcı Profili

![title](EkranGoruntuleri/2.png)

# Sepet

![title](EkranGoruntuleri/3.png)

# Sepetteki Kursları Satın Alma

![title](EkranGoruntuleri/4.png)

# Satın Alma İşlemi

![title](EkranGoruntuleri/5.png)

# Satın Aldığım Kurslar

![title](EkranGoruntuleri/6.png)

# Kurs Videoları İzleme Ekranı

![title](EkranGoruntuleri/8.png)

# Eğitmen Profil Ekranı

![title](EkranGoruntuleri/9.png)

# Eğitmen Kurs Düzenleme Ekranı

![title](EkranGoruntuleri/10.png)

# Eğitmenin Kursları

![title](EkranGoruntuleri/11.png)

# Sürükle Bırak ile Video Sırası Değiştirme

![title](EkranGoruntuleri/12.png)

# Eğitmen Video Önizleme

![title](EkranGoruntuleri/13.png)

# Video Ekleme

![title](EkranGoruntuleri/14.png)

# Kurs Ekleme

![title](EkranGoruntuleri/15.png)

# Eğitmenin Kurslarına Gelen Siparişler

![title](EkranGoruntuleri/16.png)

# Kurs Arama

![title](EkranGoruntuleri/17.png)

# Kurs Detay

![title](EkranGoruntuleri/18.png)
