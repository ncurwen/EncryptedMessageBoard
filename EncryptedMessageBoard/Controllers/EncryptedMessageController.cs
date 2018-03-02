using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EncryptedMessageBoard.Models;
using System.Linq;
using System;

namespace EncryptedMessageBoard.Controllers
{
    [Route("api/[controller]")]
    public class EncryptedMessageController : Controller
    {
        private readonly EncryptedMessageContext _context;

        public EncryptedMessageController(EncryptedMessageContext context)
        {
            _context = context;

            // Default Message
            if (_context.EncryptedMessages.Count() == 0)
            {
                _context.EncryptedMessages.Add(new EncryptedMessage("Nick Curwen", "Thank you for reviewing my application."));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<EncryptedMessage> GetAll()
        {
            return _context.EncryptedMessages.ToList();
        }

        [HttpGet("{id}", Name = "GetEncryptedMessage")]
        public IActionResult GetById(long id)
        {
            var message = _context.EncryptedMessages.FirstOrDefault(t => t.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            return new ObjectResult(message);
        }

        [HttpGet("{id}/decrypt", Name = "GetDecryptedMessage")]
        public IActionResult GetByIdAndDecrypt(long id)
        {
            var message = _context.EncryptedMessages.FirstOrDefault(t => t.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            String decryptedMessage = $"[{message.TimeStamp}] {message.Author}: {message.DecryptMessage()}";
            return new ObjectResult(decryptedMessage);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EncryptedMessage message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            _context.EncryptedMessages.Add(message);
            _context.SaveChanges();

            return CreatedAtRoute("GetEncryptedMessage", new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] EncryptedMessage item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var message = _context.EncryptedMessages.FirstOrDefault(t => t.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            message.Author = item.Author;
            message.Message = item.DecryptMessage();
            message.TimeStamp = DateTime.Now.ToString().TrimEnd();

            _context.EncryptedMessages.Update(message);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var message = _context.EncryptedMessages.FirstOrDefault(t => t.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            _context.EncryptedMessages.Remove(message);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}