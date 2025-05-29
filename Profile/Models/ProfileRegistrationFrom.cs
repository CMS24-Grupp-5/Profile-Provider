using System.ComponentModel.DataAnnotations;

public class ProfileRegistrationFrom
{
    [MinLength(2, ErrorMessage = "Förnamnet måste vara minst 2 tecken långt.")]
    [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
    public string FirstName { get; set; } = null!;

    [MinLength(2, ErrorMessage = "Efternamnet måste vara minst 2 tecken långt.")]
    [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
    public string LastName { get; set; } = null!;

    [MinLength(7, ErrorMessage = "Telefonnumret måste vara minst 7 siffror.")]
    [Required(ErrorMessage = "Telefonnummer är obligatoriskt.")]
    public string PhoneNumber { get; set; } = null!;

    [MinLength(10, ErrorMessage = "Användar-ID får inte vara tomt.")]
    [Required(ErrorMessage = "Användar-ID är obligatoriskt.")]
    public string UserId { get; set; } = null!;
}
