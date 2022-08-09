namespace UserandRoles.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleMaping> RoleMapings { get; set; } = new List<RoleMaping>();
    }
}
