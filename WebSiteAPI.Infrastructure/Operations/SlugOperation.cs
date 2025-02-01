using NuGet.Protocol.Core.Types;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using WebSiteAPI.Application.Repositories;

namespace WebSiteAPI.Infrastructure.Operations
{
    public class SlugOperation
    {
        private readonly IProductReadRepository _repository;

        public SlugOperation(IProductReadRepository repository)
        {
            _repository = repository;
        }

        public string GetUniqueSlugWithIncrement(string originalSlug)
        {
            string newSlug = originalSlug;
            int counter = 1;

            while (_repository.GetAll().Any(a => a.Slug == newSlug))
            {
                // Eğer aynı isimde bir ürün varsa, yeni bir slug oluştur
                newSlug = $"{originalSlug}-{counter}";
                counter++;
            }

            return newSlug;
        }

        public string ProductNameToNormalizeUniqueSlug(string ProductName)
        {
            string normalizedSlug = ProductName.Replace(" ", "-");
            normalizedSlug = normalizedSlug.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var item in normalizedSlug)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(item) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(item);
                }
            }
            normalizedSlug = Regex.Replace(sb.ToString(), "[^a-zA-Z0-9_-]", "");
            normalizedSlug = GetUniqueSlugWithIncrement(normalizedSlug);

            return normalizedSlug;
        }
    }

}
