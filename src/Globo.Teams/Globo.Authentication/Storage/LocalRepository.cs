namespace Globo.Authentication.Storage
{
	using System.Linq;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class LocalRepository : IUserRespository
	{
		private Dictionary<string, User> data = new Dictionary<string, User>();

		public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
		{
			return data.Select(a => a.Value).ToList();
		}

		public async Task<User> GetAsync(string email, CancellationToken cancellationToken)
		{
			if (data.ContainsKey(email)) return data[email];
			return null;
		}

		public async Task SaveAsync(User user, CancellationToken cancellationToken)
		{
			data[user.Email] = user;
		}
	}
}
