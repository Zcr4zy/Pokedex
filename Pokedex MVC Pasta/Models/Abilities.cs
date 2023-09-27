using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    [Table("Abilities")]
    public class Abilities
    {
        [Key]
        public int Id {get; set;}

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome da Habilidade")]
        [StringLength(30, ErrorMessage = "O nome da Habilidade deve possuir no máximo 30 caracteres")]
        public string Name {get; set;}

        public ICollection<PokemonAbilities>PokemonsWithAbility {get; set;}
    }
}