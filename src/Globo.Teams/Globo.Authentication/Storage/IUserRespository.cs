namespace Globo.Authentication.Storage
{
	using System.Threading;
	using System.Threading.Tasks;

	public interface IUserRespository
    {
		Task<User> GetAsync(string email, CancellationToken cancellationToken);
		Task SaveAsync(User user, CancellationToken cancellationToken);
    }
}
