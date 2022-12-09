using ProjectRPG.Models;

namespace ProjectRPG.Dtos.UserDtos
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public List<Character> Characters { get; set; }
    }
}
