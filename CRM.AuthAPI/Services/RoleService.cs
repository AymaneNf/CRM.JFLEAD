using System.Linq;
using System.Threading.Tasks;
using CRM.AuthAPI.Data;
using CRM.AuthAPI.Models;
using Microsoft.EntityFrameworkCore;  // Assurez-vous que cet espace de noms est présent

namespace CRM.AuthAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId); // Utilisation de FirstOrDefaultAsync ici
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
