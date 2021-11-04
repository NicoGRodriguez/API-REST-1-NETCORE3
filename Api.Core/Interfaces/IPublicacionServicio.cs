﻿using Api.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IPublicacionServicio
    {
        Task<IEnumerable<Publicacion>> GetPosts();
        Task<Publicacion> GetPost(int id);
        Task InsertPost(Publicacion post);
        Task<bool> UpDatePost(Publicacion publi);
        Task<bool> DeletePost(int Id);
    }
}