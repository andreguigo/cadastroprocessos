using CadProcessosApp.Models;
using CadProcessosApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CadProcessosApp.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? pagina)
        {
            var processos = await _unitOfWork.ProcessoRepository.Index();
            foreach (var processo in processos)
                processo.ConverterRawParaNPU();
            
            int tamanhoPagina = 3;
            int numeroPagina = pagina ?? 1;

            TempData["Pagina"] = pagina;
                        
            return View(processos.ToPagedList(numeroPagina, tamanhoPagina));
        }

        public async Task<IActionResult> Detalhes(Guid id, int pagina = 1)
        {
            var processo = await _unitOfWork.ProcessoRepository.Detalhes(id);
            if (processo == null) return NotFound();

            processo.DataVisualizacao = DateTime.Now;
            _unitOfWork.ProcessoRepository.SalvarEdicao(processo);
            await _unitOfWork.CompleteAsync();

            processo.ConverterRawParaNPU();

            ViewBag.Pagina = TempData["Pagina"];
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
            if (id != processo.Id) return BadRequest();

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

        public async Task<IActionResult> Excluir(Guid id)
        {
            var processo = await _unitOfWork.ProcessoRepository.Excluir(id);
            if (processo == null) return NotFound();
            return View(processo);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
        {
            var processo = await _unitOfWork.ProcessoRepository.Excluir(id);
            if (id != processo.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {                    
                    _unitOfWork.ProcessoRepository.ConfirmarExclusao(processo);
                    await _unitOfWork.CompleteAsync();
    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir o Processo. Tente novamente.");
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}