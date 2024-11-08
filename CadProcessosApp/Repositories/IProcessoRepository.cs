using System.Diagnostics;
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
    }
}