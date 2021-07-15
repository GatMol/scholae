using Newtonsoft.Json;
using Scholae.Services;
using Scholae.Validazione;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Scholae.ViewModels
{
    public class SellPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Utente utenteCorrente;
        public Libro Libro { get; set; }
        public Materia Materia { get; set; }
        public byte[] Img { get; set; }
        public string Filename { get; set; }

        public ValidatableObject<string> Nome { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Isbn { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Autore { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Edizione { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Editore { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Prezzo { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> NomeMateria { get; set; } = new ValidatableObject<string>();

        public SellPageViewModel()
        {
            AddValidationRules();
            Libro = new Libro();
            Materia = new Materia();
            Libro.Materia = Materia;
            utenteCorrente = Session.GetSession().UtenteCorrente;
        }

        public void AddValidationRules()
        {
            Nome.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Nome e' un campo obbligatorio" });
            NomeMateria.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Materia e' un campo obbligatorio" });
            Isbn.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Isbn e' un campo obbligatorio" });
            Isbn.Validations.Add(new IsValidNumberRule<string> { ValidationMessage = "Isbn e' un numero" });
            Isbn.Validations.Add(new IsLengthValidRule<string> { MinimunLenght = 13, MaximunLenght = 13, ValidationMessage = "Isbn e' un codice di 13 valori" });
            Autore.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Autore e' un campo obbligatorio" });
            Edizione.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Edizione non valida" });
            Edizione.Validations.Add(new IsValidNumberRule<string> { ValidationMessage = "Edizione e' un anno" });
            Editore.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Editore obbligatoria" });
            Prezzo.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Prezzo obbligatoria" });
            Prezzo.Validations.Add(new IsValidNumberRule<string> { ValidationMessage = "Prezzo e' un numero" });
        }

        public bool AreFieldValid()
        {
            bool isNomeValid = Nome.Validate();
            bool isNomeMateriaValid = NomeMateria.Validate();
            bool isIsbnValid = Isbn.Validate();
            bool isAutoreValid = Autore.Validate();
            bool isEdizioneValid = Edizione.Validate();
            bool isEditoreValid = Editore.Validate();
            bool isPrezzoValid = Prezzo.Validate();

            return isNomeValid && isNomeMateriaValid && isIsbnValid && isAutoreValid 
                && isEdizioneValid && isEditoreValid && isPrezzoValid;
        }

        public void GeneraLibero()
        {
            Libro.Nome = Nome.Value;
            Libro.Isbn = Isbn.Value;
            Libro.Autore = Autore.Value;
            Libro.Editore = Editore.Value;
            Libro.Edizione = Edizione.Value;
            Libro.Prezzo = int.Parse(Prezzo.Value);
            Materia.Nome = NomeMateria.Value;
        }

        public bool VendiLibro()
        {
            
            Debug.WriteLine("\nVendi libro in SellPVM, UTENTE: ");
            Debug.WriteLine(utenteCorrente.ToString());
            Debug.WriteLine("\n");
            Libro.Utente = utenteCorrente;
            Debug.WriteLine("\nSellPVM creo il libro nel DB: \n");
            Libro libro = APIConnector.CreaLibro(Libro);
            if (libro != null)
            {
                Debug.WriteLine($"\nSellPVM creato il libro nel DB\n");
                Debug.WriteLine($"\nSellPVM setto id del libro in memoria {libro.id}: \n");
                Libro.id = libro.id;
                Debug.WriteLine($"\nSellPVM libro NON NULL\n");
                Debug.WriteLine($"\nSellPVM aggiungoFotoLibro\n");
                var response = APIConnector.AggiungiFotoLibro(Libro.id, Img, Filename);
                if (response.IsSuccessful)
                {
                    Debug.WriteLine($"\nSellPVM aggiungoFotoLibro risposa con successo\n");
                    string pathImmagine = response.Content;
                    Debug.WriteLine($"\nSellPVM path immagine in memoria {pathImmagine}\n");
                    Libro.Immagine = pathImmagine;
                    Debug.WriteLine($"\nSellPVM aggiungo il libro in vendita alla lista nell'utente corrente\n");
                    utenteCorrente.AddLibroInVendita(Libro);
                    Debug.WriteLine($"\nSellPVM libriInVendita dell' UC: \n");
                    foreach (Libro l in utenteCorrente.LibriInVendita.Values)
                    {
                        Debug.WriteLine($"\n {l} \n");
                    }
                    return true;
                }
                Debug.WriteLine($"\nSellPVM cancello libro senza immagine\n");
                APIConnector.DeleteLibro(Libro.id);
                return false;
            }
            Debug.WriteLine($"\nSellPVM libro NULL\n");
            return false;
        }
        public List<string> GetMaterie()
        {
            List<Materia> materie = APIConnector.GetAllMaterie();
            return NomiMaterie(materie);
        }

        private List<string> NomiMaterie(List<Materia> materie)
        {
            List<string> nM = new List<string>();
            foreach (Materia m in materie)
            {
                nM.Add(m.Nome);
            }
            return nM;
        }
    }
}
