using Slalom.Stacks.Communication.Validation;

namespace Authentication.SecurityRules.Communication.Rules
{
    public class user_is_administrator : InRoleSecurityRule<TestCommand>
    {
        public user_is_administrator()
            : base("Administrator", "You must be an administrator to perform this action.") 
        {
        }
    }
}