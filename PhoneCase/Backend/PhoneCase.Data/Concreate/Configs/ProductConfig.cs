using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCase.Entities.Concrete;

namespace PhoneCase.Data.Concreate.Configs;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Properties).IsRequired();
        builder.Property(x => x.ImageUrl).IsRequired();
        builder.Property(x => x.Price).HasColumnType("decimal(10,2)");
        List<Product> products = [
    new Product("iPhone 15 Mat Siyah Silikon Kılıf", "Kaymaz yüzeyli, parmak izi bırakmayan mat silikon koruma.", 250, "products/iphone15-silikon-siyah.png", false){Id=1},
    new Product("Samsung S24 Pastel Mavi Silikon Kılıf", "Canlı renkte, darbelere karşı esnek ve tam koruma sağlayan kılıf.", 220, "products/s24-silikon-mavi.png", false){Id=2},
    new Product("Google Pixel 8 Pro MagSafe Uyumlu Lacivert Silikon Kılıf", "Magsafe aksesuarlarıyla uyumlu, mıknatıslı ve şık tasarım.", 350, "products/pixel8pro-silikon-lacivert.png", false){Id=3, IsDeleted=true},
    new Product("Xiaomi 13T Kamera Korumalı Yeşil Silikon Kılıf", "Sürgülü kamera koruyucusu ile lenslerinizi güvende tutar.", 280, "products/xiaomi13t-silikon-yesil.png", false){Id=4},
    new Product("iPhone 14 Pro Şeffaf Kenarlı Mor Silikon Kılıf", "Telefonun rengini gösteren şeffaf sırt ve renkli kenarlar.", 260, "products/iphone14pro-silikon-mor.png", false){Id=5},
    new Product("iPhone 15 Pro Hakiki Deri Taba Rengi Kılıf", "Zamanla güzelleşen, premium hakiki deri malzemeden üretilmiştir.", 750, "products/iphone15pro-deri-taba.png", false){Id=6, IsDeleted=true},
    new Product("Samsung S24 Ultra Siyah Suni Deri Kılıf", "Profesyonel görünüm sunan, kaliteli ve dayanıklı suni deri.", 550, "products/s24ultra-deri-siyah.png", false){Id=7},
    new Product("iPhone 15 Mıknatıslı Kahverengi Deri Kılıf", "Güçlü mıknatıslı kapak ve şık dikiş detayları.", 600, "products/iphone15-deri-kahve.png", false){Id=8},
    new Product("Google Pixel 8 Bordo Deri Kılıf", "Telefona tam oturan, zarif ve ince yapılı deri kılıf.", 620, "products/pixel8-deri-bordo.png", false){Id=9,IsDeleted=false},
    new Product("Xiaomi 14 Pro Mavi Deri Kılıf", "Modern ve şık bir görünüm için mavi renkli deri kaplama.", 580, "products/xiaomi14pro-deri-mavi.png", false){Id=10},
    new Product("iPhone 15 Sararmaz Şeffaf Kılıf", "Uzun süreli kullanımlarda sararmaya karşı dayanıklı özel malzeme.", 300, "products/iphone15-seffaf.png", false){Id=11},
    new Product("Samsung S23 FE Köşe Korumalı Şeffaf Kılıf", "Düşmelere karşı ekstra koruma için güçlendirilmiş köşeler.", 240, "products/s23fe-seffaf.png", false){Id=12},
    new Product("Xiaomi 13 Lite İnce Şeffaf Kılıf", "Telefonun orijinal inceliğini koruyan ultra ince tasarım.", 190, "products/xiaomi13lite-seffaf.png", false){Id=13},
    new Product("iPhone 13 Standlı Şeffaf Kılıf", "Video izlemek için entegre stand aparatlı şeffaf kılıf.", 320, "products/iphone13-seffaf-standli.png", false){Id=14},
    new Product("Samsung A54 MagSafe Uyumlu Şeffaf Kılıf", "Şeffaf tasarım ve MagSafe uyumluluğu bir arada.", 350, "products/samsung-a54-seffaf-magsafe.png", false){Id=15},
    new Product("iPhone 15 Pro Max Siyah Deri Cüzdanlı Kılıf", "3 kart bölmesi ve bir nakit cebi ile pratik kullanım.", 650, "products/iphone15promax-cuzdanli.png", false){Id=16,IsDeleted=true},
    new Product("Samsung S24 Lacivert Cüzdan Kılıf", "Telefonunuzu ve kartlarınızı bir arada tutan şık çözüm.", 580, "products/s24-cuzdanli.png", false){Id=17},
    new Product("iPhone 14 Kırmızı Ayrılabilir Cüzdanlı Kılıf", "İsteğe bağlı olarak cüzdan kısmından ayrılabilen mıknatıslı yapı.", 720, "products/iphone14-cuzdanli-ayrilabilir.png", false){Id=18},
    new Product("Xiaomi 13T Pro Yeşil Cüzdan Kılıf", "Stand özelliği ve kartlık cepleri ile fonksiyonel tasarım.", 550, "products/xiaomi13tpro-cuzdanli.png", false){Id=19},
    new Product("Samsung S23 Mor Cüzdanlı Kılıf", "Göz alıcı mor renkte, çok amaçlı cüzdan kılıf.", 590, "products/s23-cuzdanli-mor.png", false){Id=20},
    new Product("iPhone 15 360 Tam Koruma Siyah Kılıf", "Ekran koruyuculu ön kapak ve arka kapaktan oluşan tam gövde koruması.", 500, "products/iphone15-360.png", false){Id=21,IsDeleted=true},
    new Product("Samsung S24 Ultra Askeri Düzey Koruma Kılıfı", "Zorlu şartlara ve düşmelere karşı askeri standartlarda koruma.", 650, "products/s24ultra-360-askeri.png", false){Id=22},
    new Product("Xiaomi 14 Şeffaf 360 Koruma Kılıf", "Telefonun tasarımını gizlemeden 360 derece koruma sağlar.", 480, "products/xiaomi14-360-seffaf.png", false){Id=23},
    new Product("iPhone 14 Pro Mavi Tam Koruma Kılıfı", "Dahili ekran koruyuculu, toza ve çizilmelere dayanıklı kılıf.", 520, "products/iphone14pro-360-mavi.png", false){Id=24},
    new Product("Google Pixel 7 Pro Zırhlı Kılıf", "Katmanlı yapı ve güçlendirilmiş tamponlarla maksimum koruma.", 600, "products/pixel7pro-360-zirhli.png", false){Id=25},
    new Product("iPhone 14 Mermer Desenli Kılıf", "Altın varaklı beyaz mermer deseni ile lüks bir görünüm.", 350, "products/iphone14-desenli-mermer.png", false){Id=26},
    new Product("Samsung A54 Çiçek Bahçesi Desenli Kılıf", "Bahar renklerinde canlı çiçek desenleri.", 320, "products/samsung-a54-desenli-cicek.png", false){Id=27},
    new Product("Xiaomi 13 Geometrik Desenli Kılıf", "Modern ve minimalist geometrik şekillerle tasarlanmış kılıf.", 300, "products/xiaomi13-desenli-geometrik.png", false){Id=28},
    new Product("iPhone 15 Pro Max Galaksi Temalı Kılıf", "Derin uzay ve galaksi temalı parlak baskılı kılıf.", 380, "products/iphone15promax-desenli-galaksi.png", false){Id=29},
    new Product("Samsung S23 Ultra Kamuflaj Desenli Kılıf", "Askeri kamuflaj desenli, maceracı ruhlar için.", 340, "products/s23ultra-desenli-kamuflaj.png", false){Id=30},
    new Product("iPhone 15 Pro İnce Siyah Sert Kapak", "Polikarbonat malzemeden ultra ince ve hafif koruma.", 280, "products/iphone15pro-sertkapak-siyah.png", false){Id=31},
    new Product("Samsung S24 Gri Kumaş Dokulu Sert Kapak", "Farklı bir dokunuş sunan, kumaş kaplamalı sert kılıf.", 350, "products/s24-sertkapak-kumas.png", false){Id=32},
    new Product("Xiaomi 14 Şeffaf Mat Sert Kapak", "Buzlu cam efekti veren yarı şeffaf mat sert kılıf.", 300, "products/xiaomi14-sertkapak-mat.png", false){Id=33},
    new Product("Google Pixel 8 Karbon Fiber Desenli Sert Kapak", "Sportif ve teknolojik görünümlü karbon fiber desenli kılıf.", 380, "products/pixel8-sertkapak-karbon.png", false){Id=34,IsDeleted=true},
    new Product("iPhone 13 Kırmızı Sert Kapak", "Canlı kırmızı renkte, pürüzsüz yüzeyli sert koruma kapağı.", 290, "products/iphone13-sertkapak-kirmizi.png", false){Id=35},
    new Product("iPhone 15 Pro 5000mAh Bataryalı Kılıf", "Günün sonunu getiremeyenler için ekstra 5000mAh güç.", 1200, "products/iphone15pro-bataryali.png", false){Id=36},
    new Product("Samsung S24 Ultra 6000mAh Bataryalı Kılıf", "Yoğun kullanımda bile şarj desteği sunan yüksek kapasiteli kılıf.", 1350, "products/s24ultra-bataryali.png", false){Id=37},
    new Product("iPhone 14 4800mAh İnce Bataryalı Kılıf", "Telefonu çok kabalaştırmayan ince tasarımlı şarjlı kılıf.", 1100, "products/iphone14-bataryali-ince.png", false){Id=38},
    new Product("Google Pixel 8 Pro 5500mAh Bataryalı Kılıf", "Pixel telefonunuz için tam gün ekstra kullanım imkanı.", 1250, "products/pixel8pro-bataryali.png", false){Id=39},
    new Product("iPhone 15 Plus 5000mAh MagSafe Uyumlu Bataryalı Kılıf", "Hem şarj eden hem de MagSafe aksesuarlarını destekleyen kılıf.", 1500, "products/iphone15plus-bataryali-magsafe.png", false){Id=40},
    new Product("iPhone 15 Şeffaf Örgü Askılı Kılıf", "Ayarlanabilir boyun askısı ile telefonunuzu güvenle taşıyın.", 400, "products/iphone15-askili-orgu.png", false){Id=41},
    new Product("Samsung A54 Siyah Deri Askılı Kılıf", "Şık deri askısı ve kart cebi bulunan fonksiyonel kılıf.", 450, "products/samsung-a54-askili-deri.png", false){Id=42},
    new Product("Xiaomi 13 Lite Pembe İp Askılı Kılıf", "Spor ve şık görünüm için renkli ip askılı silikon kılıf.", 380, "products/xiaomi13lite-askili-pembe.png", false){Id=43},
    new Product("iPhone 14 Pro Zincir Askılı Kılıf", "Akşam kullanımı için şık metal zincir askılı kılıf.", 480, "products/iphone14pro-askili-zincir.png", false){Id=44},
    new Product("Samsung S23 Renkli Boncuk Askılı Kılıf", "Yaz ayları için ideal, el yapımı boncuk askılı şeffaf kılıf.", 420, "products/s23-askili-boncuk.png", false){Id=45},
    new Product("iPhone 15 Pro IP68 Su Geçirmez Kılıf", "2 metreye kadar su altında koruma sağlayan, IP68 sertifikalı kılıf.", 800, "products/iphone15pro-sugecirmez.png", false){Id=46,IsDeleted=true},
    new Product("Samsung S24 Ultra Dalış Kılıfı", "Plaj ve havuz kullanımı için tam koruma sağlayan su geçirmez kılıf.", 850, "products/s24ultra-sugecirmez.png", false){Id=47},
    new Product("GoPro Uyumlu Evrensel Su Geçirmez Kılıf", "Farklı telefon modelleriyle uyumlu, su altı çekimleri için ideal.", 750, "products/evrensel-sugecirmez.png", false){Id=48},
    new Product("iPhone 14 Toz ve Su Geçirmez Kılıf", "Sadece suya değil, toza ve kara karşı da tam yalıtım.", 780, "products/iphone14-sugecirmez.png", false){Id=49},
    new Product("Xiaomi 14 Pro Şamandıralı Su Geçirmez Kılıf", "Suda batmayan, şamandıralı yapısı ile ekstra güvenlik sağlar.", 820, "products/xiaomi14pro-sugecirmez-samandira.png", false){Id=50}
        ];
        builder.HasData(products);
        builder.HasQueryFilter(x => !x.IsDeleted);

    }
}
