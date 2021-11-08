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
        private readonly IRepository<Publicacion> _postRepositorio;
        private readonly IRepository<Usuario> _usuarioRepositorio;
        public PublicacionServicio(IRepository<Publicacion> postRepositorio, IRepository<Usuario> usuarioRepositorio)
        {
            _postRepositorio = postRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        
        public async Task<Publicacion> GetPost(int id)
        {
            return await _postRepositorio.GetById(id);
        }

        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            return await _postRepositorio.GetAll();
        }

        public async Task InsertPost(Publicacion post)
        {
            var user = await _usuarioRepositorio.GetById(post.IdUsuario);
            if (user == null)
            {
                throw new Exception("Usuario inexitente"); 
            }
            if (post.Descripcion.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await _postRepositorio.Add(post);
        }

        public async Task<bool> UpDatePost(Publicacion publi)
        {
           await _postRepositorio.Update(publi);
            return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _postRepositorio.Delete(id);
            return true;
        }
    }
}
