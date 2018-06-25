using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webbooks.Models
{
    [Table("book")]
    public class Book
    {
        [Required]
        [Key]
        [Display(Name = "Livro")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public Int64 isbn { get; set; }

        [Required]
        [MinLength(3, ErrorMessageResourceName = "Informe ao menos 3 caracteres")]
        [StringLength(60, ErrorMessageResourceName = "Informe no maximo 60 caracteres")]
        [Display(Name = "Título")]
        public string titulo { get; set; }

        [Display(Name = "Autor")]
        public int? autor { get; set; }

        [Display(Name = "Ano")]
        public int ano { get; set; }

        [Display(Name = "Sinopse")]
        [StringLength(2500, ErrorMessageResourceName = "Informe no maximo 2500 caracteres")]
        public string sinopse { get; set; }

        [Display(Name = "Autor")]
        public virtual Author autor_fk { get; set; }
    }
}