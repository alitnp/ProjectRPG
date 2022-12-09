using System.ComponentModel.DataAnnotations;

namespace ProjectRPG.Dtos.UserDtos
{
    public class AddUserDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
