using AutoMapper;
using Enterprise.App.ViewModels;
using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Enterprise.App.Controllers
{
    [Route("Produtos")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, INotification notification, IProductRepository productRepository, 
            ISupplierRepository supplierRepository, IProductService productService) : base(mapper, notification)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _productService = productService;
        }
        [Route("lista-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<ICollection<ProductViewModel>>(await _productRepository.FindAll()));
        }
        [Route("detalhes-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await FindProduct(id);
            if (supplier is null) return NotFound();

            return View(supplier);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            ProductViewModel viewModel = await PopularSupplier(new ProductViewModel());
            return View(viewModel);
        }

        [HttpPost]
        [Route("novo-produto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            viewModel = await PopularSupplier(viewModel);
            if (!ModelState.IsValid) return View(viewModel);

            var imgPath = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageUpload.FileName);

            if (!await UploadFile(viewModel.ImageUpload, imgPath)) return View(viewModel);

            viewModel.Image = imgPath;

            await _productService.Insert(_mapper.Map<Product>(viewModel));

            if (OperationIsValid()) return View(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await FindProduct(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost]
        [Route("editar-produto/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            var productUpdate = await FindProduct(id);
            viewModel.Supplier = productUpdate.Supplier;
            viewModel.Image = productUpdate.Image;
            if (!ModelState.IsValid) return View(viewModel);

            if (viewModel.ImageUpload != null)
            {
                var imgPath = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageUpload.FileName);
                if (!await UploadFile(viewModel.ImageUpload, imgPath)) return View(viewModel);
                productUpdate.Image = imgPath;
            }
            productUpdate.Nome = viewModel.Nome;
            productUpdate.Valor = viewModel.Valor;
            productUpdate.Descricao = viewModel.Descricao;
            productUpdate.Ativo = viewModel.Ativo;

            await _productService.Update(_mapper.Map<Product>(productUpdate));

            if (OperationIsValid()) return View(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await FindProduct(id);
            if (product is null) return BadRequest();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Route("excluir-produto/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var db = await FindProduct(id);
            if (db is null) return BadRequest();

            await _productService.Delete(id);

            if (OperationIsValid()) return View(db);
            TempData["Sucesso"] = "Produto excluido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> FindProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.FindProductSupplier(id));
            product.SupplierList = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.FindAll());
            return product;
        }

        private async Task<ProductViewModel> PopularSupplier(ProductViewModel viewModel)
        {
            viewModel.SupplierList = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.FindAll());
            return viewModel;
        }

        private async Task<bool> UploadFile(IFormFile imageUpload, string imgPath)
        {
            if (imageUpload.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", imgPath);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageUpload.CopyToAsync(stream);
            }

            return true;
        }
    }
}
