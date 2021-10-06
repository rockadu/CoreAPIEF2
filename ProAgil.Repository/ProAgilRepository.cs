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
                                .Where(t => t.Tema.ToLower().Contains(tema.ToLower()));

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

        public async Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(c => c.RedesSociais);

            if(includeEventos)
            {
                query = query.Include(c => c.PalestrantesEventos)
                .ThenInclude(c => c.Evento);
            }

            query = query.Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                .Include(r => r.RedesSociais);

            if(includeEventos)
            {
                query = query
                            .Include(pe => pe.PalestrantesEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}