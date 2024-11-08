using CadProcessosApp.Data;

namespace CadProcessosApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProcessoDbContext _contexto;

        public UnitOfWork(ProcessoDbContext contexto)
        {
            _contexto = contexto;
            ProcessoRepository = new ProcessoRepository(_contexto);
        }

        public IProcessoRepository ProcessoRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}