using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebSiteAPI.Domain.Entities;

namespace WebSiteAPI.Infrastructure.Operations
{
    public class UrlHelperOperation
    {
        public static string NormalizeCategoryForUrl(string CategoryName)
        {
            string normalized = CategoryName.Replace(" ", "-");

            // Türkçe karakterleri uygun hale getir
            normalized = normalized.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Diğer özel karakterleri temizle
            normalized = Regex.Replace(sb.ToString(), "[^a-zA-Z0-9_-]", "");

            // Küçük harfe çevir
            normalized = normalized.ToLower();

            return normalized;
        }

        public static string RestoreCategoryFromUrl(string normalizedCategoryName)
        {
            // Burada, normalize edilmiş kategori adının orijinal halini döndürebilirsiniz.
            // Örneğin, "demir-sandalye" -> "Demir Sandalye" şeklinde bir işlem yapabilirsiniz.
            // Ancak tam olarak nasıl geri döndürmek istediğinize bağlı olarak bu kısmı özelleştirmeniz gerekebilir.
            // Bu örnek sadece bir fikir vermek amacıyla yazılmıştır.
            string[] parts = normalizedCategoryName.Split('-');
            StringBuilder restored = new StringBuilder();

            foreach (string part in parts)
            {
                restored.Append(char.ToUpper(part[0]) + part.Substring(1));
                restored.Append(" ");
            }

            return restored.ToString().Trim();
        }
    }
}
