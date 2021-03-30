using System; 

namespace dqt.domain.Encoding
{
    public class ParameterEncoder 
    {
        public static string Base64StringDecode(string param)
        {
            var bytes = Convert.FromBase64String(param);
            var decodedString = System.Text.Encoding.UTF8.GetString(bytes);
            return decodedString;
        }
        public static string Base64StringEncode(string originalString)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(originalString);
            var encodedString = Convert.ToBase64String(bytes);
            return encodedString;
        }
    }
}
