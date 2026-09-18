using System.ComponentModel.DataAnnotations;

namespace company_backend.Domain.Entities
{
    [CompanyNameMatchesUrl(ErrorMessage = "Company Name must be relevant to the Website URL.")]
    public class Company
    {
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Company Name must be between 5 and 50 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Website URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL (e.g., https://example.com).")]
        public string WebSiteUrl { get; set; }
    }
}
