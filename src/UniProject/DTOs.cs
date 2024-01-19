using System.ComponentModel.DataAnnotations;
namespace DTOs
{
    public class RequestUserDto
    {
        public string Username {get; set;}
        public string UnhashedPassword {get; set;}
    }

    public class ResponseUserDto
    {
        public int UserId {get; set;}
        public string Username {get; set;}
    }

    public class RequestPostDto
    {
        public int UserId {get; set;}
        public string Content {get; set;}

    }

}

