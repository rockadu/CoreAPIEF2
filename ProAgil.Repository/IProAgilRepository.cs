using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        // GERAL
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         // EVENTOS
         Task<Evento[]> GetAllEvetoAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEvetoAsync(bool includePalestrantes);
         Task<Evento> GetEvetoAsyncById(int id, bool includePalestrantes);

         // PALESTRANTE
         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos);
         Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEventos);

    }
}