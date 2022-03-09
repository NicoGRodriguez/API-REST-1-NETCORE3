using Api.Core.ConsultaFiltros;
using Api.Core.Entidades;
using Api.Core.Excepciones;
using Api.Core.Interfaces;
using Api.Core.PersonalizadasEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Servicios
{
    public class PublicacionServicio : IPublicacionServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublicacionServicio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Publicacion> GetPost(int id)
        {
            return await _unitOfWork.PostRepositorio.GetById(id);
        }

        public ListaPagina<Publicacion> GetPosts(PublicacionConsultaFiltro filtros)
        {
            var posts = _unitOfWork.PostRepositorio.GetAll();
            if (filtros.idUsuario != null)
            {
                posts = posts.Where(x => x.IdUsuario == filtros.idUsuario);
            }
            if (filtros.fecha != null)
            {
                posts = posts.Where(x => x.Fecha.ToShortDateString() == filtros.fecha?.ToShortDateString());
            }
            if (filtros.descripcion != null)
            {
                posts = posts.Where(x => x.Descripcion.ToLower().Contains(filtros.descripcion.ToLower()));
            }
            var paginadoPublicaciones = ListaPagina<Publicacion>.Creacion(posts, filtros.numeroPagina, filtros.cantidadItemPagina);
            
            return paginadoPublicaciones;
        }

        public async Task InsertPost(Publicacion post)
        {
            var user = await _unitOfWork.UserRepositorio.GetById(post.IdUsuario);
            if (user == null)
            {
                throw new NegocioExcepcion("Usuario inexitente"); 
            }
            var userPost = await _unitOfWork.PostRepositorio.GetPostsByUser(post.IdUsuario);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Fecha).FirstOrDefault();
                if ((DateTime.Now - lastPost.Fecha).TotalDays < 7)
                {
                    throw new NegocioExcepcion("No puedes publicar ");
                }                
            }          
            if (post.Descripcion.Contains("Sexo"))
            {
                throw new NegocioExcepcion("Contenido no permitido");
            }
            await _unitOfWork.PostRepositorio.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpDatePost(Publicacion publi)
        {
            _unitOfWork.PostRepositorio.Update(publi);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepositorio.Delete(id);
            return true;
        }
    }
}   
