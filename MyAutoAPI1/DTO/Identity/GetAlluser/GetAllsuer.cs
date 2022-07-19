﻿namespace MyAutoAPI1.DTO.Identity.GetAlluser
{
    public class GetAlluser : IGetAlluser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public GetAlluser(string id, string userName, string email, bool emailConfirmed, string phoneNumber, bool phoneNumberConfirmed)
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
        }
    }
}