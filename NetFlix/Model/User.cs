﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public class User
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        
        // sub fileds 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int isAdmin { get; set; }

        public User(string username, string hasedPassword, DateTime dob, string gender)
        {
            this.Username = username;
            this.Password = hasedPassword;
            this.Dob = dob;
            this.Gender = gender;
        }

        public User(int id, string username, int isAdmin) { 
            this.Id = id;
            this.Username = username;
            this.isAdmin = isAdmin;
        }

        public User(int id, string username)
        {
            this.Id = id;
            this.Username = username;
        }
    }
}