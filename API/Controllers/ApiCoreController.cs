namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCoreController : ControllerBase
    {
        private Context _context;
        private JwtService _jwtService;

        protected Context Context => _context ??= HttpContext.RequestServices.GetService<Context>();
        protected JwtService JwtService => _jwtService ??= HttpContext.RequestServices.GetService<JwtService>();
    }
}
