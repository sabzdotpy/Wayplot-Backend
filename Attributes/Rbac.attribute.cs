namespace Wayplot_Backend.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class RequiredRoleAttribute : Attribute
    {
        public string Role { get; }
        public RequiredRoleAttribute(string role) => Role = role;
    }
}
