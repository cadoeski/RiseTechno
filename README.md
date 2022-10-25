# Microservices Rehber Uygulaması

Rehber kişi oluşturma, kaldırma,güncelleme, rehbere eklenen kişinin iletişim ve konum bilgilerinin eklenip raporlanabileceği microservice uygulaması.

 
## Senaryo

• Rehberde kişi oluşturma

• Rehberde kişi kaldırma

• Rehberdeki kişiye iletişim bilgisi ekleme

• Rehberdeki kişiden iletişim bilgisi kaldırma

• Rehberdeki kişilerin listelenmesi

• Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin 
getirilmesi

• Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor 
talebi

• Sistemin oluşturduğu raporların listelenmesi

• Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi

 ## Teknik Tasarım
 
 Teknik Tasarım
Kişiler: Sistemde teorik anlamda sınırsız sayıda kişi kaydı yapılabilecektir. Her kişiye 
bağlı iletişim bilgileri de yine sınırsız bir biçimde eklenebilmelidir.
Karşılanması beklenen veri yapısındaki gerekli alanlar aşağıdaki gibidir:

• UUID

• Ad

• Soyad

• Firma

• İletişim Bilgisi

o Bilgi Tipi: Telefon Numarası, E-mail Adresi, Konum

o Bilgi İçeriği

Rapor: Rapor talepleri asenkron çalışacaktır. Kullanıcı bir rapor talep ettiğinde, sistem 
arkaplanda bu çalışmayı darboğaz yaratmadan sıralı bir biçimde ele alacak; rapor 
tamamlandığında ise kullanıcının "raporların listelendiği" endpoint üzerinden raporun 
durumunu "tamamlandı" olarak gözlemleyebilmesi gerekmektedir.
Rapor basitçe aşağıdaki bilgileri içerecektir:

• Konum Bilgisi
• O konumda yer alan rehbere kayıtlı kişi sayısı

• O konumda yer alan rehbere kayıtlı telefon numarası sayısı

Veri yapısı olarak da:

• UUID

• Raporun Talep Edildiği Tarih

• Rapor Durumu (Hazırlanıyor, Tamamlandı)

## Gereksinimler


+ .Net Core
+  C#
+  Docker
+  RabbitMQ

## Git


```

git clone https://github.com/cadoeski/RiseTechno

```
 
## Kurulum

```
docker-compose -f docker/docker-compose.yml up 
```
