namespace CadProcessosApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProcessoRepository ProcessoRepository { get; }
        Task<int> CompleteAsync();
    }
}