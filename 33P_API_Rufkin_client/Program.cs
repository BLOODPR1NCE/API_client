using _33P_API_Rufkin_client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _33P_API_Rufkin_client
{
    internal static class Program
    {

        static async Task Main()
        {
            while (true)
            {
                Console.WriteLine("1. Получить список авторов");
                Console.WriteLine("2. Получить список жанров");
                Console.WriteLine("3. Получить список книг");
                Console.WriteLine("4. Получить список книг по жанрам");
                Console.WriteLine("5. Удалить кингу");
                Console.WriteLine("6. Добавить книгу");
                Console.WriteLine("7. Добавить автора");
                Console.WriteLine("8. Обновить информацию о авторе");
                Console.WriteLine("9. Обновить информацию о книге");
                Console.WriteLine("10. Выход");

                Console.WriteLine("Выберите пункт меню: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        await GetAuthors();
                        break;
                    case "2":
                        await GetGenres();
                        break;
                    case "3":
                        await GetBooks();
                        break;
                    case "4":
                        await GetBooksGenres();
                        break;
                    case "5":
                        await DeleteBook();
                        break;
                    case "6":
                        await AddBook();
                        break;
                    case "7":
                        await AddAuthor();
                        break;
                    case "8":
                        await UpdateAuthor();
                        break;
                    case "9":
                        await UpdateBook();
                        break;
                    case "10":
                        break;
                }
            }
        }

        static async Task GetAuthors()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            HttpResponseMessage message = await client.GetAsync("authorlist");
            string str = await message.Content.ReadAsStringAsync();
            List<Author> authors = JsonConvert.DeserializeObject<List<Author>>(str);

            Console.WriteLine("");
            foreach (var author in authors)
            {
                Console.WriteLine($"ID: {author.Id}, Имя: {author.Fullname}, Дата рождения: {author.Birthdate:yyyy-MM-dd}, Страна: {author.Country}");
            }
            Console.WriteLine("");
        }

        static async Task GetGenres()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            HttpResponseMessage message = await client.GetAsync("genrelist");
            string str = await message.Content.ReadAsStringAsync();
            List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(str);

            Console.WriteLine("");
            foreach (var genre in genres)
            {
                Console.WriteLine($"ID: {genre.Id}, Наименование: {genre.Name}");
            }
            Console.WriteLine("");
        }

        static async Task GetBooks()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            HttpResponseMessage message = await client.GetAsync("booklist");
            string str = await message.Content.ReadAsStringAsync();
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(str);

            Console.WriteLine("");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Наименование: {book.Title}, Год публикации: {book.YearPublication}, Автор: {book.Author}");
            }
            Console.WriteLine("");
        }

        static async Task GetBooksGenres()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            HttpResponseMessage message = await client.GetAsync("bookgenrelist");
            string str = await message.Content.ReadAsStringAsync();
            List<BooksGenre> bookgenres = JsonConvert.DeserializeObject<List<BooksGenre>>(str);

            Console.WriteLine("");
            foreach (var bookgenre in bookgenres)
            {
                Console.WriteLine($"ID: {bookgenre.Id}, Книга: {bookgenre.Book}, Жанр: {bookgenre.Genre}");
            }
            Console.WriteLine("");
        }

        static async Task DeleteBook()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            Console.Write("Введите ID книги для удаления: ");
            var id = Convert.ToInt32(Console.ReadLine());

            HttpResponseMessage deleteBook = await client.DeleteAsync($"/bookdelete/{id}");
            Console.WriteLine("");
            Console.WriteLine("Книга успешно удалена");
            Console.WriteLine("");
        }

        static async Task AddBook()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            Console.Write("Введите название новой книги: ");
            var title = Console.ReadLine();
            Console.Write("Введите ID автора: ");
            var author = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите год публикации: ");
            var yearPublication = Convert.ToInt32(Console.ReadLine());


            Book newBook = new Book()
            {
                Title = $"{title}",
                Author = int.Parse($"{author}"),
                YearPublication = int.Parse($"{yearPublication}"),
            };
            JsonContent contentCreate2 = JsonContent.Create(newBook);
            HttpResponseMessage createBook = await client.PostAsync("/bookcreate", contentCreate2);

            Console.WriteLine("");
            Console.WriteLine($"Книга успешно добавлена");
            Console.WriteLine("");
        }

        static async Task AddAuthor()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            Console.Write("Введите ФИО автора: ");
            var fullname = Console.ReadLine();
            Console.Write("Введите год рождения: ");
            var dateY = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите месяц рождения: ");
            var dateM = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите день рождения: ");
            var dateD = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите страниу: ");
            var country = Console.ReadLine();


            Author newAuthor = new Author()
            {
                Fullname = $"{fullname}",
                Birthdate = new DateOnly(dateY, dateM, dateD),
                Country = $"{country}",
            };

            JsonContent contentCreate = JsonContent.Create(newAuthor);
            HttpResponseMessage createAuthor = await client.PostAsync("/authorcreate", contentCreate);

            Console.WriteLine("");
            Console.WriteLine($"Автор успешно добавлен");
            Console.WriteLine("");
        }

        static async Task UpdateAuthor()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            Console.Write("Введите ID автора для обновления: ");
            int id = Convert.ToInt32(Console.ReadLine());

            HttpResponseMessage messageAuthor = await client.GetAsync($"authorlist/{id}");
            string str = await messageAuthor.Content.ReadAsStringAsync();
            Author author = JsonConvert.DeserializeObject<Author>(str);

            Console.Write("Введите новое имя автора: ");
            string fullname = Console.ReadLine();
            author.Fullname = $"{fullname}";

            JsonContent contentUpdate = JsonContent.Create(author);
            HttpResponseMessage updateAuthor = await client.PutAsync("/authorupdate", contentUpdate);

            Console.WriteLine("");
            Console.WriteLine("Автор успешно обновлен");
            Console.WriteLine("");
        }

        static async Task UpdateBook()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7106/");

            Console.Write("Введите ID книги для обновления: ");
            int id = Convert.ToInt32(Console.ReadLine());

            //обновление книги
            HttpResponseMessage messageBook = await client.GetAsync($"booklist/{id}");
            string str = await messageBook.Content.ReadAsStringAsync();
            Book book = JsonConvert.DeserializeObject<Book>(str);

            Console.Write("Введите новое название книги: ");
            var title = Console.ReadLine();
            book.Title = title;

            JsonContent contentUpdate2 = JsonContent.Create(book);
            HttpResponseMessage updateBook = await client.PutAsync("/bookupdate", contentUpdate2);

            Console.WriteLine("");
            Console.WriteLine("Книга успешно обновлена");
            Console.WriteLine("");
        }
    }
}
