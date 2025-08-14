namespace Core.Api.Models;
public class Menu
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDisable { get; set; } = false;
    public string Icon { get; set; } = string.Empty;
    public bool HasSubMenu { get; set; } = false;
    public ICollection<Menu> SubMenus { get; set; } = [];
    public string Route { get; set; } = string.Empty;
    public string Breadcrumb { get; set; } = string.Empty;
}
