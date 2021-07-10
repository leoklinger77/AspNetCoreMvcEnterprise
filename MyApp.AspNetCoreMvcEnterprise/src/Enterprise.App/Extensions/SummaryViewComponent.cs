using Enterprise.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotification _notification;

        public SummaryViewComponent(INotification notification)
        {
            _notification = notification;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifier = await Task.FromResult(_notification.FindAll());

            notifier.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x.Message));

            return View(notifier);
        }
    }
}
