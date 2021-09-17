using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRE.Web.UI.Areas.Admin.Models;
using AutoMapper;
using HRE.Core.Identity;
using HRE.Core.Shared.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class LoginsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;
        private readonly CustomUserManager _userManager;
        private readonly CustomRoleManager _roleManager;
        private readonly IMapper _mapper;

        public LoginsController(HrDbContext dbContext, CustomUserManager userManager, CustomRoleManager roleManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // GET: Admin/Logins
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            return View(users);
        }

        // GET: Admin/Logins/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Admin/Logins/Create
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            CreateUserViewModel model = new();
            ViewBag.BODID = new SelectList(await _dbContext.BODs.ToListAsync(cancellationToken), "Id", "Code");
            ViewBag.RoleID = new SelectList(await _dbContext.Roles.ToListAsync(cancellationToken), "Id", "RoleName");
            return View(model);
        }

        // POST: Admin/Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleID.ToString());
                CustomUser user = new()
                {
                    UserName = model.UserName,
                    FullName = model.UserName,
                    IsDisabled = model.IsDisabled,
                    BODID = model.BODID
                };
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, role.Name);
                return RedirectToAction("Index");
            }

            ViewBag.BODID = new SelectList(_dbContext.BODs, "Id", "Code", model.BODID);
            ViewBag.RoleID = new SelectList(_dbContext.Roles, "Id", "RoleName", model.RoleID);
            return View(model);
        }

        // GET: Admin/Logins/Edit/5
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.BODID = new SelectList(await _dbContext.BODs.ToListAsync(cancellationToken), "Id", "Code", user.BODID);
            ViewBag.RoleID = new SelectList(await _dbContext.Roles.ToListAsync(cancellationToken), "Id", "RoleName", user.Roles.Select(r => r.Id).FirstOrDefault());
            return View(_mapper.Map<EditUserViewModel>(user));
        }

        // POST: Admin/Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleID.ToString());

                CustomUser user = await _userManager.FindByIdAsync(id.ToString());
                await _userManager.SetUserNameAsync(user, model.UserName);
                user.IsDisabled = model.IsDisabled;
                user.BODID = model.BODID;
                await _userManager.UpdateAsync(user);

                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                if (!string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                return RedirectToAction("Index");
            }

            ViewBag.BODID = new SelectList(await _dbContext.BODs.ToListAsync(cancellationToken), "Id", "Code", model.BODID);
            ViewBag.RoleID = new SelectList(await _dbContext.Roles.ToListAsync(cancellationToken), "Id", "RoleName", model.RoleID);
            return View(model);
        }

        // GET: Admin/Logins/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
