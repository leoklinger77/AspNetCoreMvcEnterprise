using AutoMapper;
using Enterprise.App.Extensions;
using Enterprise.App.ViewModels;
using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enterprise.App.Controllers
{
    [Authorize]    
    public class SupplierController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;

        public SupplierController(IMapper mapper, INotification notification, ISupplierRepository supplierRepository,
            ISupplierService supplierService) : base(mapper, notification)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
        }

        [AllowAnonymous]        
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<ICollection<SupplierViewModel>>(await _supplierRepository.FindAll()));
        }

        [AllowAnonymous]        
        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return NotFound();

            return View(supplier);
        }
                
        [ClaimsAuthorize("Supplier", "Insert")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]        
        [ClaimsAuthorize("Supplier", "Insert")]        
        public async Task<IActionResult> Create(SupplierViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierService.Insert(supplier);
            if (OperationIsValid()) return View(viewModel);
            return RedirectToAction(nameof(Index));
        }
                
        [ClaimsAuthorize("Supplier", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost]        
        [ClaimsAuthorize("Supplier", "Edit")]        
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (!ModelState.IsValid) return NotFound();

            var supplier = _mapper.Map<Supplier>(viewModel);
            await _supplierService.Update(supplier);
            if (OperationIsValid()) return View(viewModel);
            return RedirectToAction(nameof(Index));
        }
        
        [ClaimsAuthorize("Supplier", "Remove")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);
            if (supplier is null) return BadRequest();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]        
        [ClaimsAuthorize("Supplier", "Remove")]        
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await FindSupplierAndAddressAndProduct(id);
            if (supplier is null) return BadRequest();
            await _supplierService.Delete(id);
            if (OperationIsValid()) return View(supplier);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]        
        public async Task<IActionResult> FindAddress(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);

            if (supplier is null) return BadRequest();

            return PartialView("_DetailsAddress", supplier);
        }
        [HttpGet]
        [ClaimsAuthorize("Supplier", "Update")]        
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await FindSupplierAndAddress(id);

            if (supplier is null) return BadRequest();

            return PartialView("_UpdateAddress", new SupplierViewModel { Endereco = supplier.Endereco });
        }

        [HttpPost]        
        [ClaimsAuthorize("Supplier", "Update")]        
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplier)
        {
            ModelState.Remove("Documento");
            ModelState.Remove("Nome");
            if (!ModelState.IsValid) return PartialView("_UpdateAddress", supplier);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(supplier.Endereco));
            if (OperationIsValid()) return View(supplier);

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
