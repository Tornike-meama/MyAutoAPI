using System.Collections.Generic;

namespace MyAutoAPI1.DTO.Identity.GetAlluser
{
    public class GetuserById : IGetUserById
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public List<string> UserRoles { get; set; }

        public GetuserById(string id, string userName, string email, bool emailConfirmed, string phoneNumber, bool phoneNumberConfirmed, List<string> userRoles)
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            UserRoles = userRoles;
        }
    }
}