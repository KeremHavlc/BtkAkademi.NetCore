using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //IoC Container uygulandı!
        private readonly RepositoryContext _context;
        //buradaki _context readonly olduğu için ya constructor içerisinde ya da tanımlandığı yerde set edilebilir.
        public BooksController(RepositoryContext context)
        {
            //Burada RepositoryContext sınıfı IoC Container'dan alınıyor. context=>RepositoryContext nesnesidir.
            _context = context;
            //context nesnesi _context nesnesine set ediliyor.
            //ve bütün değerlere erişilebiliyor.
        }

        [HttpGet]
        public IActionResult GetMyBook()
        {
            try
            {
                var result = _context.Books.ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var result = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if (result is null)
                    return NotFound();
                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                _context.Books.Add(book);
                _context.SaveChanges();
                return Ok("Ekleme İşlemi Başarılı!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                    return NotFound();

                if (id != book.Id)
                    return BadRequest();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _context.SaveChanges();
                return Ok("Güncelleme Başarılı!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
