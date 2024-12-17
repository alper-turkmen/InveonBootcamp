namespace KutuphaneYonetim.ViewModels
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<RoleItem> Roles { get; set; }
    }

    public class RoleItem
    {
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}