namespace PlatformService.Models
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the call response is correct, the specific data for each method is found in the 'data' property.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        /// <example>true</example>
        public bool Success { get; set; }
        /// <summary>
        /// The code result.
        /// </summary>
        /// <value>
        /// The code result.
        /// </value>
        /// <example>200</example>
        public String? CodeResult { get; set; }
        /// <summary>
        /// The message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        /// <example>Success, and there is a response body.</example>
        public String? Message { get; set; }
        /// <summary>
        /// Contains the response data for each method.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T? Data { get; set; }
    }
}
