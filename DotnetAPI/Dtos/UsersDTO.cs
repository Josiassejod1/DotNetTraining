namespace DotnetAPI.DTOS
{
  public partial class UserDTO
  {
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Gender { get; set; } = "";
    public string UserId { get; set; } = "";
    public bool Active { get; set; }

  }
}
