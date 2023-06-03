using System;
namespace Scholae.ViewModels
{
    public class Materia
    {
        public Materia()
        {
        }

        public String Nome
        {
            get;
            set;

        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
