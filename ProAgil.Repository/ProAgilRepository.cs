using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public ProAgilContext _context { get; }

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEvetoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lotes)
                                            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestrantesEventos)
                .ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(d => d.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEvetoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lotes)
                                            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestrantesEventos)
                .ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(d => d.DataEvento)
                                .Where(t => t.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEvetoAsyncById(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lotes)
                                            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestrantesEventos)
                .ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(d => d.DataEvento)
                                .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public Task<Evento[]> GetPalestranteAsync(int palestranteId, bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }

        public Task<Evento[]> GetAllPalestrantesAsyncByName(bool includePalestrantes)
        {
            throw new System.NotImplementedException();
        }
    }
}