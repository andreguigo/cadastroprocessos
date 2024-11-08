using CadProcessosApp.Data;
using CadProcessosApp.Models;
using CadProcessosApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadProcessosApp.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var processos = await _unitOfWork.ProcessoRepository.Index();
            foreach (var processo in processos)
                processo.ConverterRawParaNPU();
            
            return View(processos);
        }

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var processo = await _unitOfWork.ProcessoRepository.Detalhes(id);
            if (processo == null) return NotFound();

            processo.DataVisualizacao = DateTime.Now;
            _unitOfWork.ProcessoRepository.SalvarEdicao(processo);
            await _unitOfWork.CompleteAsync();

            processo.ConverterRawParaNPU();
            return View(processo);
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir(Processo processo)
        {
            if (ModelState.IsValid)
            {
                processo.DataCadastro = DateTime.Now;
                processo.ConverterNPUParaRaw();

                await _unitOfWork.ProcessoRepository.Inserir(processo);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processo);
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var processo = await _unitOfWork.ProcessoRepository.Detalhes(id);
            if (processo == null) return NotFound();

            processo.ConverterRawParaNPU();
            return View(processo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, Processo processo)
        {
            if (id != processo.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    processo.ConverterNPUParaRaw();
                    
                    _unitOfWork.ProcessoRepository.SalvarEdicao(processo);
                    await _unitOfWork.CompleteAsync();
    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível atualizar o Processo. Tente novamente.");
                }

            }
            return View(processo);
        }

        // // GET: Processo/Excluir/{id}
        // public async Task<IActionResult> Excluir(Guid? id)
        // {
        //     if (id == null)
        //         return NotFound();

        //     var processo = await _contexto.Processos
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (processo == null)
        //         return NotFound();

        //     return View(processo);
        // }

        // // POST: Processo/Excluir/{id}
        // [HttpPost, ActionName("Excluir")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> ConfirmarExcluir(Guid id)
        // {
        //     var processo = await _contexto.Processos.FindAsync(id);
            
        //     _contexto.Processos.Remove(processo!);
        //     await _contexto.SaveChangesAsync();

        //     return RedirectToAction(nameof(Index));
        // }

        // private bool ExisteProcesso(Guid id)
        // {
        //     return _contexto.Processos.Any(e => e.Id == id);
        // }
    }
}