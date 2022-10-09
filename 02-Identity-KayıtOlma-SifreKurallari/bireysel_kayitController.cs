using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rentid.Entities;
using rentid.ViewModels;

namespace rentid.Controllers
{
    [Route("bireysel-kayit")]
    public class bireysel_kayitController:Controller
    {
        private readonly UserManager<appUser> userManager;
        public bireysel_kayitController(UserManager<appUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult index()
        {
          //TODO: Implement Realistic Implementation
          return View();
        }
        [HttpPost]
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