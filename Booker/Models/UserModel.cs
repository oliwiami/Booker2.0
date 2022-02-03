using System.ComponentModel.DataAnnotations;

namespace Booker.Models
{
    public class UserModel
    {
        public int UsedrId { get; set; }

        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
