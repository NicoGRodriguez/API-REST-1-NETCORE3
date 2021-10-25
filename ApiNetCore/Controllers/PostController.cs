using Api.Core.DTOs;
using Api.Core.Entidades;
using Api.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepositorio _postRepositorio;
        private readonly IMapper _mapper;

        public PostController(IPostRepositorio postRepositorio, IMapper mapper)
        {
            _postRepositorio = postRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepositorio.GetPosts();
            //mapeo de una entidad base a un entidad origen o destino
            var postsDTO = _mapper.Map<IEnumerable<PublicacionDTO>>(posts);
            //Metodo viejo de mapeo
            //posts.Select(x => new PublicacionDTO
            //{
            //    IdPublicacion = x.IdPublicacion,
            //    Fecha = x.Fecha,
            //    Descripcion = x.Descripcion,
            //    Imagen = x.Imagen,                
            //    IdUsuario = x.IdUsuario
            //});
            return Ok(postsDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepositorio.GetPost(id);
            var postDTO = _mapper.Map<IEnumerable<PublicacionDTO>>(post);
            return Ok(postDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PublicacionDTO postDTO)
        {
            var post = _mapper.Map<Publicacion>(postDTO);
            await _postRepositorio.InsertPost(post);
            return Ok(post);
        }
    }
}
