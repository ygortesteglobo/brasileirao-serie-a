namespace Globo.Authentication
{
	using Globo.Authentication.Storage;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class UserManager
    {
		private IUserRespository repository = null;

		private static UserManager instance = null;
		public static UserManager Instance
		{
			get
			{
				if (instance == null)
					throw new Exception("You must inject the storage dependency");

				return instance;
			}
			set
			{
				instance = value;
			}
		}

		public UserManager(IUserRespository repository)
		{
			this.repository = repository ?? throw new ArgumentException("Cannot init UserManager with null repository.", "repository");
		}

		public async Task<User> GetAsync(string email, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			User user = await repository.GetAsync(email, cancellationToken);

			if (user == null) return null;
			user.IsPersisted = true;

			return user;
		}

		public async Task SaveAsync(User user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			user.Validate();
			if(!user.IsPersisted)
				user.Password = Cryptography.GenerateHash(user.Password);

			await repository.SaveAsync(user, cancellationToken);
		}

		public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await repository.GetAllAsync(cancellationToken);
		}
	}
}
