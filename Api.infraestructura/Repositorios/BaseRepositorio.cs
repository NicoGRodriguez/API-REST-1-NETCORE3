using Api.Core.Entidades;
using Api.Core.Interfaces;
using Api.infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.infraestructura.Repositorios
{
    public class BaseRepositorio<T> : IRepository<T> where T : BaseEntidad
    {
        private readonly SocialApiContext _context;
        private readonly DbSet<T> _entidades;
        public BaseRepositorio(SocialApiContext context)
        {
            _context = context;
            _entidades = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entidades.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _entidades.FindAsync(id);  
        }
        public async Task Add(T ent)
        {
            _entidades.Add(ent);
            await _context.SaveChangesAsync();
        }          
        public async Task Update(T ent)
        {
            _entidades.Update(ent);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            T ent = await GetById(id);
            _entidades.Remove(ent);
            _context.SaveChanges();
        }   
    }
}
