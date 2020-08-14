using System;
using RealmEyeNET.Constants;
using RealmEyeNET.Definition;

namespace RealmEyeNET.Error
{
	public class ApiResponse
	{
		public ApiStatusCode StatusCode { get; }

		public ApiResponse(ApiStatusCode statusCode) => StatusCode = statusCode;

		/// <summary>
		/// Gets the specifics on a status code.
		/// </summary>
		/// <returns>An object containing the specific error title and message.</returns>
		public ApiResponseData GetMessage()
		{
			var e = StatusCode switch
			{
				ApiStatusCode.PrivateProfile => new ApiResponseData
				{
					ErrorMessage =
						"The user's profile is either private or the specified name wasn't found on RealmEye.",
					ErrorTitle = "Private Profile or No Profile Found",
					Status = StatusCode
				},
				ApiStatusCode.ConnectionError => new ApiResponseData
				{
					ErrorMessage = "An error occurred when connecting to the person's RealmEye profile.",
					ErrorTitle = "Connection Error",
					Status = StatusCode
				},
				ApiStatusCode.PrivateCharacters => new ApiResponseData
				{
					ErrorMessage = "The user's characters are private.",
					ErrorTitle = "Private Characters",
					Status = StatusCode
				},
				ApiStatusCode.PrivateGuildHistory => new ApiResponseData
				{
					ErrorMessage = "The user's guild history is private.",
					ErrorTitle = "Private Guild History",
					Status = StatusCode
				},
				ApiStatusCode.PrivateNameHistory => new ApiResponseData
				{
					ErrorMessage = "The user's name history is private.",
					ErrorTitle = "Private Name History",
					Status = StatusCode
				},
				ApiStatusCode.PrivatePets => new ApiResponseData
				{
					ErrorMessage = "The user's pets are private.",
					ErrorTitle = "Private Pets",
					Status = StatusCode
				},
				ApiStatusCode.PrivateRankHistory => new ApiResponseData
				{
					ErrorMessage = "The user's rank history is private.",
					ErrorTitle = "Private Rank History",
					Status = StatusCode
				},
				// should never hit
				ApiStatusCode.Success => new ApiResponseData
				{
					ErrorMessage = "The connection was successful.",
					ErrorTitle = "Request Successful",
					Status = StatusCode
				},
				ApiStatusCode.UnspecifiedError => new ApiResponseData
				{
					ErrorMessage = "An unspecified error occurred. This may be due to a parsing error.",
					ErrorTitle = "Unspecified Error",
					Status = StatusCode
				},
				_ => throw new ArgumentOutOfRangeException()
			};
			e.Status = StatusCode;
			return e;
		}
	}
}