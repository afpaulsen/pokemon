using System;
using System.Collections.Generic;
using PokemonModel;
using System.IO;
using System.Linq;

namespace PokeDBModel
{
    public class PokeDB
    {
        private List<Pokemon> Pokemons { get; set; }
        public string Headers { get; set; }
        private List<Pokemon> allLegendary { get; set; }

        public PokeDB(String Path)
        {
            Pokemons = ReadPokemon(Path);
        }


        private List<Pokemon> ReadPokemon(string Path)
        {
            using (var reader = new StreamReader(Path))
            {
                //Save the header
                var headerVal = reader.ReadLine().Split(',').ToList();
                headerVal.RemoveRange(0, 4);
                headerVal.RemoveAt(headerVal.Count - 1);
                Headers = String.Join(", ", headerVal);

                List<Pokemon> list = new List<Pokemon>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    try
                    {
                        //This does not look very pretty, but parsing never is
                        list.Add(new Pokemon(Int32.Parse(values[0]), values[1], values[2], values[3], Int32.Parse(values[4]), Int32.Parse(values[5]), Int32.Parse(values[6]), Int32.Parse(values[7]), Int32.Parse(values[8]), Int32.Parse(values[9]), Int32.Parse(values[10]), Int32.Parse(values[11]), bool.Parse(values[12])));

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                return list;
            }
        }

        public List<Pokemon> SearchType(string needle)
        {
            List<Pokemon> foundType1 = new List<Pokemon>();
            List<Pokemon> foundType2 = new List<Pokemon>();

            if (!String.IsNullOrEmpty(needle))
            {
                foundType1 = Pokemons.FindAll(s => s.Type1.Contains(needle));
                foundType2 = Pokemons.FindAll(s => s.Type1.Contains(needle));
            }

            //Assuming a pokemon cannot have same type twice, otherwise union should be used
            foundType1.AddRange(foundType2);

            return foundType1;
        }

        public List<Pokemon> ListMultiType()
        {
            //Could also utilise caching
            //TODO: Datastructure for pokemons could be improved by equating empty string and null
            return Pokemons.FindAll(s => !s.Type2.Equals(""));
        }

        public List<Pokemon> ListAllLegendary()
        {
            //Utilise caching
            if (allLegendary == null)
            {
                allLegendary = Pokemons.FindAll(s => s.Legendary);
            }

            return allLegendary;
        }

        public List<Pokemon> SearchName(string needle)
        {

            //TODO: This is case-sensitive, this might not be what is needed.
            return Pokemons.FindAll(s => s.Name.Contains(needle));
            //return Pokemons.FindAll(s => s.Name.ToLower().Contains(needle.ToLower()));

            //This could be improved using both prefix and suffix search
            //We have already discussed this :-)
        }

        public Pokemon SearchNameExplicit(string needle)
        {
            //Find returns the first element if found
            return Pokemons.Find(s => s.Name.Equals(needle));
        }

        public List<Pokemon> SearcHeader(string header, int value)
        {
            //This could be done less verbose using Dictionary<string, int> in the datastructure for pokemons
            switch (header)
            {
                case "Total":
                    {
                        return Pokemons.FindAll(s => s.Total >= value);
                    }
                case "HP":
                    {
                        return Pokemons.FindAll(s => s.HP >= value);
                    }
                case "Attack":
                    {
                        return Pokemons.FindAll(s => s.Attack >= value);
                    }
                case "Defense":
                    {
                        return Pokemons.FindAll(s => s.Defense >= value);
                    }
                case "SpAtk":
                    {
                        return Pokemons.FindAll(s => s.SpAtk >= value);
                    }
                case "SpDef":
                    {
                        return Pokemons.FindAll(s => s.SpDef >= value);
                    }
                case "Speed":
                    {
                        return Pokemons.FindAll(s => s.Speed >= value);
                    }
                case "Generation":
                    {
                        return Pokemons.FindAll(s => s.Generation >= value);
                    }

                default:
                    {
                        throw new InvalidOperationException("Header not found");
                    }
            }
        }
    }
}
