using CadProcessosApp.Data;
using CadProcessosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CadProcessosApp.Repositories
{
    public class ProcessoRepository : IProcessoRepository
    {
        private readonly ProcessoDbContext _contexto;

        public ProcessoRepository(ProcessoDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Processo>> Index()
        {
            return await _contexto.Processos.ToListAsync();
        }

        public async Task<Processo> Detalhes(Guid id)
        {
            return await _contexto.Processos.FindAsync(id);
        }

        public async Task Inserir(Processo processo)
        {
            await _contexto.Processos.AddAsync(processo);
        }

        public async Task<Processo> Editar(Guid id)
        {
            return await _contexto.Processos.FindAsync(id);
        }

        public void SalvarEdicao(Processo processo)
        {
            _contexto.Processos.Update(processo);
        }

        public async Task<Processo> Excluir(Guid id)
        {
            return await _contexto.Processos.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public void ConfirmarExclusao(Processo processo)
        {
            _contexto.Processos.Remove(processo);
        }
    }
}