using System.ComponentModel.DataAnnotations;
namespace Controllers;

public class RequestUserDto
{
    [Required]
    public string Username {get; set;}  = string.Empty;
    [Required]
    public string UnhashedPassword {get; set;}  = string.Empty;
}

public class ResponseUserDto
{
    public int UserId {get; set;}
    public string Username {get; set;}
}

public class RequestPostDto
{
    [Required]
    public int UserId {get; set;}
    [Required]
    public string Content {get; set;}  = string.Empty;

}
