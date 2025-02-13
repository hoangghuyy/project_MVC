using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace project_mvc.Helpers
{
    public static class Utility
    {
        public static string CreatePasswordHash(string password, string saltkey)
        {
            byte[] salt = Convert.FromBase64String(saltkey);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string ConvertRewrite(string unicode)
        {
            try
            {
                if (!string.IsNullOrEmpty(unicode))
                {
                    unicode = NewUnicodeToAscii(unicode);
                    unicode = unicode.ToLower().Trim();
                    unicode = Regex.Replace(unicode, @"\s+", " ");
                    unicode = Regex.Replace(unicode, "[\\s]", "-");
                    //Unicode = UnicodeToAscii(Unicode);
                    unicode = Regex.Replace(unicode, @"-+", "-");
                    unicode = unicode.Replace("®", "");
                    unicode = unicode.Replace("™", "");
                }
            }
            catch
            {
            }
            return unicode;
        }

        /// <summary>
        /// create by BienLV 02-04-2014
        /// remove special character 
        /// </summary>
        /// <param name="unicode"></param>
        /// <returns></returns>
        public static string NewUnicodeToAscii(string unicode)
        {
            try
            {
                unicode = Regex.Replace(unicode, "[á|à|ả|ã|ạ|â|ă|ấ|ầ|ẩ|ẫ|ậ|ắ|ằ|ẳ|ẵ|ặ]", "a", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", "e", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", "u", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[í|ì|ỉ|ĩ|ị]", "i", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[ó|ò|ỏ|õ|ọ|ô|ơ|ố|ồ|ổ|ỗ|ộ|ớ|ờ|ở|ỡ|ợ]", "o", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[đ|Đ]", "d", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ]", "y", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[,|~|@|/|.|:|?|#|$|%|&|*|(|)|+|”|“|'|\"|!|`|–]", "", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[áàảãạâăấầẩẫậắằẳẵặ]", "a", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[éèẻẽẹêếềểễệ]", "e", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[úùủũụưứừửữự]", "u", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[íìỉĩị]", "i", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[óòỏõọôơốồổỗộớờởỡợ]", "o", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[đ]", "d", RegexOptions.IgnoreCase); // Note: Đ is already included in the above regex
                unicode = Regex.Replace(unicode, "[ýỳỷỹỵ]", "y", RegexOptions.IgnoreCase);
                unicode = Regex.Replace(unicode, "[,~@/.:?#$%&*()”“'\"!`–]", "", RegexOptions.IgnoreCase);

            }
            catch { }
            return unicode;
        }

        public static List<string> FolderToListString(string array)
        {
            List<string> lst = new();
            try
            {
                if (!string.IsNullOrEmpty(array))
                {
                    array = array.Trim('/');
                    lst = array.Split('/').ToList();
                }
            }
            catch { }
            return lst.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public static string RemoveHTMLTag(string source) => !string.IsNullOrEmpty(source) ? Regex.Replace(source, "<.*?>", "") : string.Empty;

        public static string ValidString(string unicode, string code, bool removeHtml = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(unicode))
                {
                    switch (code)
                    {
                        case "301":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\w^\-:/.=?@]";
                                unicode = Regex.Replace(unicode, rex, "");
                                unicode = unicode.Replace("--", "-");
                                break;
                            }
                        case "link":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\w^\-:/.=?@#&]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                        case "arrayint":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\d,]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                        case "arraycode":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\w,]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                        case "code":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[\W+]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                        case "title":
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\w\s^\-_()|/.,!&?%\[]]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                        default:
                            {
                                if (removeHtml == true)
                                {
                                    unicode = RemoveHTML(unicode);
                                }
                                string rex = @"[^\w\s]";
                                unicode = Regex.Replace(unicode, rex, "");
                                break;
                            }
                    }
                }
            }
            catch { }
            return unicode;
        }
        public static string RemoveHTML(string source) => string.IsNullOrEmpty(source) == false ? Regex.Replace(source, "<.*?>", "") : string.Empty;
        public static string CreateSaltKey(int size)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
		public static string FormatNumber(object val, int comma = 0)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			string format = "N" + comma;
			string result = string.Format("{0:" + format + "}", val);
			return result;
		}
		public static string ActionText(string action)
		{
			return action switch
			{
				"Edit" => "Sửa",
				_ => "Thêm mới",
			};
		}
		public static string CharacterSpecail(string key)
		{
			try
			{
				if (!string.IsNullOrEmpty(key))
				{
					//key = key.Replace("_", "~_");
					key = key.Replace("'", "~''");
					key = key.Replace("%", "~%");
					key = key.Replace("~", "~~");
				}
			}
			catch
			{

			}
			return key;
		}
	}
}
