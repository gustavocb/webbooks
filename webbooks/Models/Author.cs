using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace webbooks.Models
{
    [Table("author")]
    public class Author
    {
        [Required]
        [Key]
        [Display(Name = "Autor")]
        public int ID { get; set; }

        [Required]
        [MinLength(3, ErrorMessageResourceName = "Informe ao menos 3 caracteres")]
        [StringLength(60, ErrorMessageResourceName = "informe no máximo 60 caracteres")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        public virtual ICollection<Book> book { get; set; }
    }
}