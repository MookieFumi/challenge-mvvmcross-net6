using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Challenge.Services.Marvel
{
    public class MarvelAuthenticatorService : IMarvelAuthenticatorService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly string _privateKey;
        private readonly string _publickey;

        public MarvelAuthenticatorService(IDateTimeService dateTimeService, string privateKey, string publickey)
        {
            _dateTimeService = dateTimeService;
            _privateKey = privateKey;
            _publickey = publickey;
        }

        public string GetAuthInfo()
        {
            var ts = UnixTimeNow().ToString(CultureInfo.InvariantCulture);

            var hashInput = ts + _privateKey + _publickey;
            var hash = CalculateMd5Hash(hashInput).ToLower();

            return $"apikey={_publickey}&hash={hash}&ts={ts}";
        }

        private static string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        private long UnixTimeNow()
        {
            var timeSpan = (_dateTimeService.UtcNow() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long) timeSpan.TotalSeconds;
        }
    }
}