# E-Ticaret Platformu için Microservice Mimarisi

Bu proje, RabbitMQ ve PostgreSQL kullanarak .NET Core ile geliştirilmiş bir e-ticaret platformu için temel bir microservice mimarisini göstermektedir. Her proje, ayrı ayrı geliştirilmiş olup, bağımlılıkların daha iyi yönetilebilmesi için ortak class library projelerinde toplanabilir. Ayrıca, microservislerin izlenebilirliğini ve performans takibini sağlamak için **Jaeger** ile dağıtık izleme (tracing) entegrasyonu yapılabilir.

## İçindekiler
- [Proje Tanımı](#proje-tanımı)
- [Mimari Genel Bakış](#mimari-genel-bakış)
  - [RabbitMQ ile Mesajlaşma](#rabbitmq-ile-mesajlaşma)
  - [PostgreSQL ile Veri Yönetimi](#postgresql-ile-veri-yönetimi)
- [Ekstra Özellikler](#ekstra-özellikler)
- [Kurulum](#kurulum)
  - [Docker](#docker)
  - [Projeyi Çalıştırma](#projeyi-çalıştırma)

## Proje Tanımı

Bu platform aşağıdaki microservislerden oluşmaktadır:

- **Order Servisi**: Gelen siparişleri alır ve işleme koyar.
- **Catalog Servisi**: Ürün stoklarını yönetir.
- **Notification Servisi**: Kullanıcılara e-posta veya SMS ile bildirimler gönderir.

RabbitMQ, servisler arası iletişim için asenkron mesajlaşma sağlarken, PostgreSQL her bir servisin verilerini yönetmek için kullanılır.

## Mimari Genel Bakış

Proje, aşağıdaki ana bileşenlerden oluşmaktadır:

- **RabbitMQ**: Mesaj kuyruklarını yönetir ve servisler arasında asenkron iletişim sağlar.
- **PostgreSQL**: Her microservis, kendi PostgreSQL veritabanını kullanarak veri kalıcılığını sağlar.
- **.NET Core**: Tüm microservisler .NET Core ile geliştirilmiştir.

### RabbitMQ ile Mesajlaşma

- **Catalog Kuyruğu**: Satılan ürünlerin stoklarının düşürülmesi için Catalog servisine HTTP isteği gönderir.
- **Notification Kuyruğu**: Bildirimlerin gönderilmesi için Notification servisine HTTP isteği gönderir.

### PostgreSQL ile Veri Yönetimi

Her microservis, kendi PostgreSQL veritabanına sahiptir:

- **Order Veritabanı**: Sipariş bilgilerini saklar.
- **Catalog Veritabanı**: Ürün stok bilgilerini saklar.
- **Notification Veritabanı**: Gönderilen bildirimleri loglar.

## Ekstra Özellikler

1. **Retry Mekanizmaları**: Hatalara karşı tolerans sağlamak için servislerde tekrar deneme mekanizmaları uygulanabilir.
2. **Loglama**: Servisler önemli aksiyonları ve hataları loglar.
3. **EF Core / Repository Deseni**: Veritabanı işlemleri için Entity Framework Core, repository deseni ile birlikte kullanılır.
4. **Docker/Docker Compose**: Tüm sistem, kolay dağıtım için container'lara alınmıştır.

## Kurulum

### Docker

Proje, tüm servislerin ve bağımlılıkların izole container'larda çalıştırılması için Docker desteği içermektedir.

### Projeyi Çalıştırma

1. Repoyu klonlayın.
2. Bilgisayarınızda Docker kurulu olduğundan emin olun.
3. Aşağıdaki komutu çalıştırarak servisleri başlatın:

   ```bash
   docker-compose up --build
