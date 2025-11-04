public class AccountService : IAccountService
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;

    public AccountService(UserManager<Users> userManager, SignInManager<Users> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = new Users 
        { 
            UserName = model.Email, 
            Email = model.Email, 
            // Map other properties from your RegisterViewModel to your Users model
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        return result;
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        // Ensure you are searching by username or email as appropriate for your setup
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        return result;
    }
}