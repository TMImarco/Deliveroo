using System.ComponentModel.DataAnnotations;

namespace Deliveroo.Models;

public class RegistrazioneViewModel
{
	[Required(ErrorMessage = "Il nome è obbligatorio")]
	public string Nome { get; set; }

	[Required(ErrorMessage = "Il telefono è obbligatorio")]
	public string Telefono { get; set; }

	[Required(ErrorMessage = "L'indirizzo è obbligatorio")]
	public string Indirizzo { get; set; }

	[Required(ErrorMessage = "Lo username è obbligatorio")]
	public string Username { get; set; }

	[Required(ErrorMessage = "La password è obbligatoria")]
	[MinLength(6, ErrorMessage = "La password deve avere almeno 6 caratteri")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Conferma la password")]
	[Compare("Password", ErrorMessage = "Le password non coincidono")]
	public string ConfermaPassword { get; set; }
}