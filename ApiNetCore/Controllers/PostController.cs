using Api.Core.Interfaces;
using Api.infraestructura.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepositorio _postRepositorio;

        public PostController(IPostRepositorio postRepositorio)
        {
            _postRepositorio = postRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepositorio.GetPosts();

            return Ok(posts);
        }
    }
}
