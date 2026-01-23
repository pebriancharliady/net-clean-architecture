using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PebriBox.WebAPI.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ISender _sender = null;
        public ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
