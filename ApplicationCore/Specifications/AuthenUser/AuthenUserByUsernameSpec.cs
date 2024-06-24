using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.AuthenUser;
public class AuthenUserByUsernameSpec : SpecificationBase<Entities.AuthenUser>
{
    public AuthenUserByUsernameSpec(string userName, string password)
    {
        ApplyFilter(entity => entity.Username == userName);
        ApplyFilter(entity => entity.Password == password);
    }
}
