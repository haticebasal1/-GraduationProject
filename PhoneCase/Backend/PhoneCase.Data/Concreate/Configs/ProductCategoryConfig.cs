using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCase.Entities.Concrete;

namespace PhoneCase.Data.Concreate.Configs;

public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x => new { x.ProductId, x.CategoryId });
        List<ProductCategory> productCategories =
        [
            new (1, 1), // iPhone 15 Mat Siyah Silikon Kılıf -> Sadece Silikon
    new (2, 1), // Samsung S24 Pastel Mavi Silikon Kılıf -> Sadece Silikon
    new (3, 1), // Google Pixel 8 Pro MagSafe Uyumlu Lacivert Silikon Kılıf -> Sadece Silikon
    new (4, 1), // Xiaomi 13T Kamera Korumalı Yeşil Silikon Kılıf -> Sadece Silikon
    new (5, 1), // iPhone 14 Pro Şeffaf Kenarlı Mor Silikon Kılıf
    new (5, 3), // -> Hem "Silikon" hem de "Şeffaf" bir kılıf
    new (6, 2), // iPhone 15 Pro Hakiki Deri Taba Rengi Kılıf -> Sadece Deri
    new (7, 2), // Samsung S24 Ultra Siyah Suni Deri Kılıf -> Sadece Deri
    new (8, 2), // iPhone 15 Mıknatıslı Kahverengi Deri Kılıf -> Sadece Deri
    new (9, 2), // Google Pixel 8 Bordo Deri Kılıf -> Sadece Deri
    new (10, 2), // Xiaomi 14 Pro Mavi Deri Kılıf -> Sadece Deri
    new (11, 3), // iPhone 15 Sararmaz Şeffaf Kılıf -> Sadece Şeffaf
    new (12, 3), // Samsung S23 FE Köşe Korumalı Şeffaf Kılıf
    new (12, 1), // -> Hem "Şeffaf" hem de darbeye karşı "Silikon" özellikli
    new (13, 3), // Xiaomi 13 Lite İnce Şeffaf Kılıf
    new (13, 7), // -> Hem "Şeffaf" hem de "Sert Kapak" (ince olduğu için)
    new (14, 3), // iPhone 13 Standlı Şeffaf Kılıf -> Sadece Şeffaf
    new (15, 3), // Samsung A54 MagSafe Uyumlu Şeffaf Kılıf -> Sadece Şeffaf
    new (16, 4), // iPhone 15 Pro Max Siyah Deri Cüzdanlı Kılıf
    new (16, 2), // -> Hem "Cüzdanlı" hem de malzemesi "Deri"
    new (17, 4), // Samsung S24 Lacivert Cüzdan Kılıf
    new (17, 2), // -> Hem "Cüzdanlı" hem de malzemesi "Deri" görünümlü
    new (18, 4), // iPhone 14 Kırmızı Ayrılabilir Cüzdanlı Kılıf
    new (18, 2), // -> Hem "Cüzdanlı" hem de "Deri"
    new (19, 4), // Xiaomi 13T Pro Yeşil Cüzdan Kılıf -> Sadece Cüzdanlı
    new (20, 4), // Samsung S23 Mor Cüzdanlı Kılıf -> Sadece Cüzdanlı
    new (21, 5), // iPhone 15 360 Tam Koruma Siyah Kılıf
    new (21, 7), // -> Hem "Tam Koruma" hem de "Sert Kapak"
    new (22, 5), // Samsung S24 Ultra Askeri Düzey Koruma Kılıfı
    new (22, 7), // -> Hem "Tam Koruma" hem de "Sert Kapak"
    new (23, 5), // Xiaomi 14 Şeffaf 360 Koruma Kılıf
    new (23, 3), // -> Hem "Tam Koruma" hem de "Şeffaf"
    new (24, 5), // iPhone 14 Pro Mavi Tam Koruma Kılıfı
    new (24, 7), // -> Hem "Tam Koruma" hem de "Sert Kapak"
    new (25, 5), // Google Pixel 7 Pro Zırhlı Kılıf
    new (25, 7), // -> Hem "Tam Koruma" hem de "Sert Kapak"
    new (26, 6), // iPhone 14 Mermer Desenli Kılıf
    new (26, 7), // -> Hem "Desenli" hem de genellikle "Sert Kapak" üzerine baskı
    new (27, 6), // Samsung A54 Çiçek Bahçesi Desenli Kılıf
    new (27, 1), // -> Hem "Desenli" hem de genellikle "Silikon" üzerine baskı
    new (28, 6), // Xiaomi 13 Geometrik Desenli Kılıf
    new (28, 7), // -> Hem "Desenli" hem de "Sert Kapak"
    new (29, 6), // iPhone 15 Pro Max Galaksi Temalı Kılıf
    new (30, 6), // Samsung S23 Ultra Kamuflaj Desenli Kılıf
    new (31, 7), // iPhone 15 Pro İnce Siyah Sert Kapak -> Sadece Sert Kapak
    new (32, 7), // Samsung S24 Gri Kumaş Dokulu Sert Kapak -> Sadece Sert Kapak
    new (33, 7), // Xiaomi 14 Şeffaf Mat Sert Kapak -> Bu da aynı zamanda Şeffaf sayılabilir
    new (33, 3),
    new (34, 7), // Google Pixel 8 Karbon Fiber Desenli Sert Kapak
    new (34, 6), // -> Hem "Sert Kapak" hem de "Desenli" (karbon fiber bir desen)
    new (35, 7), // iPhone 13 Kırmızı Sert Kapak -> Sadece Sert Kapak
    new (36, 8), // iPhone 15 Pro 5000mAh Bataryalı Kılıf
    new (36, 7), // -> Hem "Bataryalı" hem de kasası "Sert Kapak"
    new (37, 8), // Samsung S24 Ultra 6000mAh Bataryalı Kılıf
    new (37, 7), // -> Hem "Bataryalı" hem de "Sert Kapak"
    new (38, 8), // iPhone 14 4800mAh İnce Bataryalı Kılıf
    new (38, 7), // -> Hem "Bataryalı" hem de "Sert Kapak"
    new (39, 8), // Google Pixel 8 Pro 5500mAh Bataryalı Kılıf
    new (39, 7), // -> Hem "Bataryalı" hem de "Sert Kapak"
    new (40, 8), // iPhone 15 Plus MagSafe Uyumlu Bataryalı Kılıf
    new (40, 7), // -> Hem "Bataryalı" hem de "Sert Kapak"
    new (41, 9), // iPhone 15 Şeffaf Örgü Askılı Kılıf
    new (41, 3), // -> Hem "Askılı" hem de "Şeffaf"
    new (42, 9), // Samsung A54 Siyah Deri Askılı Kılıf
    new (42, 2), // -> Hem "Askılı" hem de "Deri"
    new (43, 9), // Xiaomi 13 Lite Pembe İp Askılı Kılıf
    new (43, 1), // -> Hem "Askılı" hem de "Silikon"
    new (44, 9), // iPhone 14 Pro Zincir Askılı Kılıf -> Sadece Askılı
    new (45, 9), // Samsung S23 Renkli Boncuk Askılı Kılıf -> Sadece Askılı
    new (46, 10), // iPhone 15 Pro IP68 Su Geçirmez Kılıf
    new (46, 5),  // -> Hem "Su Geçirmez" hem de "Tam Koruma" sağlar
    new (47, 10), // Samsung S24 Ultra Dalış Kılıfı
    new (47, 5),  // -> Hem "Su Geçirmez" hem de "Tam Koruma"
    new (48, 10), // GoPro Uyumlu Evrensel Su Geçirmez Kılıf -> Sadece Su Geçirmez
    new (49, 10), // iPhone 14 Toz ve Su Geçirmez Kılıf
    new (49, 5),  // -> Hem "Su Geçirmez" hem de "Tam Koruma"
    new (50, 10), // Xiaomi 14 Pro Şamandıralı Su Geçirmez Kılıf
    new (50, 5)   // -> Hem "Su Geçirmez" hem de "Tam Koruma"
        ];
        builder.HasData(productCategories);
        builder.HasQueryFilter(x => !x.Category!.IsDeleted && !x.Product!.IsDeleted);
    }
}
