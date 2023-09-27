using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Abilities> Abilities { get; set; }
        public DbSet<Pokemons> Pokemons { get; set; }
        public DbSet<PokemonAbilities> PokemonsAbilities { get; set; }
        public DbSet<PokemonTypes> PokemonsTypes { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Weaknesses> Weaknesses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Many to Many - PokemonAbilities
            builder.Entity<PokemonAbilities>().HasKey(
                pa => new { pa.PokemonId, pa.AbilityId }
            );
            builder.Entity<PokemonAbilities>()
            .HasOne(pa => pa.Pokemon)
            .WithMany(p => p.Abilities)
            .HasForeignKey(pa => pa.PokemonId);

            builder.Entity<PokemonAbilities>()
            .HasOne(pa => pa.Ability)
            .WithMany(a => a.PokemonsWithAbility)
            .HasForeignKey(pa => pa.AbilityId);
            #endregion

            #region Many to Many - PokemonTypes
            builder.Entity<PokemonTypes>().HasKey(
                pt => new { pt.PokemonId, pt.TypeId }
            );
            builder.Entity<PokemonTypes>()
            .HasOne(pa => pa.Pokemon)
            .WithMany(p => p.Types)
            .HasForeignKey(pa => pa.PokemonId);

            builder.Entity<PokemonTypes>()
            .HasOne(pa => pa.Type)
            .WithMany(a => a.PokemonsOfThisType)
            .HasForeignKey(pa => pa.TypeId);
            #endregion

            #region Many to Many - Weaknesses
            builder.Entity<Weaknesses>().HasKey(
                w => new { w.PokemonId, w.TypeId }
            );
            builder.Entity<Weaknesses>()
            .HasOne(w => w.Pokemon)
            .WithMany(p => p.Weaknesses)
            .HasForeignKey(w => w.PokemonId);

            builder.Entity<Weaknesses>()
            .HasOne(w => w.Type)
            .WithMany(a => a.PokemonsWithThisWeakness)
            .HasForeignKey(w => w.TypeId);
            #endregion

            #region Config Identity
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRole");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaim");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "UserToken");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogin");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaim");
            });
            #endregion
        
            #region Populate Roles
            var roles = new List<IdentityRole>(){
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                },
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Moderador",
                    NormalizedName = "MODERADOR"
                },
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    NormalizedName = "USUARIO"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            #endregion

            #region Populate Users
            var hash = new PasswordHasher<User>();
            byte[] avatarPic = System.IO.File.ReadAllBytes(
                System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\img\avatar.png");
            var users = new List<User>(){
                new User{
                    Id= Guid.NewGuid().ToString(),
                    Name = "Carlos Alexandre Alves Cunha",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@pokedex.com",
                    NormalizedEmail = "ADMIN@POKEDEX.COM",
                    EmailConfirmed = true,
                    PasswordHash = hash.HashPassword(null, "123456"),
                    SecurityStamp = hash.GetHashCode().ToString(),
                    ProfilePicture = avatarPic,
                    BirthDate = DateTime.Parse("03/11/2004")
                }
            };
            builder.Entity<User>().HasData(users);
            #endregion

            #region Populate User Role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[1].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[2].Id
                }
            );
            #endregion

            #region Populate Types
            List<Types> types = new List<Types>(){
                new Types {
                    Id = 1,
                    Name = "Normal"
                },
                new Types {
                    Id = 2,
                    Name = "Fogo"
                },
                new Types {
                    Id = 3,
                    Name = "Água"
                },
                new Types {
                    Id = 4,
                    Name = "Elétrico"
                },
                 new Types {
                    Id = 5,
                    Name = "Planta"
                },
                 new Types {
                    Id = 6,
                    Name = "Gelo"
                },
                 new Types {
                    Id = 7,
                    Name = "Lutador"
                },
                 new Types {
                    Id = 8,
                    Name = "Venenoso"
                },
                 new Types {
                    Id = 9,
                    Name = "Terra"
                },
                 new Types {
                    Id = 10,
                    Name = "Voador"
                },
                 new Types {
                    Id = 11,
                    Name = "Psíquico"
                },
                 new Types {
                    Id = 12,
                    Name = "Inseto"
                },
                 new Types {
                    Id = 13,
                    Name = "Pedra"
                },
                 new Types {
                    Id = 14,
                    Name = "Fantasma"
                },
                 new Types {
                    Id = 15,
                    Name = "Dragão"
                },
                 new Types {
                    Id = 16,
                    Name = "Noturno"
                },
                 new Types {
                    Id = 17,
                    Name = "Metálico"
                },
                 new Types {
                    Id = 18,
                    Name = "Fada"
                }
              
            };
            builder.Entity<Types>().HasData(types);
            #endregion

            #region Populate Pokemons
            List<Pokemons> pokemons = new List<Pokemons>(){
                new Pokemons(){
                    Id = 1,
                    Name = "Bulbasaur",
                    EvolvedFromId= null,
                    Description = "Bulbasaur pode ser visto cochilando sob luz solar intensa. Há uma semente em suas costas. Ao absorver a luz do sol, sua semente cresce progressivamente.",
                    Height = 0.7,
                    Weight = 6.9,
                    Gender = Gender.Both,
                    Image = "img/pokemons/001.png"
                },

                 new Pokemons(){
                    Id = 2,
                    Name = "Ivysaur",
                    EvolvedFromId= 1,
                    Description = "Há um broto nas costas desse pokemon. Para suportar seu peso, as pernas e o tronco de Ivysaur ficam grossos e fortes. Se começar a passar mais tempo deitado sob a luz do sol, é um sinal de que o broto florescerá em uma flor grande em breve.",
                    Height = 1,
                    Weight = 13,
                    Gender = Gender.Both,
                    Image = "img/pokemons/002.png"
                 },

                 new Pokemons(){
                    Id = 3,
                    Name = "Venusaur",
                    EvolvedFromId= 2,
                    Description = "Há uma flor grande nas costas do Venusaur. Diz-se que a flor adquire cores vivas se receber muita nutrição e luz solar. O aroma da flor acalma as emoções das pessoas.",
                    Height = 2,
                    Weight = 100,
                    Gender = Gender.Both,
                    Image = "img/pokemons/003.png"
                 },

                  new Pokemons(){
                    Id = 4,
                    Name = "Charmander",
                    EvolvedFromId= null,
                    Description = "A chama que queima na ponta da cauda é uma indicação de suas emoções. A chama tremula quando Charmander está se divertindo. Se o pokemon se enfurecer, a chama queima ferozmente.",
                    Height = 0.6,
                    Weight = 8.5,
                    Gender = Gender.Both,
                    Image = "img/pokemons/004.png"
                 },                          

                 new Pokemons(){
                    Id = 5,
                    Name = "Charmeleon",
                    EvolvedFromId= 4,
                    Description = "Charmeleon impiedosamente destrói seus inimigos usando suas garras afiadas. Se encontrar um inimigo forte, ele se torna agressivo. Nesse estado excitado, a chama na ponta de sua cauda brilha com uma cor branca azulada.",
                    Height = 1.1,
                    Weight = 19,
                    Gender = Gender.Both,
                    Image = "img/pokemons/005.png"
                 },

                 new Pokemons(){
                    Id = 6,
                    Name = "Charizard",
                    EvolvedFromId= 5,
                    Description = "Charizard voa pelo céu em buca de oponentes poderosos. Ele respira um calor tão grande que derrete qualquer coisa. No entanto, nunca dá um sopro ardente a qualquer oponente mais fraco que ele.",
                    Height = 1.7,
                    Weight = 90.5,
                    Gender = Gender.Both,
                    Image = "img/pokemons/006.png"
                 },

                  new Pokemons(){
                    Id = 7,
                    Name = "Squirtle",
                    EvolvedFromId= null,
                    Description = "A concha de Squirtle não é apenas usada para proteção. A forma arredondada da concha e as ranhuras na superfície ajudam a minimizar a resistência na água, permitindo que este pokemon nade em alta velocidade.",
                    Height = 0.5,
                    Weight = 9,
                    Gender = Gender.Both,
                    Image = "img/pokemons/007.png"
                 },

                  new Pokemons(){
                    Id = 8,
                    Name = "Wartortle",
                    EvolvedFromId= 7,
                    Description = "Sua cauda é grande e coberta com um pêlo rico e grosso. A cauda torna-se cada vez mais profunda na cor à medida que Wartotle envelhece. Os arranhões em sua concha são uma evidência da dureza deste Pokémon como um lutador.",
                    Height = 1,
                    Weight = 22.5,
                    Gender = Gender.Both,
                    Image = "img/pokemons/008.png"
                 },

                  new Pokemons(){
                    Id = 9,
                    Name = "Blastoise",
                    EvolvedFromId= 8,
                    Description = "Blastoise tem bicos de água que se projetam de sua concha. Os bicos de água são muito precisos. Eles podem disparar balas de água com precisão suficiente para atingir latas vazias a uma distância de mais de 60 metros.",
                    Height = 1.6,
                    Weight = 85.5,
                    Gender = Gender.Both,
                    Image = "img/pokemons/009.png"
                 },

                  new Pokemons(){
                    Id = 10,
                    Name = "Caterpie",
                    EvolvedFromId= null,
                    Description = "Caterpie tem um apetite voraz. Ele pode devorar folhas maiores que seu corpo diante dos seus olhos. De sua antena, este pokémon libera um odor terrivelmente forte.",
                    Height = 0.3,
                    Weight = 2.9,
                    Gender = Gender.Both,
                    Image = "img/pokemons/010.png"
                 },

                  new Pokemons(){
                    Id = 11,
                    Name = "Metapod",
                    EvolvedFromId= 10,
                    Description = "A concha que cobre o corpo deste Pokémon é tão dura quanto uma laje de ferro. Metapod não se move muito. Ele fica parado porque está preparando suas entranhas suaves para a evolução dentro da casca dura",
                    Height = 0.7,
                    Weight = 9.9,
                    Gender = Gender.Both,
                    Image = "img/pokemons/011.png"
                 },

                  new Pokemons(){
                    Id = 12,
                    Name = "Butterfree",
                    EvolvedFromId= 11,
                    Description = "Butterfree tem uma capacidade superior para procurar mel delicioso de flores. Pode até procurar, extrair e transportar mel de flores que estão desabrochando a mais de 10 quilômetros do ninho",
                    Height = 1.1,
                    Weight = 32,
                    Gender = Gender.Both,
                    Image = "img/pokemons/12.png"
                 },
            };
            builder.Entity<Pokemons>().HasData(pokemons);
            #endregion
            
            #region Populate PokemonTypes
            List<PokemonTypes> pokeTypes = new List<PokemonTypes>(){
                new PokemonTypes(){
                    PokemonId = 1,
                    TypeId = 5
                },

                new PokemonTypes(){
                    PokemonId = 1,
                    TypeId = 8
                },

                new PokemonTypes(){
                    PokemonId = 2,
                    TypeId = 5
                },

                 new PokemonTypes(){
                    PokemonId = 2,
                    TypeId = 8
                },

                 new PokemonTypes(){
                    PokemonId = 3,
                    TypeId = 5
                },

                 new PokemonTypes(){
                    PokemonId = 3,
                    TypeId = 8
                },

                 new PokemonTypes(){
                    PokemonId = 4,
                    TypeId = 2
                },

                 new PokemonTypes(){
                    PokemonId = 5,
                    TypeId = 2
                },

                 new PokemonTypes(){
                    PokemonId = 6,
                    TypeId = 2
                },

                 new PokemonTypes(){
                    PokemonId = 6,
                    TypeId = 10
                },

                 new PokemonTypes(){
                    PokemonId = 7,
                    TypeId = 3
                },

                 new PokemonTypes(){
                    PokemonId = 8,
                    TypeId = 3
                },

                 new PokemonTypes(){
                    PokemonId = 9,
                    TypeId = 3
                },

                 new PokemonTypes(){
                    PokemonId = 10,
                    TypeId = 12
                },
                 new PokemonTypes(){
                    PokemonId = 11,
                    TypeId = 12
                },

                 new PokemonTypes(){
                    PokemonId = 12,
                    TypeId = 12
                },

                 new PokemonTypes(){
                    PokemonId = 12,
                    TypeId = 10
                }
            };
            builder.Entity<PokemonTypes>().HasData(pokeTypes);
            #endregion
        
        }

    }
}
