using Microsoft.EntityFrameworkCore;
using MyBook.DAL.Entities;

namespace MyBook.DataAccess.Seed;

public partial class Seeds
{
    public static void CreateAuthors(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new
            {
                //https://mybook.ru/author/antoniya-bajett/
                Id = new Guid("320852a1-b75b-4b89-b286-873c80d11727"),
                FullName = "Антония Сьюзен Байетт",
                Description =
                    " английская писательница. Автор более двух десятков книг, носитель множества почётных учёных степеней различных университетов и лауреат многочисленных литературных наград и премий.",
                Image = "https://i2.mybook.io/c/288x336/author_photos/67/6f/676f2223-7bad-4900-8098-f06d98ec61ad.jpg"
            },
            new
            {
                //https://mybook.ru/author/dzhen-sinsero-2/
                Id = new Guid("51e7d2f1-d989-4e59-86c8-278123f564ea"),
                FullName = "Джен Синсеро",
                Description = "Американская писательница, оратор и тренер по успеху.",
                Image = "https://i3.mybook.io/c/288x336/author_photos/e0/7e/e07e6648-5bbc-4110-9e22-2e3dfd40c110.jpe"
            },
            new
            {
                //https://mybook.ru/author/dzhordzh-oruell/
                Id = new Guid("2ee0cdd2-a3d6-414f-9038-874b12916a86"),
                FullName = "Джордж Оруэлл",
                Description =
                    "Джордж Оруэлл (George Orwell) – творческий псевдоним английского писателя и публициста. Настоящее имя – Эрик Артур Блэр (Eric Arthur Blair). Родился 25 июня 1903 года в Индии в семье британского торгового агента. Оруэлл учился в школе св. Киприана. В 1917 году получил именную стипендию и до 1921 года посещал Итонский Колледж. Жил в Великобритании и других странах Европы, где перебивался случайными заработками и начал писать. Пять лет служил в колониальной полиции в Бирме, про что в 1934 году рассказал в повести «Дни в Бирме».",
                Image = "https://i1.mybook.io/c/288x336/author_photos/0f/be/0fbe593e-84d2-4d9c-9b8d-a746363a8661.jpg"
            },
            new
            {
                //https://mybook.ru/author/mark-menson/
                Id = new Guid("02788b50-5eae-42ce-a375-c0416840d687"),
                FullName = "Марк Мэнсон",
                Description =
                    "Американский автор и консультант по личному развитию, предприниматель и блогер. Ведет блог под своим именем на одноименном сайте. По состоянию на 2019 год написал три книги. Книга «Тонкое искусство пофигизма» заняла шестое место в списке бестселлеров The New York Times.",
                Image = "https://i1.mybook.io/c/288x336/author_photos/b8/0a/b80ac274-40fe-4525-9f0f-59a2fa5159c7.jpg"
            },
            new
            {
                //https://mybook.ru/author/fedor-mihajlovich-dostoevskij/
                Id = new Guid("93348ec2-1d0b-4aff-a83e-aebe01a891d6"),
                FullName = "Федор Достоевский",
                Description =
                    "Федор Достоевский родился в 1821 году в Москве. Отец Достоевского не считал, что писательство — серьезное занятие для молодого человека, а потому отправил его и брата Михаила изучать инженерное дело, что невероятно тяготило молодые умы. Достоевский все свое время уделял самообразованию и был одним из умнейших людей своего века. Первый литературный опыт, еще в студенчестве, оказался успешным, и шаг за шагом Достоевский вошел в круг влиятельных авторов и публицистов. Раннюю славу Достоевскому принес его первый роман «Бедные люди».",
                Image = "https://i2.mybook.io/c/288x336/author_photos/f1/9a/f19a27f4-c7d5-4945-b478-07ef957b9b24.jpg"
            });
    }
}