using abposus.Data;
using abposus.Interfaces;
using abposus.Models;
using abposus.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace abposus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetUsers();
            return View(users);
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(user != null)
            {
                var passworkCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if(passworkCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginViewModel);
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Type = registerViewModel.Type
            };

            var newUserReponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if(newUserReponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, registerViewModel.Type.ToString());
            }

            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
