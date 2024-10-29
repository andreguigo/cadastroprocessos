using Microsoft.EntityFrameworkCore;

namespace CadProcessosApp.Models
{
    public class Paginacao<T> : List<T>
    {
        public int PaginaAtual { get; private set; }
        public int TotalPaginas { get; private set; }

        public Paginacao(List<T> itens, int contagem, int paginaAtual, int tamanhoPagina)
        {
            PaginaAtual = paginaAtual;
            TotalPaginas = (int)Math.Ceiling(contagem / (double)tamanhoPagina);

            this.AddRange(itens);
        }

        public bool PaginaAnterior => PaginaAtual > 1;
        public bool ProximaPagina => PaginaAtual < TotalPaginas;

        public static async Task<Paginacao<T>> CreateAsync(IQueryable<T> fonte, int paginaAtual, int tamanhoPagina)
        {
            var contagem = await fonte.CountAsync();
            var itens = await fonte.Skip((paginaAtual - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            
            return new Paginacao<T>(itens, contagem, paginaAtual, tamanhoPagina);
        }
    }
}