using AutoMapper;
using Enterprise.App.ViewModels;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Enterprise.App.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductController(IMapper mapper, IProductRepository productRepository, ISupplierRepository supplierRepository) : base(mapper)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<ICollection<ProductViewModel>>(await _productRepository.FindAll()));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await FindProduct(id);
            if (supplier is null) return NotFound();

            return View(supplier);
        }

        public async Task<IActionResult> Create()
        {
            ProductViewModel viewModel = await PopularSupplier(new ProductViewModel());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            viewModel = await PopularSupplier(viewModel);
            if (!ModelState.IsValid) return View(viewModel);

            var imgPath = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageUpload.FileName);

            if (!await UploadFile(viewModel.ImageUpload, imgPath)) return View(viewModel);

            viewModel.Image = imgPath;

            await _productRepository.Insert(_mapper.Map<Product>(viewModel));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await FindProduct(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost]
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

            await _productRepository.Update(_mapper.Map<Product>(productUpdate));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await FindProduct(id);
            if (product is null) return BadRequest();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (await FindProduct(id) is null) return BadRequest();
            await _productRepository.Delete(id);
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
