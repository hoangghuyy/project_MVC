using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Globalization;
using System.Net;
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
        public static string DisplayShowName(string? Name, bool show) => show == false ? "<del>" + Name + "</del>" : "<span>" + Name + "</span>";
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

		public static List<string> StringToListString(string array)
		{
			List<string> lst = new();
			try
			{
				if (!string.IsNullOrEmpty(array))
				{
					array = array.Trim(',');
					lst = array.Split(',').ToList();
				}
			}
			catch { }
			return lst;
		}

        public static string TrimLength(string input, int maxLength)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > maxLength)
                {
                    maxLength -= "...".Length;
                    maxLength = input.Length < maxLength ? input.Length : maxLength;
                    bool isLastSpace = input[maxLength] == ' ';
                    string part = input.Substring(0, maxLength);
                    if (isLastSpace)
                        return part + "...";
                    int lastSpaceIndexBeforeMax = part.LastIndexOf(' ');
                    if (lastSpaceIndexBeforeMax == -1)
                        return part + "...";
                    return string.Concat(input.AsSpan(0, lastSpaceIndexBeforeMax), "...");
                }
            }
            return input;
        }

		public static string GetFormatPriceType(decimal? value, Int16? type, string content, bool? showprice = true)
		{
			switch (type)
			{
				case 1:
					if (value.HasValue)
					{
						if (value.Value == 0)
							return content;
						else if (showprice.HasValue && showprice == false)
							return content;
						else
							return String.Format("{0:0,0}", value).Replace(".", ",") + "đ";
					}
					else
						return content;
				case 2:
					return "$" + value;
				case 3:
					return value.HasValue ? value.Value == 0 ? content : string.Format("{0:0,0}", value).Replace(".", ",") : content;
				case 4:
					return value.HasValue
						? value.Value switch
						{
							0 => content,
							_ => showprice.HasValue && showprice == false ? content : String.Format("{0:0,0}", value).Replace(".", ","),
						}
						: content;
				case 5:
					if (value.HasValue)
					{
						if (value.Value == 0)
							return "Miễn phí";
						else if (showprice.HasValue && showprice == false)
							return content;
						else
							return String.Format("{0:0,0}", value).Replace(".", ",") + "đ";
					}
					else
						return content;
				default: return string.Empty;
			}
		}

		public static string Link(string moduleAscii, string nameAscii, string redirect)
		{
			if (!string.IsNullOrEmpty(redirect))
			{
				return redirect;
			}
			if (!string.IsNullOrEmpty(moduleAscii) && !string.IsNullOrEmpty(nameAscii))
			{
				return "/" + moduleAscii + "/" + nameAscii;
			}
			if (!string.IsNullOrEmpty(moduleAscii))
			{
				moduleAscii = moduleAscii.TrimEnd('/');
			}
			if (!string.IsNullOrEmpty(nameAscii))
			{
				nameAscii = nameAscii.TrimEnd('/');
			}
			if (!string.IsNullOrEmpty(nameAscii))
			{
				string moduleSub = nameAscii[..1];
				if (moduleSub == "/")
				{
					return nameAscii;
				}
				return "/" + nameAscii;
			}
			if (!string.IsNullOrEmpty(moduleAscii))
			{
				string moduleSub = moduleAscii[..1];
				if (moduleSub == "/")
				{
					return moduleAscii;
				}
				return "/" + moduleAscii;
			}
			return string.Empty;
		}
	}
}
