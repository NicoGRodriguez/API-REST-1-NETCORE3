using Api.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Interfaces
{
    public interface IRepositorio<T> where T : BaseEntidad
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T ent);
        void Update(T ent);
        Task Delete(int id);

    }
}
