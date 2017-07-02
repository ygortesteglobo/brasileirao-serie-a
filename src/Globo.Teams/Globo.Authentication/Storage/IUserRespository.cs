namespace Globo.Authentication.Storage
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public interface IUserRespository
    {
		Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
		Task<User> GetAsync(string email, CancellationToken cancellationToken);
		Task SaveAsync(User user, CancellationToken cancellationToken);
    }
}
