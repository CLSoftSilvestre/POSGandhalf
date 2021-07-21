using System;
namespace POSClasses
{
    public class User : Person
    {
        // Class variable and properties
        public string UserLoginName { get; set; }
        public string UserPassword { get; set; }
        public DateTime UserLastLogin { get; set; }
        public bool UserActive { get; set; }
        public Role UserRole { get; set; } = Role.Operator;

        // Static variable and properties
        private static bool UserHasLogin;
        public static User CurrentUser;

        // Class Methods
        public User()
        {
        }

        
        public void SetRole(Role newRole)
        {
            UserRole = newRole;
        }

        // Static Methods
        // TODO: Provisorio até implementar o EntityFramework 
        public static bool Login(string Username, string Password)
        {
            // TODO: Necessário atualizar na base de dados a data do ultimo login...
            UserHasLogin = true;
            return true;
        }

        public static void Logout()
        {
            UserHasLogin = false;
        }

        public static bool UserHasLoginActive()
        {
            return UserHasLogin;
        }
    }

    public enum Role
    {
        Operator = 1,
        Supervisor = 2,
        Administrator = 3,
    }
}
