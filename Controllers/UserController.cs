using Microsoft.AspNetCore.Mvc;
using CRUDOperation.Model;
using CRUDOperation.Service;

namespace CRUDOperation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound();

            return user;
        }

        // POST: api/user
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, User user)
        {
            var existingUser = _userService.Get(id);
            if (existingUser == null)
                return NotFound();

            user.Id = existingUser.Id;
            _userService.Update(id, user);
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound();

            _userService.Delete(id);
            return NoContent();
        }
    }
}
