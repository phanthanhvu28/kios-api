using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.AuthenUser;
public class AuthenUserByUsernamePassSpec : SpecificationBase<Entities.AuthenUser>
{
    public AuthenUserByUsernamePassSpec(string userName, string password)
    {
        ApplyFilter(entity => entity.Username == userName);
        ApplyFilter(entity => entity.Password == password);
    }
}
