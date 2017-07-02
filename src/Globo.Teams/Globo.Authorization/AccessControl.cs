namespace Globo.Authorization
{
	public class AccessControl
    {
		public static AccessControl Instance = new AccessControl();

		public bool HasAccess(string user, string role)
		{
			// TODO: FAZER CONTROLE DE ACESSO POR USUÁRIO
			return true;
		}
    }
}
