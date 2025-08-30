using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneCase.Entities.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Data.Concreate.Configs;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    builder.Property(x => x.Description).IsRequired();
    builder.Property(x => x.ImageUrl).IsRequired();
    builder.Property(x => x.Type).IsRequired();
List<Category> categories = [
    new Category("Silikon Kılıflar", "Esnek ve darbe emici silikon telefon kılıfları", "categories/silikon.png") 
        { Id = 1, Type = CategoryType.Material },

    new Category("Deri Kılıflar", "Şık ve profesyonel görünümlü hakiki ve suni deri kılıflar", "categories/deri.png") 
        { Id = 2, Type = CategoryType.Style },

    new Category("Şeffaf Kılıflar", "Telefonun orijinal tasarımını gösteren şeffaf koruma kılıfları", "categories/seffaf.png") 
        { Id = 3, Type = CategoryType.Device,IsDeleted=true },

    new Category("Cüzdanlı Kılıflar", "Kart ve nakit bölmeli, çok fonksiyonlu telefon kılıfları", "categories/cuzdanli.png") 
        { Id = 4, Type = CategoryType.Material },

    new Category("Tam Koruma (360) Kılıflar", "Telefonun hem önünü hem arkasını kaplayan tam koruma sağlayan kılıflar", "categories/tam-koruma.png") 
        { Id = 5, Type = CategoryType.Campaign,IsDeleted=true },

    new Category("Baskılı & Desenli Kılıflar", "Kişiye özel ve çeşitli desenlere sahip kılıflar", "categories/desenli.png") 
        { Id = 6, Type = CategoryType.Style },

    new Category("Sert Kapak (Hard Case) Kılıflar", "Polikarbonat gibi sert malzemelerden yapılmış, çizilmelere karşı dayanıklı kılıflar", "categories/sert-kapak.png") 
        { Id = 7, Type = CategoryType.Device , IsDeleted=true},

    new Category("Bataryalı Kılıflar", "Dahili bataryası ile telefonunuzu şarj edebilen kılıflar", "categories/bataryali.png") 
        { Id = 8, Type = CategoryType.Campaign },

    new Category("Askılı Kılıflar", "Boyuna veya omuza asmak için askı aparatlı telefon kılıfları", "categories/askili.png") 
        { Id = 9, Type = CategoryType.Style, IsDeleted=true},

    new Category("Su Geçirmez Kılıflar", "Su altı kullanımı ve sıvı temaslarına karşı koruma sağlayan özel kılıflar", "categories/su-gecirmez.png") 
        { Id = 10, Type = CategoryType.Material }
];
    builder.HasData(categories);
    builder.HasQueryFilter(x => !x.IsDeleted);
  }
}
