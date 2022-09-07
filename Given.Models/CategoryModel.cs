using Given.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Given.Models
{     
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }      
}
