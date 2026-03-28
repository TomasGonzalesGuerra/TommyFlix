namespace TommyFlix.Web.Models;

public class CastMember
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Character { get; set; }
    public string? Profile_Path { get; set; }
    public int Order { get; set; }
}