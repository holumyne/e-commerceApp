using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}

//    public class BaseController : ControllerBase
//    {

//        private readonly IConfiguration _configuration;
//        private readonly ILogger<BaseController> _logger;
//        public BaseController(ILogger<BaseController> logger, IConfiguration configuration)
//        {
//            _logger = logger;
//            _configuration = configuration;

//        }

//        [Serializable]
//        public class InvalidStudentNameException : Exception
//        {
//            public InvalidStudentNameException() { }

//            public InvalidStudentNameException(string name) : base(String.Format("Authorized token access: {0}", name))
//            {

//            }
//        }
//        protected string GetUser_IP()
//        {
//            StringValues ipAddresses = new StringValues();
//            string VisitorsIPAddr = string.Empty;
//            if (HttpContext.Request.Headers.Any(m => m.Key == "HTTP_X_FORWARDED_FOR"))
//            {
//                HttpContext.Request.Headers.TryGetValue("HTTP_X_FORWARDED_FOR", out ipAddresses);
//                VisitorsIPAddr = ipAddresses.FirstOrDefault();
//            }
//            else if (HttpContext.Request.Host.Host.Length != 0)
//            {
//                VisitorsIPAddr = HttpContext.Request.Host.Host;//HttpContext.Current.Request.UserHostAddress;
//            }
//            return VisitorsIPAddr;
//        }

//        protected T FromXml<T>(String xml)
//        {
//            T returnedXmlClass = default(T);

//            try
//            {
//                using (TextReader reader = new StringReader(xml))
//                {
//                    try
//                    {
//                        returnedXmlClass =
//                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
//                    }
//                    catch (InvalidOperationException)
//                    {
//                        // String passed is not XML, simply return defaultXmlClass
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Exception occured: {ex.Message}");
//            }

//            return returnedXmlClass;
//        }

//        /// <summary>
//        /// Authenticates the vendor requesting connection to the API
//        /// </summary>
//        /// <param name="userName" example="1007435">The membershipId of the user</param>
//        /// <param name="password" example="ToluOlusakin2020$#">The password of the user</param>
//        /// <returns>True/False</returns>
//        protected bool ValidateVendor(string userName, string password)
//        {
//            VendorRequest request = new VendorRequest
//            {
//                UserName = userName,
//                Password = password
//            };

//            ApiUser vendor = this._vendorRepository.ValidateVendor(request);
//            if (vendor != null && vendor.UserName != null)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        protected bool ValidateHashedVendor(string userName, string password)
//        {
//            var users = _apiRepository.GetUsers();
//            foreach (var user in users)
//            {
//                var encrpytedName = SecurityHelper.EncryptTripleDes(user.UserName, true, user.HashKey);
//                if (encrpytedName == userName)
//                {
//                    string vPassword = SecurityHelper.DecryptTripleDes(password, true, user.HashKey);
//                    VendorRequest request = new VendorRequest();
//                    request.UserName = SecurityHelper.DecryptTripleDes(userName, true, user.HashKey);
//                    request.Password = vPassword;
//                    ApiUser vendor = this._vendorRepository.ValidateVendor(request);
//                    if (vendor != null && vendor.UserName != null)
//                    {
//                        return true;
//                    }
//                    break;
//                }
//            }
//            return false;
//        }
//        protected string ToQueryString(object model)
//        {
//            Type t = model.GetType();
//            NameValueCollection nvc = new NameValueCollection();
//            foreach (var p in t.GetProperties())
//            {
//                var name = p.Name;
//                var value = p.GetValue(model, null);
//                if (value != null && value.ToString().Length > 0)
//                {
//                    nvc.Add(name, value.ToString());
//                }
//            }
//            var array = (from key in nvc.AllKeys
//                         from value in nvc.GetValues(key)
//                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();
//            return "?" + string.Join("&", array);
//        }

//    }

//}
