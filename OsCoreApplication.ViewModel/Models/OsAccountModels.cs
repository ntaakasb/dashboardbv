using System;

namespace OsCoreApplication.ViewModel.Models
{
    [Serializable]
    public class OsAccountModels
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsCms { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedUser { get; set; }
    }
}
