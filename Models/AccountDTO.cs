using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CSharpAssignment.Models
{
    public class AccountDTO
    {
        
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Fullname { get; set; }
        public string Role { get; set; }
        public int StatusId { get; set; }
        
        public string Email { get; set; }

        public string Picture { get; set; }

        public AccountDTO(int accountID, string fullname, int statusId, string email, string picture)
        {
            AccountID = accountID;
            Fullname = fullname;
            StatusId = statusId;
            Email = email;
            Picture = picture;
        }

        public AccountDTO(int iD, string username, string fullname, int statusId, string email)
        {
            AccountID = iD;
            Username = username;
            Fullname = fullname;
            StatusId = statusId;
            Email = email;
        }
        public AccountDTO(string fullname, string email, string picture)
        {
            Fullname = fullname;
            Email = email;
            Picture = picture;
            StatusId = 1;
        }
    }
}