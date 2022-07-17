using System.Collections.Generic;

namespace MyAutoAPI1.DTO.Identity.GetAlluser
{
    public interface IGetUserById
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
