using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
   [Table("PokemonAbilities")]
    public class PokemonAbilities
    {
        [Key, Column(Order = 1)]
        public int PokemonId {get; set;}
        [ForeignKey("PokemonId")]
        public Pokemons Pokemon {get; set;}

        [Key, Column(Order = 2)]
        public int AbilityId{get; set;}
        [ForeignKey("AbilityId")]
        public Abilities Ability {get; set;}
    }
}