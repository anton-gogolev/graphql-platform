using HotChocolate.Types.Relay;

namespace HotChocolate.Fusion.Shared.Accounts;

[GraphQLName("Query")]
public class AccountQuery
{
    public IEnumerable<User> GetUsers([Service] UserRepository repository) =>
        repository.GetUsers();

    [NodeResolver]
    public User? GetUserById(int id, [Service] UserRepository repository) =>
        repository.GetUser(id);

    public IEnumerable<User> GetUsersById(
        [ID(nameof(User))] IEnumerable<int> ids,
        [Service] UserRepository repository)
    {
        foreach (var id in ids)
        {
            var user = repository.GetUser(id);

            if (user is not null)
            {
                yield return user;
            }
        }
    }
}
