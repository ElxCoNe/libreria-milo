using LibreriaMilo.Models;
using LibreriaMilo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaMilo.Controllers;

public class LoanController : Controller
{
    private readonly LoanService _loanService;
    private readonly BookService _bookService;
    private readonly UserService _userService;

    public LoanController(
        LoanService loanService,
        BookService bookService,
        UserService userService)
    {
        _loanService = loanService;
        _bookService = bookService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        var response = _loanService.GetAllLoans();

        return View(response.Data);
    }

    public IActionResult Create()
    {
        LoadDropdownData();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Loan loan)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdownData();
            return View(loan);
        }

        var response = _loanService.CreateLoan(loan);

        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            LoadDropdownData();
            return View(loan);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var response = _loanService.GetLoanById(id);

        if (!response.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        LoadDropdownData();
        return View(response.Data);
    }

    [HttpPost]
    public IActionResult Edit(int id, Loan loan)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdownData();
            return View(loan);
        }

        var response = _loanService.UpdateLoan(id, loan);

        if (!response.IsSuccess)
        {
            ViewBag.ErrorMessage = response.Message;
            LoadDropdownData();
            return View(loan);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var response = _loanService.GetLoanById(id);

        if (!response.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        return View(response.Data);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var response = _loanService.DeleteLoan(id);

        if (!response.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    private void LoadDropdownData()
    {
        ViewBag.Users = _userService.GetAllUsers().Data;
        ViewBag.Books = _bookService.GetAllBooks().Data;
    }
}