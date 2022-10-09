using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rentid.Entities;
using rentid.ViewModels;

namespace rentid.Controllers
{
    public class girisController:Controller
    {
        private readonly UserManager<appUser> userManager;
        private readonly SignInManager<appUser> signInManager;

        public girisController(UserManager<appUser> userManager,SignInManager<appUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult index(string returnUrl)
        {
          TempData["returnUrl"] = returnUrl;
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> index(girisVM vm)
        {
          if (ModelState.IsValid)
          {
              appUser user = await userManager.FindByEmailAsync(vm.email);

              if (user != null)
              {
                  //Cookie de ki bilgileri siler!
                  await signInManager.SignOutAsync();

                    //appUser , password , isPersistant , lookOutOnFailure
                    //Kullanıcı , Sifre , Beni Hatırla , Hatalı giriş yapılınca bir süre engelleme
                    Microsoft.AspNetCore.Identity.SignInResult res = await signInManager.PasswordSignInAsync(user,vm.sifre,vm.beniHatirla,false);

                    // 1) Res.Success - 2)Res.RequiresTwoFacror (2 adımlı doğrulama) - 3)res.isLockeOut (Hatalı giriş sonucu kitlimi değil mi)  -  4) res.isNotAllowed (Kısıtlı erişimde doğru bilgileri girdi mi girmedi mi)
                    if (res.Succeeded)
                    {
                        if (TempData["returnUrl"] != null)
                        {
                            return Redirect(TempData["returnUrl"].ToString());
                        }
                        return RedirectToAction("index","profil");
                    }
                    else{
                      ModelState.AddModelError("","*E-posta adresi veya şifre yanlış!");
                      return View(vm);
                    }


              }
              else{
                ModelState.AddModelError("","*Kayıtlı E-posta adresi bulunamadı!");
              }
          }
          return View(vm);
        }
    }
}