using KutuphaneYonetim.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneYonetim.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "Rol adı boş olamaz.");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                return RedirectToAction("List");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

 

  
[HttpGet]
public async Task<IActionResult> Delete(string id)
{
    if (string.IsNullOrEmpty(id))
    {
        return NotFound("Rol ID bulunamadı.");
    }

    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound("Rol bulunamadı.");
    }

    return View(role);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(string id)
{
    if (string.IsNullOrEmpty(id))
    {
        return NotFound("Rol ID bulunamadı.");
    }

    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound("Rol bulunamadı.");
    }

    var result = await _roleManager.DeleteAsync(role);

    if (result.Succeeded)
    {
        return RedirectToAction("List");
    }

    ModelState.AddModelError("", "Rol silinemedi.");
    return View("Delete", role);
}

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            ViewBag.userId = id; 

            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var roleViewModelList = new List<AssignRoleViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var assignRoleToUserViewModel = new AssignRoleViewModel()
                {
                    UserId = currentUser.Id,
                    UserName = currentUser.UserName,
                    Roles = new List<RoleItem>
            {
                new RoleItem
                {
                    RoleName = role.Name,
                    IsSelected = userRoles.Contains(role.Name) 
                }
            }
                };

                roleViewModelList.Add(assignRoleToUserViewModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, List<AssignRoleViewModel> requestList)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("Kullanıcı ID bulunamadı.");
            }

            var userToAssignRoles = await _userManager.FindByIdAsync(userId);
            if (userToAssignRoles == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var currentRoles = await _userManager.GetRolesAsync(userToAssignRoles);

            var selectedRoles = requestList
                .SelectMany(model => model.Roles)
                .Where(role => role.IsSelected)
                .Select(role => role.RoleName)
                .ToList();

            var rolesToRemove = currentRoles.Except(selectedRoles).ToList();
            var rolesToAdd = selectedRoles.Except(currentRoles).ToList();

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(userToAssignRoles, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Roller kaldırılırken bir hata oluştu.");
                    return View(requestList);
                }
            }

            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(userToAssignRoles, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Roller atanırken bir hata oluştu.");
                    return View(requestList);
                }
            }

            return RedirectToAction("List", "User");
        }


        [HttpGet]
public async Task<IActionResult> Edit(string id)
{
    if (string.IsNullOrEmpty(id))
    {
        return NotFound("Rol ID bulunamadı.");
    }

    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound("Rol bulunamadı.");
    }

    return View(role);
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, string roleName)
{
    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(roleName))
    {
        ModelState.AddModelError("", "Geçerli bir rol adı giriniz.");
        return View();
    }

    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound("Rol bulunamadı.");
    }

    role.Name = roleName;
    var result = await _roleManager.UpdateAsync(role);

    if (result.Succeeded)
    {
        return RedirectToAction("List");
    }

    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error.Description);
    }

    return View(role);
}
    }
}