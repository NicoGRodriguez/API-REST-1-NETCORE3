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
        private readonly IUnitOfWork _unitOfWork;
        public PublicacionServicio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Publicacion> GetPost(int id)
        {
            return await _unitOfWork.PostRepositorio.GetById(id);
        }

        public  IEnumerable<Publicacion> GetPosts()
        {
            return  _unitOfWork.PostRepositorio.GetAll();
        }

        public async Task InsertPost(Publicacion post)
        {
            var user = await _unitOfWork.UserRepositorio.GetById(post.IdUsuario);
            if (user == null)
            {
                throw new Exception("Usuario inexitente"); 
            }
            if (post.Descripcion.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }
            await _unitOfWork.PostRepositorio.Add(post);
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
