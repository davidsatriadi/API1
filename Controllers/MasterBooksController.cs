using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API1.Models;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterBooksController : ControllerBase
    {
        private readonly Buat_TesContext _context;

        public MasterBooksController(Buat_TesContext context)
        {
            _context = context;
        }

        // GET: api/MasterBooks
        [HttpGet]
        public IEnumerable<MasterBook> GetMasterBook()
        {
            return _context.MasterBook;
        }

        // GET: api/MasterBooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMasterBook([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var masterBook = await _context.MasterBook.FindAsync(id);

            if (masterBook == null)
            {
                return NotFound();
            }

            return Ok(masterBook);
        }

        // PUT: api/MasterBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMasterBook([FromRoute] string id, [FromBody] MasterBook masterBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != masterBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(masterBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MasterBooks
        [HttpPost]
        public async Task<IActionResult> PostMasterBook([FromBody] MasterBook masterBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MasterBook.Add(masterBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MasterBookExists(masterBook.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMasterBook", new { id = masterBook.Id }, masterBook);
        }

        // DELETE: api/MasterBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterBook([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var masterBook = await _context.MasterBook.FindAsync(id);
            if (masterBook == null)
            {
                return NotFound();
            }

            _context.MasterBook.Remove(masterBook);
            await _context.SaveChangesAsync();

            return Ok(masterBook);
        }

        private bool MasterBookExists(string id)
        {
            return _context.MasterBook.Any(e => e.Id == id);
        }
    }
}