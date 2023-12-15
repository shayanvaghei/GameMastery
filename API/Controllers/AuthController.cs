

namespace API.Controllers
{
    public class AuthController : ApiCoreController
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApplicationUserDto>> RefreshPage()
        {
            var user = await Context.Users.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == User.GetUserId());

            if (user == null) return Unauthorized();

            return CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUserDto>> Login(LoginDto model)
        {
            var user = await Context.Users.Include(x => x.Country).FirstOrDefaultAsync(x => x.UserName == model.UserName);

            if (user == null) return Unauthorized("Invalid username or password");

            var result = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!result)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(CreateApplicationUserDto(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUserDto>> Register(RegisterDto model)
        {
            if (await PlayerNameTakenAsync(model.PlayerName))
            {
                return BadRequest("Player name is taken, please choose another name");
            }

            var country = await Context.Country.FindAsync(model.CountryId);
            if (country == null) return BadRequest("Please choose your country");

            var userToAdd = new ApplicationUser
            {
                PlayerName = model.PlayerName,
                UserName = model.PlayerName.ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password, SD.BCRYPT_WORK_FACTOR),
                // CountryId = country.Id
                Country = country
            };

            Context.Users.Add(userToAdd);
            await Context.SaveChangesAsync();

            return Ok(CreateApplicationUserDto(userToAdd));
        }

        private ApplicationUserDto CreateApplicationUserDto(ApplicationUser user)
        {
            var jwt = JwtService.CreateToken(user);
            var cookieOptions = new CookieOptions { IsEssential = true, HttpOnly = true, Expires = DateTime.UtcNow.AddDays(1) };
            Response.Cookies.Append(SD.GameMasteryToken, jwt, cookieOptions);

            return new ApplicationUserDto
            {
                PlayerName = user.PlayerName,
                CountryName = user.Country.Name
            };
        }

        private async Task<bool> PlayerNameTakenAsync(string playerName)
        {
            return await Context.Users.AnyAsync(x => x.UserName == playerName.ToLower());
        }
    }
}
