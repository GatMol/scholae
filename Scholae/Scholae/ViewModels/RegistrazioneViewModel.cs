using RestSharp;
using Scholae.Services;
using Scholae.Validazione;
using System.ComponentModel;
using System.Diagnostics;

namespace Scholae.ViewModels
{
    public class RegistrazioneViewModel : INotifyPropertyChanged
    {
        public ValidatableObject<string> Nome { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Cognome { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatablePair<string> Password { get; set; } = new ValidatablePair<string>();
        public ValidatableObject<string> Telefono { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Nazionalita { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Citta { get; set; } = new ValidatableObject<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public RegistrazioneViewModel()
        {
            AddValidationRules();
        }

        public void AddValidationRules()
        {
            Nome.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Nome e' un campo obbligatorio" });
            Cognome.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Cognome e' un campo obbligatorio" });
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email e' un campo obbligatorio" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Email non valida" });
            Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password obbligatoria" });
            Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password obbligatoria" });
            Password.Validations.Add(new MatchPairValidationRule<string> { ValidationMessage = "Le password devono coincidere" });
            Telefono.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Telefono obbligatorio" });
            Telefono.Validations.Add(new IsValidNumberRule<string> { ValidationMessage = "Numero non valido" });
            Telefono.Validations.Add(new IsLengthValidRule<string> { MinimunLenght = 10, MaximunLenght = 10, ValidationMessage = "10 numeri necessari" });
            Nazionalita.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Nazionalita e' un campo obbligatorio" });
            Citta.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Citta e' un campo obbligatorio" });
        }

        public IRestResponse SignUp()
        {
            if (AreFieldValid())
            {
                string nome = Nome.Value;
                string cognome = Cognome.Value;
                string email = Email.Value;
                string password = Password.Item1.Value;
                long telefono = long.Parse(Telefono.Value);
                string nazionalita = Nazionalita.Value;
                string citta = Citta.Value;
                Utente u = new Utente(nome, cognome, email, password, telefono, nazionalita, citta);
                Debug.WriteLine(u.ToString());
                return APIConnector.Signup(u);
            }
            return null;
        }


        public bool AreFieldValid()
        {
            bool isNomeValid = Nome.Validate();
            bool isCognomeValid = Cognome.Validate();
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();
            bool isTelefonoValid = Telefono.Validate();
            bool isNazionalitaValid = Nazionalita.Validate();
            bool isCittaValid = Citta.Validate();

            return isNomeValid && isCognomeValid && isEmailValid && isPasswordValid && isTelefonoValid && isNazionalitaValid && isCittaValid;

        }
    }
}