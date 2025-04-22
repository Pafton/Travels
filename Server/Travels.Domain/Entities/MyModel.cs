using System;
using System.Collections.Generic;

namespace Travels.Domain.Entities
{
    public class MyModel
    {
        public int Id { get; set; }
        public string Kraj { get; set; } = string.Empty; 
        public string Miasto { get; set; } = string.Empty;  
        public string Nazwa { get; set; } = string.Empty; 
        public string Opis { get; set; } = string.Empty; 
        public string Kategoria { get; set; } = string.Empty;  
    }
}
