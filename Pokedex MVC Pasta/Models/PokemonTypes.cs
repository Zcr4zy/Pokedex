using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models 
{
    [Table("PokemonTypes")]
    public class PokemonTypes
    {
        [Key, Column(Order = 1)]
        public int PokemonId {get; set;}
        [ForeignKey("PokemonId")]
        public Pokemons Pokemon {get; set;}

        [Key, Column(Order = 2)]
        public int TypeId{get; set;}
        [ForeignKey("TypeId")]
        public Types Type {get; set;}
    }
}