using System;

namespace StroopTest.Models
{
    [Serializable]
    public class User {
        public long id;
        public string fullName;
        public string email;
        public string gender;
        public string age;
    }
}