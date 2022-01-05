using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;

namespace BibliotecaJogosAPI.Repository.Interface
{
    public interface IJogoRepository
    {
        public IEnumerable<Jogo> SelectTodosJogos(BibliotecaDBContext _context);
        public Jogo? SelectJogoPorID(BibliotecaDBContext _context, int id);
        public IEnumerable<Jogo> SelectJogoPorNome(BibliotecaDBContext _context, string nome);
        public bool DeleteJogo(BibliotecaDBContext _context, int id);
        public bool InsertJogo(BibliotecaDBContext _context, Jogo jogo);
        public bool UpdateJogo(BibliotecaDBContext _context, Jogo jogo);
        public bool SaveChanges(BibliotecaDBContext _context);
    }
}
