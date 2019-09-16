using System;

namespace StroopTest.Models
{
    [Serializable]
    public class User {
        public long Id;
        public string fullname;
        public string email;
        public string gender;
        public string age;
    }
}