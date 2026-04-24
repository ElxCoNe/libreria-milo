using LibreriaMilo.Models;
using LibreriaMilo.Response;
using LibreriaMilo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaMilo.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;
    
    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var response = _bookService.GetAllBooks();
        return View(response.Data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        var response = _bookService.SaveBook(book);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Edit(int id)
    {
        var response = _bookService.GetBookById(id);
        if (response.IsSuccess)
        {
            return View(response.Data);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(int id, Book book)
    {
        var response = _bookService.UpdateBook(id, book);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public IActionResult Delete(int id)
    {
        var response = _bookService.DeleteBook(id);
        if (response.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        return View();
    }
}