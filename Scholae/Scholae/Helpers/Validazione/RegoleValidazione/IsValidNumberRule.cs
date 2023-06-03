namespace Scholae.Validazione
{
    public class IsValidNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                long.Parse($"{value}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
