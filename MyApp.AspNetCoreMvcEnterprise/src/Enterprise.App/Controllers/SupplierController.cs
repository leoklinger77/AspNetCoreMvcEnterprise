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
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;       

        public SupplierController(IMapper mapper, ISupplierRepository supplierRepository) : base(mapper)
        {
            _supplierRepository = supplierRepository;            
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<ICollection<SupplierViewModel>>(await _supplierRepository.FindAll()));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return NotFound();

            return View(supplier);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierRepository.Insert(supplier);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (!ModelState.IsValid) return NotFound();

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierRepository.Update(supplier);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            await _supplierRepository.Delete(id);
            return RedirectToAction(nameof(Index));
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
