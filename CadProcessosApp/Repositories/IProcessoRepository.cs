using CadProcessosApp.Models;

namespace CadProcessosApp.Repositories
{
    public interface IProcessoRepository
    {
        Task<IEnumerable<Processo>> Index();
        Task<Processo> Detalhes(Guid id);
        Task Inserir(Processo processo);
        Task<Processo> Editar(Guid id);
        void SalvarEdicao(Processo processo);
        Task<Processo> Excluir(Guid id);
        void ConfirmarExclusao(Processo processo);
    }
}