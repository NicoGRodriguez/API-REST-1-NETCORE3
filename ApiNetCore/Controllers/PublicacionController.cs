using Api.Core.ConsultaFiltros;
using Api.Core.DTOs;
using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.Core.PersonalizadasEntidades;
using Api.infraestructura.Interfaces;
using Api.Respuestas;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionServicio _publicacionServicio;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PublicacionController(IPublicacionServicio publicacionServicio, IMapper mapper, IUriService uriService)
        {
            _publicacionServicio = publicacionServicio;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet (Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery] PublicacionConsultaFiltro filtros)
        {
            var posts = _publicacionServicio.GetPosts(filtros);
            //mapeo de una entidad base a un entidad origen o destino
            var postsDTO = _mapper.Map<IEnumerable<PublicacionDTO>>(posts);
            var metadata = new Metadata
            {
                TotalItem = posts.TotalItem,
                CantidadItemPagina = posts.ItemsPaginaTrae,
                PaginaActual = posts.PaginaActual,
                TotalPaginas = posts.TotalPaginas,
                IrPaginaProxima = posts.IrPaginaProxima,
                IrPaginaPrevia = posts.IrPaginaPrevia,
                PaginaProximaUrl = _uriService.GetPostPaginationUri(filtros, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PaginaPreviaUrl = _uriService.GetPostPaginationUri(filtros, Url.RouteUrl(nameof(GetPosts))).ToString()
            };
            var respuesta = new ApiRepuesta<IEnumerable<PublicacionDTO>>(postsDTO) { Meta = metadata };
            Response.Headers.Add("x-paginacion", JsonConvert.SerializeObject(metadata));

            return Ok(respuesta);



            //Metodo viejo de mapeo
            //posts.Select(x => new PublicacionDTO
            //{
            //    IdPublicacion = x.IdPublicacion,
            //    Fecha = x.Fecha,
            //    Descripcion = x.Descripcion,
            //    Imagen = x.Imagen,                
            //    IdUsuario = x.IdUsuario
            //});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _publicacionServicio.GetPost(id);
            var postDTO = _mapper.Map<PublicacionDTO>(post);
            var respuesta = new ApiRepuesta<PublicacionDTO>(postDTO);
            return Ok(respuesta);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PublicacionDTO postDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            var post = _mapper.Map<Publicacion>(postDTO);
            await _publicacionServicio.InsertPost(post);
            postDTO = _mapper.Map<PublicacionDTO>(post);
            var respuesta = new ApiRepuesta<PublicacionDTO>(postDTO);
            return Ok(respuesta);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, PublicacionDTO postDTO)
        {
            var post = _mapper.Map<Publicacion>(postDTO);
            post.id = id;
            var result = await _publicacionServicio.UpDatePost(post);
            var respuesta = new ApiRepuesta<bool>(result);
            return Ok(respuesta);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _publicacionServicio.DeletePost(id);
            var respuesta = new ApiRepuesta<bool>(result);
            return Ok(respuesta);
        }
    }
}
