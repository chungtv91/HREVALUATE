using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.EntityFrameworkCore;
using HRE.Web.UI.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HRE.Core.Identity;
using HRE.Core.Shared.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class RolesController : HrAdminController
    {
        private readonly HrDbContext _dbContext;
        private readonly CustomRoleManager _roleManager;
        private readonly IMapper _mapper;

        public RolesController(HrDbContext dbContext, CustomRoleManager roleManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // GET: Admin/Roles
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var roles = await _dbContext.Roles.ToListAsync(cancellationToken);
            return View(roles);
        }

        // GET: Admin/Roles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // GET: Admin/Roles/Create
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            cancellationToken.ThrowIfCancellationRequested();
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomRole role = new()
                {
                    Name = model.Name
                };
                await _roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EditRoleViewModel>(role));
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                role.Name = model.Name;
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
