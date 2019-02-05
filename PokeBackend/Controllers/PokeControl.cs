using System;
using System.Collections.Generic;
using PokemonModel;
using PokeDBModel;

namespace PokeControlController
{
    public class PokeControl
    {
        private int RoundsToFight { get; set; }

        public PokeControl(int RoundsToFight = 8)
        {

            this.RoundsToFight = RoundsToFight;

        }

        /// <summary>
        /// Determines the winner of a pokemon battle
        /// </summary>
        /// <param name="PokemonAStr">Pokemon Figther A</param>
        /// <param name="PokemonBStr">Pokemon Figther B</param>
        /// <param name="pokeDB">The pokemon Database</param>
        /// <returns>The winner of the pokemon battle</returns>
        public string Battle(string PokemonAStr, string PokemonBStr, PokeDB pokeDB)
        {
            //This could be done by iterating each round through, runtime will be affected directly by amount of rounds
            //But could also be done with a few calculations and some conditions

            Pokemon PokemonA = pokeDB.SearchNameExplicit(PokemonAStr);
            Pokemon PokemonB = pokeDB.SearchNameExplicit(PokemonBStr);

            if (PokemonA == null && PokemonB == null)
            {
                return "No Contest";
            }

            if (PokemonA == null)
            {
                return PokemonB.Name + " Wins - Enemy No Show";
            }

            if (PokemonB == null)
            {
                return PokemonA.Name + " Wins - Enemy No Show";
            }

            int RoundsToWinPokemonA = RoundsToWinBattle(PokemonA.Attack, PokemonB.HP, PokemonB.Defense);
            int RoundsToWinPokemonB = RoundsToWinBattle(PokemonB.Attack, PokemonA.HP, PokemonA.Defense);

            //Nobody can win in defined rounds
            if (RoundsToWinPokemonA > RoundsToFight && RoundsToWinPokemonB > RoundsToFight)
            {
                //Business decided the winner is highest HP
                int EnemyHPPokemonA = EnemyHPAfterNRounds(PokemonA.Attack, PokemonB.HP, PokemonB.Defense, RoundsToFight);
                int EnemyHPPokemonB = EnemyHPAfterNRounds(PokemonB.Attack, PokemonA.HP, PokemonA.Defense, RoundsToFight);

                if (EnemyHPPokemonA < EnemyHPPokemonB)
                {
                    return PokemonA.Name + " Wins - Enemy HP " + EnemyHPPokemonA + " < " + EnemyHPPokemonB;
                }
                else if (EnemyHPPokemonB < EnemyHPPokemonA)
                {
                    return PokemonB.Name + " Wins - Enemy HP " + EnemyHPPokemonB + " < " + EnemyHPPokemonA;
                }
                else
                {
                    //Business must decide
                    return "Tie after " + RoundsToFight + " rounds";
                }

            }


            if (RoundsToWinPokemonA < RoundsToWinPokemonB)
            {
                return PokemonA.Name + " Wins - Round " + RoundsToWinPokemonA;
            }
            else if (RoundsToWinPokemonB < RoundsToWinPokemonA)
            {
                return PokemonB.Name + " Wins - Round " + RoundsToWinPokemonB;

            }
            else
            {
                //Fastest pokemon wins
                if (PokemonA.Speed > PokemonB.Speed)
                {
                    return PokemonA.Name + " Wins - Speed " + PokemonA.Speed + ">" + PokemonB.Speed;
                }
                else if (PokemonB.Speed > PokemonA.Speed)
                {
                    return PokemonB.Name + " Wins - Speed " + PokemonB.Speed + ">" + PokemonA.Speed;
                }
                else
                {
                    //Business must decide
                    return "Tie - Speed is same";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Attack">Attacking pokemons Attack Points</param>
        /// <param name="EnemyHP">Enemy Health Points</param>
        /// <param name="EnemyDefense">Enemy Defense Points</param>
        /// <returns>Amount of rounds to win the battle</returns>
        private int RoundsToWinBattle(int Attack, int EnemyHP, int EnemyDefense)
        {

            int dmg = DamageDealt(Attack, EnemyDefense);

            //Special case with no damage
            if (dmg == 0)
            {
                //Return more than max rounds
                //This could also be done by raising and handling an exception
                return RoundsToFight + 1;
            }

            //We have to be careful here, we must round to ceiling 
            return (int)Math.Ceiling((float)EnemyHP / dmg);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Attack">Attacking pokemons Attack Points</param>
        /// <param name="EnemyHP">Enemy Health Points</param>
        /// <param name="EnemyDefense">Enemy Defense Points</param>
        /// <param name="n">Amount of rounds to fight</param>
        /// <returns>The enemy hp after n rounds</returns>
        /// <remarks>
        /// Do note result can be negative
        /// </remarks>
        private int EnemyHPAfterNRounds(int Attack, int EnemyHP, int EnemyDefense, int n)
        {
            int dmg = DamageDealt(Attack, EnemyDefense);

            return EnemyHP - (n * dmg);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Attack">Attacking pokemons Attack Points</param>
        /// <param name="EnemyDefense">Enemy Defense Points</param>
        /// <returns>Damage per rount dealt</returns>
        private int DamageDealt(int Attack, int EnemyDefense)
        {
            int DamageDealt = (Attack - EnemyDefense);
            if (DamageDealt < 0)
            {
                DamageDealt = 0;
            }

            return DamageDealt;

        }

    }


}
