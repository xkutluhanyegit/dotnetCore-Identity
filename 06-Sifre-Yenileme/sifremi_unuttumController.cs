using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rentid.Entities;
using rentid.ViewModels;

namespace rentid.Controllers
{
    
    public class sifremi_unuttumController:Controller
    {
        private readonly UserManager<appUser> userManager;
        public sifremi_unuttumController(UserManager<appUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult index()
        {
          //TODO: Implement Realistic Implementation
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> index(sifremi_unuttumVM vm)
        {
          if (ModelState.IsValid)
          {
              appUser user = await userManager.FindByEmailAsync(vm.email);

              if (user != null)
              {
                  string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                  string passwordResetLink = Url.Action("reset","sifremi_unuttum" , new {
                    userId = user.Id,
                    token = passwordResetToken
                  },HttpContext.Request.Scheme);

                  HelperMethods.sifreYenileme.passwordResetSendMail(passwordResetLink,vm.email);

                  return RedirectToAction("index","giris");
              }
              else{
                ModelState.AddModelError("","*Kayıtlı E-posta adresi bulunamadı!");
              }
          }
          return View();
        }

        public IActionResult reset(string userId , string token)
        {
          TempData["userId"] = userId;
          TempData["token"] = token;
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> reset(sifre_yenileVM vm)
        {
          string token = TempData["token"].ToString();
          string userId = TempData["userId"].ToString();

          appUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult res = await userManager.ResetPasswordAsync(user,token,vm.sifre);
                if (res.Succeeded)
                {
                  await userManager.UpdateSecurityStampAsync(user);
                    return RedirectToAction("index","giris");
                }
                else{
                  foreach (var item in res.Errors)
                  {
                      ModelState.AddModelError("",item.Description);
                  }
                }
        
            }
          return View(vm);
        }

    }
}