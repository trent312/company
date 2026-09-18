using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace company_backend.Domain.Entities
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CompanyNameMatchesUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not Company company)
            {
                return ValidationResult.Success;
            }

            if (string.IsNullOrWhiteSpace(company.CompanyName) || string.IsNullOrWhiteSpace(company.WebSiteUrl))
            {
                // Skip relevance check if fields are empty; [Required] handles these.
                return ValidationResult.Success;
            }

            if (!Uri.TryCreate(company.WebSiteUrl, UriKind.Absolute, out var uri))
            {
                return ValidationResult.Success; // [Url] handles invalid URL formats.
            }

            // 1. Normalize company name (remove special chars & corporate suffixes)
            string normalizedName = NormalizeText(company.CompanyName);

            // 2. Extract core domain (e.g., "https://www.acme-corp.com/about" -> "acmecorp")
            string host = uri.Host.StartsWith("www.", StringComparison.OrdinalIgnoreCase) ? uri.Host[4..] : uri.Host;
            string coreDomain = host.Split('.')[0].Replace("-", "").Replace("_", "").ToLowerInvariant();

            // 3. Relevance Logic: Domain contains name OR Name contains domain
            bool isDirectMatch = coreDomain.Contains(normalizedName) || normalizedName.Contains(coreDomain);

            if (isDirectMatch)
            {
                return ValidationResult.Success;
            }

            // 4. Word Token Match (for multi-word names like "Acme Global Solutions")
            var words = normalizedName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            bool wordMatch = words.Any(word => word.Length > 2 && coreDomain.Contains(word));

            if (wordMatch)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                ErrorMessage ?? "The Company Name does not appear to be relevant to the provided Website URL.",
                new[] { nameof(Company.CompanyName), nameof(Company.WebSiteUrl) }
            );
        }

        private static string NormalizeText(string input)
        {
            string text = input.ToLowerInvariant();
            // Strip common corporate terms
            text = Regex.Replace(text, @"\b(inc|llc|ltd|corp|corporation|group|co|gmbh|solutions|tech)\b", "", RegexOptions.IgnoreCase);
            // Strip special characters
            return Regex.Replace(text, @"[^a-z0-9\s]", "").Trim();
        }
    }
}
