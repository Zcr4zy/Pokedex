using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    [Table("Pokemons")]
    public class Pokemons
    {
        [Key]
        public int Id { get; set;}

        [Display(Name = "Evoluído de")]
        public int? EvolvedFromId {get; set;}
        [ForeignKey("EvolvedFromId")]
        public Pokemons PokemonBase {get; set;}

        [Display(Name = "Nome" )]
        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(30, ErrorMessage = "O nome deve possuir no máximo 30 caracteres")]
        public string Name {get; set;}

       [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a Descrição")]
        [StringLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres")]
        public string Description {get; set;}

        [Display(Name = "Altura" )]
        [Column(TypeName = "float(4,2)")]
        [Required(ErrorMessage = "Informe a Altura")]
        public double Height {get; set;}

        [Display(Name = "Peso" )]
        [Column(TypeName = "float(6,3)")]
        [Required(ErrorMessage = "Informe o Peso")]
        public double Weight {get; set;}

        [Display(Name = "Genêro")]
        [Required(ErrorMessage = "Selecione a opção de Genêro")]
        public Gender Gender {get; set;}

        [Display(Name = "Imagem")]
        [StringLength(200)]
        public string Image {get; set;}

        public ICollection<PokemonAbilities> Abilities {get; set;}

        public ICollection<PokemonTypes> Types{get; set;}

        public ICollection<Weaknesses> Weaknesses {get; set;}
    }
}