using Api.Core.Entidades;
using Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Servicios
{
    public class PublicacionServicio : IPublicacionServicio
    {
        private readonly IPostRepositorio _postRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public PublicacionServicio(IPostRepositorio postRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _postRepositorio = postRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepositorio.DeletePost(id);
        }

        public async Task<Publicacion> GetPost(int id)
        {
            return await _postRepositorio.GetPost(id);
        }

        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            return await _postRepositorio.GetPosts();
        }

        public async Task InsertPost(Publicacion post)
        {
            var user = await _usuarioRepositorio.GetUsuario(post.IdUsuario);
            if (user == null)
            {
                throw new Exception("Usuario inexitente"); 
            }
            if (post.Descripcion.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await _postRepositorio.InsertPost(post);
        }

        public async Task<bool> UpDatePost(Publicacion publi)
        {
           return await _postRepositorio.UpDatePost(publi);
        }
    }
}
