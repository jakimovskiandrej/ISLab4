using BookApplication.Domain.DomainModels;
using BookApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Implementation
{
    public class ApiDataFetchService : IApiDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;

        public ApiDataFetchService(IBookService bookService, IChapterService chapterService)
        {
            _httpClient = new HttpClient();
            _bookService = bookService;
            _chapterService = chapterService;
        }

        public async Task<List<Book>> FetchBooksFromApi()
        {
            string URL = "http://is-lab4.ddns.net:8080/books";
            HttpResponseMessage response = await _httpClient.GetAsync(URL);
            var data = await response.Content.ReadFromJsonAsync<List<BookDTO>>();
            var books = data.Select(dto => new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Name,
                ISBN = dto.IsbnCode,
                PublishedYear = dto.PublishedYear,
                Description = dto.ShortDescription,
                Author = dto.AuthorFirstName + " " + dto.AuthorLastName,
                BookId = dto.Id
            }).ToList();

            foreach (var book in books)
            {
                _bookService.Insert(book);
            }
            return books;
        }

        public async Task<List<Chapter>> FetchChaptersFromApi(Guid bookId, string index)
        {
            var book = _bookService.GetById(bookId);

            string URL = $"http://is-lab4.ddns.net:8080/chapters?bookId={book.BookId}&studentIndex=226086";
            HttpResponseMessage message = await _httpClient.PostAsync(URL, null);

            var data = await message.Content.ReadFromJsonAsync<List<ChapterDTO>>();

            var chapters = data.Select(dto => new Chapter
            {
                BookId = bookId,
                Book = _bookService.GetById(bookId),
                Title = dto.Title,
                PageCount = dto.TotalPages,
                Summary = dto.Overview,
                ChapterNumber = dto.ChapterId,
                HasExercises = dto.IncludesExercises,
                KeyConcept = dto.KeyConcept,
                DifficultyLevel = dto.Level,
                LastUpdated = dto.LastUpdated
            }).ToList();

            foreach (var item in chapters)
            {
                _chapterService.Insert(item);
            }

            return chapters;
        }
    }
}
