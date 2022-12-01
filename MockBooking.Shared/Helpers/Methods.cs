using System.Security.Cryptography;

namespace MockBooking.Shared.Helpers
{
	public static class Methods
	{
		public static string Sha512Hash(string inputString)
		{
			var inputHashBytes = System.Text.Encoding.UTF8.GetBytes(inputString);
			using var shaM = SHA512.Create();
			var outputHashBytes = shaM.ComputeHash(inputHashBytes);
			var ret = BitConverter.ToString(outputHashBytes).Replace("-", "").ToLower();
			return ret;

		}
	}
}
