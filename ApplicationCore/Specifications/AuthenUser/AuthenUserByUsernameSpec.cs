using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.AuthenUser;
public class AuthenUserByUsernameSpec : SpecificationBase<Entities.AuthenUser>
{
    public AuthenUserByUsernameSpec(string userName)
    {
        ApplyFilter(entity => entity.Username == userName);
    }
}
