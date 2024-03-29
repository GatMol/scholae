﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Scholae.Validazione
{
    interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }

        bool Validate();

        bool IsValid { get; set; }
    }
}
