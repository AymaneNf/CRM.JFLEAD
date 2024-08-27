namespace CRM.AuthAPI.Services
{
    public interface IRoleService
    {
        Task AddPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
