using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rentid.Entities;
using rentid.ViewModels;

namespace rentid.Controllers
{
    
    public class bireysel_kayitController:Controller
    {
        private readonly UserManager<appUser> userManager;
        public bireysel_kayitController(UserManager<appUser> userManager)
        {
            this.userManager = userManager;
        }
        [Route("/bireysel-kayit")]
        public IActionResult index()
        {
          //TODO: Implement Realistic Implementation
          return View();
        }
        [HttpPost]
        [Route("/bireysel-kayit")]
        public async Task<IActionResult> index(bireyselKayitVM vm)
        {
          if (ModelState.IsValid)
          {
              appUser user = new appUser();
              user.ad = vm.ad;
              user.soyad = vm.soyad;
              user.Email = vm.email;
              user.UserName = vm.email;
              user.kvkk = vm.kvkk;
              user.kayitTarihi = DateTime.Now.Date;

              IdentityResult res = await userManager.CreateAsync(user,vm.sifre);

              if (res.Succeeded)
              {
                  //Kayıt anında rol tanımlama
                  await userManager.AddToRoleAsync(user,"Bireysel");

                  string emailConfirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                  string link = Url.Action("email_dogrulama","bireysel_kayit" , new {
                    userId = user.Id,
                    token = emailConfirmToken
                  },HttpContext.Request.Scheme);
                  HelperMethods.emailDogrulama.emailConfirmSendMail(link,user.Email);

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

        public async Task<IActionResult> email_dogrulama(string userid,string token)
        {
          appUser user = await userManager.FindByIdAsync(userid);

          if (user != null)
          {
              IdentityResult res = await userManager.ConfirmEmailAsync(user,token);

              if (res.Succeeded)
              {
                  return RedirectToAction("index","giris");
              }
              else{
                ModelState.AddModelError("","*Hata ile karşılaşıldı. Lütfen hizmet sağlayıcınızla iletişime geçin!");
              }
          }

          return View();
        }
    }
}