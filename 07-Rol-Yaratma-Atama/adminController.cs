using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rentid.Entities;
using rentid.ViewModels;

namespace rentid.Controllers
{
    [Authorize(Roles = "Admin")]
    public class adminController:Controller
    {
        private readonly RoleManager<appRole> roleManager;
        public adminController(RoleManager<appRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult index()
        {
          ViewBag.roleCount = roleManager.Roles.Count();
          return View();
        }

        public IActionResult roles()
        {
          return View(roleManager.Roles.ToList());
        }

        [ActionName("roles-add")]
        public IActionResult roles_add()
        {

          return View();
        }

        [HttpPost]
        [ActionName("roles-add")]
        public async Task<IActionResult> roles_add(rolesAddVM vm)
        {
            if (ModelState.IsValid)
            {
                appRole role = await roleManager.FindByNameAsync(vm.rolAdi);

                if (role == null)
                {
                    appRole r = new appRole();
                    r.Name = vm.rolAdi;
                    IdentityResult res = await roleManager.CreateAsync(r);
                    
                    if (res.Succeeded)
                    {
                        return RedirectToAction("roles","admin");
                    }
                    else{
                        ModelState.AddModelError("","*Rol ekleme işlemi gerçekleştirilemedi!");
                        return View(vm);
                    }
                }
                else{
                    ModelState.AddModelError("","*Rol mevcut olduğundan ekleme işlemi gerçekleştirilemedi!");
                }
            }
          return View(vm);
        }

        /*Role Delete*/
        [HttpPost]
        public IActionResult roles_delete(string id)
        {
          appRole role = roleManager.FindByIdAsync(id).Result;

          if (role != null)
          {
              IdentityResult res = roleManager.DeleteAsync(role).Result;
              if (res.Succeeded)
              {
                  return RedirectToAction("roles","admin");
              }
          }
          return View();
        }
    }
}