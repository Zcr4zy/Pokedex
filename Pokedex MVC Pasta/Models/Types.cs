using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    [Table("Types")]
    public class Types
    {
        [Key]
        public int Id{get; set;}

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome do Tipo")]
        [StringLength(30,ErrorMessage="O nome do tipo deve possuir no máximo 30 caracteres")]
        public string Name { get;set; }

        public ICollection<PokemonTypes> PokemonsOfThisType {get; set;}
        public ICollection<Weaknesses> PokemonsWithThisWeakness {get; set;}
    }
}