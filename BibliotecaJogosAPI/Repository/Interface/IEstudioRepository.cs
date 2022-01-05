using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;

namespace BibliotecaJogosAPI.Repository.Interface
{
    public interface IEstudioRepository
    {
        public IEnumerable<Estudio> SelectTodosEstudios(BibliotecaDBContext _context);
        public Estudio? SelectEstudioPorID(BibliotecaDBContext _context, int id);
        public IEnumerable<Estudio> SelectEstudioPorNome(BibliotecaDBContext _context, string nome);
        public bool DeleteEstudio(BibliotecaDBContext _context, int id);
        public bool InsertEstudio(BibliotecaDBContext _context, Estudio estudio);
        public bool UpdateEstudio(BibliotecaDBContext _context, Estudio estudio);
        public bool SaveChanges(BibliotecaDBContext _context);
    }
}
