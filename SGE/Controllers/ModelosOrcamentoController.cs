using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Models;

namespace SGE.Controllers
{
    [Authorize]
    public class ModelosOrcamentoController : Controller
    {
        private readonly SGEMvcContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ModelosOrcamentoController(SGEMvcContext context,
            IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ModelosOrcamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModeloOrcamento.ToListAsync());
        }

        // GET: ModelosOrcamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloOrcamento = await _context.ModeloOrcamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloOrcamento == null)
            {
                return NotFound();
            }

            return View(modeloOrcamento);
        }

        // GET: ModelosOrcamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelosOrcamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,TextoInicial,TextoFinal,Assinatura,Rodapé,Id,CNPJ,RazaoEmpresa")] ModeloOrcamento modeloOrcamento)
        {
            if (ModelState.IsValid)
            {
                modeloOrcamento.Status = Status.Ativo;
                modeloOrcamento.DataCadastro = DateTime.Now;
                _context.Add(modeloOrcamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", new { id = modeloOrcamento.Id });
        }

        // GET: ModelosOrcamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloOrcamento = await _context.ModeloOrcamento.FindAsync(id);
            if (modeloOrcamento == null)
            {
                return NotFound();
            }
            ViewBag.Nome = modeloOrcamento.Nome;
            ViewData["Logomarca"] = string.Format("/images/{0}", modeloOrcamento.LogomarcaPath);
            return View(modeloOrcamento);
        }

        // POST: ModelosOrcamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,TextoInicial,TextoFinal,Assinatura,Rodapé,Id,Status,CNPJ,RazaoEmpresa,LogomarcaPath,ContentType")] ModeloOrcamento modeloOrcamento)
        {
            if (id != modeloOrcamento.Id)
            {
                return NotFound();
            }
          
            if (ModelState.IsValid)
            {
                try
                {
                    modeloOrcamento.DataAlteracao = DateTime.Now;
                    _context.Update(modeloOrcamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloOrcamentoExists(modeloOrcamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Nome = modeloOrcamento.Nome;
            return View(modeloOrcamento);
        }


        //[HttpPost]
        //public IActionResult UploadImagem(IList<IFormFile> arquivos, int id)
        //{
        //    IFormFile imagemEnviada = arquivos.FirstOrDefault();
        //    if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        imagemEnviada.OpenReadStream().CopyTo(ms);

        //        var imagemEntity = _context.ModeloOrcamento.Find(id);

        //        imagemEntity.Logomarca = ms.ToArray();
        //        imagemEntity.ContentType = imagemEnviada.ContentType;
        //        _context.Update(imagemEntity);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("Edit",new { id });
        //}

        [HttpPost]
        public IActionResult UploadImagem(IList<IFormFile> arquivos, int id)
        {
            IFormFile imagemEnviada = arquivos.FirstOrDefault();
            if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
            {
                var imageName = Path.GetFileName(imagemEnviada.FileName);
                var imageExt = Path.GetExtension(imageName);
                var imagePath = String.Empty;
                if (imageExt.ToUpper() == ".JPG" || imageExt == ".PNG" || imageExt == ".JPEG")
                {
                    imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "images", imageName);
                    using (var stream = System.IO.File.Create(imagePath))
                    {
                        imagemEnviada.CopyTo(stream);
                    }
                }

                var imagemEntity = _context.ModeloOrcamento.Find(id);
                imagemEntity.LogomarcaPath = imageName;
                imagemEntity.ContentType = imagemEnviada.ContentType;
                _context.Update(imagemEntity);
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", new { id });
        }

        [HttpGet]
        public FileStreamResult VerImagem(int id)
        {
            var imagem = _context.ModeloOrcamento.FirstOrDefault(m => m.Id == id);
            if (imagem.Logomarca != null)
            {
                MemoryStream ms = new MemoryStream(imagem.Logomarca);
                return new FileStreamResult(ms, imagem.ContentType);
            }
            else
                return null;
        }

        // GET: ModelosOrcamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloOrcamento = await _context.ModeloOrcamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloOrcamento == null)
            {
                return NotFound();
            }

            return View(modeloOrcamento);
        }

        // POST: ModelosOrcamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeloOrcamento = await _context.ModeloOrcamento.FindAsync(id);
            _context.ModeloOrcamento.Remove(modeloOrcamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloOrcamentoExists(int id)
        {
            return _context.ModeloOrcamento.Any(e => e.Id == id);
        }
    }
}
