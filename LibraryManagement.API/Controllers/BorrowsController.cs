using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.BLL.DTOs.Borrows;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly BorrowService _borrowService;

        public BorrowsController(BorrowService borrowService) => _borrowService = borrowService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetBorrows()
        {
            var borrows = await _borrowService.GetAllBorrowsAsync();
            return Ok(borrows);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecordDto>> GetBorrow(int id)
        {
            var borrow = await _borrowService.GetBorrowByIdAsync(id);
            return borrow == null ? NotFound() : Ok(borrow);
        }

        [HttpPost]
        public async Task<ActionResult<int>> BorrowBook(BorrowCreateDto borrowCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var borrowId = await _borrowService.BorrowBookAsync(borrowCreateDto);
            return CreatedAtAction(nameof(GetBorrow), new { id = borrowId }, borrowId);
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            await _borrowService.ReturnBookAsync(id);
            return NoContent();
        }
    }
}
