using AutoMapper;
using Enterprise.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        private readonly INotification _notification;
        public BaseController(IMapper mapper, INotification notification)
        {
            _mapper = mapper;
            _notification = notification;
        }

        protected bool OperationIsValid()
        {
            return _notification.HasNotification();
        }
    }
}
