using System;
using Xunit;
using PokeDBModel;
using PokemonModel;
using PokeControlController;
using System.Collections.Generic;

namespace PokeBackend.Tests
{
    public class Tests : IDisposable
    {
        private PokeDB pokeDB;
        private PokeControl pokeCtrl;
        private PokeControl pokeCtrlManyRounds;

        public Tests()
        {
            pokeDB = new PokeDB(@"..\..\..\..\PokeBackend\Data\pokemons.csv");
            pokeCtrl = new PokeControl(8);
            pokeCtrlManyRounds = new PokeControl(1000);
        }

        [Fact]
        public void SearchTypeGrass_ReturnsN()
        {
            List<Pokemon> Pokemons = pokeDB.SearchType("Grass");

            Assert.Equal(140, Pokemons.Count);
        }

        [Fact]
        public void ListAllLegendaryContainsArticuno()
        {
            List<Pokemon> AllLegendary = pokeDB.ListAllLegendary();
            Pokemon Articuno = pokeDB.SearchNameExplicit("Articuno");

            Assert.Contains(Articuno, AllLegendary);
        }

        [Fact]
        public void ListAllLegendaryDoesNotContainPikachu()
        {
            List<Pokemon> AllLegendary = pokeDB.ListAllLegendary();
            Pokemon Pikachu = pokeDB.SearchNameExplicit("Pikachu");

            Assert.DoesNotContain(Pikachu, AllLegendary);
        }


        // Tests can be extended here

        //BATTLE SYSTEM
        [Fact]
        public void PikachuWinsByNoShow()
        {
            string winner = pokeCtrl.Battle("Pikachu", "MissingNo", pokeDB);

            Assert.Equal("Pikachu Wins - Enemy No Show", winner);
        }

        [Fact]
        public void NobodyWinsOnNoShow()
        {
            string winner = pokeCtrl.Battle("MissingNo", "Not Found", pokeDB);

            Assert.Equal("No Contest", winner);
        }

        [Fact]
        public void CharizardWinsOverPikachu()
        {
            string winner = pokeCtrl.Battle("Charizard", "Pikachu", pokeDB);

            Assert.Equal("Charizard Wins - Round 1", winner);
        }

        [Fact]
        public void CharizardCannotWinAgainstHimself()
        {
            string winner = pokeCtrl.Battle("Charizard", "Charizard", pokeDB);

            //Tie, nobody lost all HP
            Assert.Equal("Tie after 8 rounds", winner);
        }

        [Fact]
        public void CharizardCannotWinAgainstHimselfNotEvenInLongFigths()
        {
            string winner = pokeCtrlManyRounds.Battle("Charizard", "Charizard", pokeDB);

            //Tie, both loses all HP in same round - tie on speed
            Assert.Equal("Tie - Speed is same", winner);
        }


        public void Dispose()
        {
            pokeDB = null;
            pokeCtrl = null;
        }
    }
}
