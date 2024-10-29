using CadProcessosApp.Data;
using CadProcessosApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadProcessosApp.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly ProcessoDbContext _contexto;
        private const int TamanhoPagina = 5;

        public ProcessoController(ProcessoDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: Processo
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            int tamanhoPagina = TamanhoPagina;
            var processos = _contexto.Processos.AsQueryable();

            foreach (var processo in processos)
                processo.ConverterRawParaNPU();

            TempData["PaginaAtual"] = paginaAtual;

            return View(await Paginacao<Processo>.CreateAsync(processos, paginaAtual ?? 1, tamanhoPagina));
        }

        // GET: Processo/Detalhes/{Guid}
        public async Task<IActionResult> Detalhes(Guid? id, int paginaAtual = 1)
        {
            var processo = await _contexto.Processos.FindAsync(id);
            if (processo == null) return NotFound();

            processo.DataVisualizacao = DateTime.Now;
            _contexto.Update(processo);
            await _contexto.SaveChangesAsync();

            processo.ConverterRawParaNPU();

            ViewBag.PaginaAtual = TempData["PaginaAtual"];

            return View(processo);
        }

        // GET: Processo/Inserir
        public IActionResult Inserir()
        {
            return View();
        }

        // POST: Processo/Inserir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir(Processo processo)
        {
            if (ModelState.IsValid)
            {
                processo.Id = Guid.NewGuid();
                processo.DataCadastro = DateTime.Now;
                processo.ConverterNPUParaRaw();

                _contexto.Add(processo);
                await _contexto.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(processo);
        }

        // GET: Processo/Editar/{Guid}
        public async Task<IActionResult> Editar(Guid? id)
        {
            if (id == null)
                return NotFound();

            var processo = await _contexto.Processos.FindAsync(id);
            if (processo == null)
                return NotFound();
            
            processo.ConverterRawParaNPU();

            return View(processo);
        }

        // POST: Processo/Editar/{Guid}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, Processo processo)
        { 
            if (id!= processo.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                processo.DataCadastro = DateTime.Now;
                processo.ConverterNPUParaRaw();
            
                _contexto.Update(processo);
                await _contexto.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }

            return View(processo);
        }

        // GET: Processo/Excluir/{id}
        public async Task<IActionResult> Excluir(Guid? id)
        {
            if (id == null)
                return NotFound();

            var processo = await _contexto.Processos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processo == null)
                return NotFound();

            return View(processo);
        }

        // POST: Processo/Excluir/{id}
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExcluir(Guid id)
        {
            var processo = await _contexto.Processos.FindAsync(id);
            
            _contexto.Processos.Remove(processo!);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ExisteProcesso(Guid id)
        {
            return _contexto.Processos.Any(e => e.Id == id);
        }
    }
}