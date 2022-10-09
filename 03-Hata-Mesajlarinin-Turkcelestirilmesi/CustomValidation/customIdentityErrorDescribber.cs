using Microsoft.AspNetCore.Identity;

namespace rentid.CustomValidation
{
    public class customIdentityErrorDescribber:IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError(){
                Code="InvalidUserName",
                Description=$"Bu {userName} email adresi geçersizdir!"
            };
        }
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError(){
                Code="InvalidEmail",
                Description=$"Bu {email} email adresi geçersizdir!"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError(){
                Code="DuplicateEmail",
                Description=$"Bu {email} email adresi kullanılmaktadır"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError(){
                Code="DuplicateUserName",
                Description=$"Bu {userName} email adresi kullanılmaktadır"
            };
        }

        
    }
}