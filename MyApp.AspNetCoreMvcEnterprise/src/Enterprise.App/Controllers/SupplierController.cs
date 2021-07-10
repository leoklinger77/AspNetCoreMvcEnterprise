using AutoMapper;
using Enterprise.App.ViewModels;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enterprise.App.Controllers
{
    [Route("fornecedores")]
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierController(IMapper mapper, ISupplierRepository supplierRepository, IAddressRepository addressRepository) : base(mapper)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<ICollection<SupplierViewModel>>(await _supplierRepository.FindAll()));
        }

        [Route("lista-de-fornecedores/detalhes-fornecdor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return NotFound();

            return View(supplier);
        }

        [Route("novo-fornecedor")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-fornecedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierRepository.Insert(supplier);

            return RedirectToAction(nameof(Index));
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost]
        [Route("editar-fornecedor/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (!ModelState.IsValid) return NotFound();

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierRepository.Update(supplier);

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [Route("excluir-fornecedor/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            await _supplierRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
                
        public async Task<IActionResult> FindAddress(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);

            if (supplier is null) return BadRequest();

            return PartialView("_DetailsAddress", supplier);
        }

        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);

            if (supplier is null) return BadRequest();

            return PartialView("_UpdateAddress", new SupplierViewModel { Endereco = supplier.Endereco });
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplier)
        {
            ModelState.Remove("Documento");
            ModelState.Remove("Nome");
            if (!ModelState.IsValid) return PartialView("_UpdateAddress", supplier);

            await _addressRepository.Update(_mapper.Map<Address>(supplier.Endereco));

            var url = Url.Action("FindAddress", "Supplier", new { id = supplier.Id });

            return Json(new { Success = true, url });
        }

        private async Task<SupplierViewModel> FindSupplierAndAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.FindSupplierAndAddress(id));
        }
        private async Task<SupplierViewModel> FindSupplierAndAddressAndProduct(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.FindSupplierAndAddressAndProduct(id));
        }
    }
}
