using System;
using System.Collections.Generic;

namespace Travels.Domain.Entities
{
    public class MyModel
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int YearBuilt { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public IReadOnlyList<string> Tags { get; set; }
    }
}
