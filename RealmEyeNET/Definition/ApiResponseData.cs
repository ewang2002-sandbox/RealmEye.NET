using RealmEyeNET.Constants;

namespace RealmEyeNET.Definition
{
	public struct ApiResponseData
	{
		public ApiStatusCode Status { get; set; }
		public string ErrorTitle { get; set; }
		public string ErrorMessage { get; set; }
	}
}