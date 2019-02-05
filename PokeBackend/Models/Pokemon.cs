using System.Runtime.Serialization;

namespace PokemonModel
{

    [DataContract]
    public class Pokemon
    {
        [DataMember]
        public int No { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type1 { get; set; }

        [DataMember]
        public string Type2 { get; set; }

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int Attack { get; set; }

        [DataMember]
        public int Defense { get; set; }

        [DataMember]
        public int SpAtk { get; set; }

        [DataMember]
        public int SpDef { get; set; }

        [DataMember]
        public int Speed { get; set; }

        [DataMember]
        public int Generation { get; set; }

        [DataMember]
        public bool Legendary { get; set; }

        public Pokemon(int No, string Name, string Type1, string Type2, int Total, int HP, int Attack, int Defense, int SpAtk, int spDef, int Speed, int Generation, bool Legendary)
        {
            this.No = No;
            this.Name = Name;
            this.Type1 = Type1;
            this.Type2 = Type2;
            this.Total = Total;
            this.HP = HP;
            this.Attack = Attack;
            this.Defense = Defense;
            this.SpAtk = SpAtk;
            this.SpDef = spDef;
            this.Speed = Speed;
            this.Generation = Generation;
            this.Legendary = Legendary;
        }
    }
}
