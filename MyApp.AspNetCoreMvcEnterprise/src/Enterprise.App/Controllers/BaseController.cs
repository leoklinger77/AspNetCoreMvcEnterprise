using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        public BaseController(IMapper mapper = null)
        {
            _mapper = mapper;
        }
    }
}
