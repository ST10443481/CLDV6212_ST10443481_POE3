using System.ComponentModel.DataAnnotations;

namespace ABCRetailers.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Shipping Address")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string? ShippingAddress { get; set; }

        // NEW: Role selection
        [Required(ErrorMessage = "Please select an account type")]
        [Display(Name = "Account Type")]
        public string Role { get; set; } = "Customer";
    }
}