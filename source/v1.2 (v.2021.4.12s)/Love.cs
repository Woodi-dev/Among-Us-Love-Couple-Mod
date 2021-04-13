using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using InnerPlayerControl = PlayerControl;

namespace LoveCoupleMod
{
    public enum EndReason
    {
        LoveWin = 0,
        ImpostorWin =1,
        CrewmateWin =2,
        NoWin =-1,
        IgnoreEndReason = -2,


    }
    public class Love : PlayerComponent
    {
        public Player lover;
        public static Color color = new Color(1, 102 / 255.0f, 204 / 255.0f);
        public bool spawnedAsImpostor;
        public Love(Player player, Player lover) : base(player)
        {
            name = "Love";
            this.lover = lover;
            spawnedAsImpostor = parent.isImpostor();
        }
        public override void Update()
        {
          
        }

        public static EndReason CheckWin()
        {
            Player[] alive = PlayerController.getAlivePlayers();
            Player[] lovers = getLovers();
            if (lovers.Length != 2) return EndReason.IgnoreEndReason;
            if (!lovers[0].isAlive() | !lovers[1].isAlive()) return EndReason.IgnoreEndReason;

           int imposterAlive = 0;
            foreach(Player player in alive)
            {
                if (player.isImpostor()) imposterAlive++;
            }
            if (alive.Length == 4)
            {
                
                    if (lovers[0].isImpostor() | lovers[1].isImpostor())
                    {
                        return EndReason.NoWin;
                    }
                    else
                    {
                        if (imposterAlive >= 2)
                        {
                            return EndReason.ImpostorWin;
                        }else if (imposterAlive <= 1)
                        {
                            return EndReason.IgnoreEndReason;
                        }
                  
                    }

              
            }
            else if (alive.Length == 3)
            {        
                return EndReason.LoveWin;
            }
            else if (alive.Length == 2)
            {        
                return EndReason.LoveWin;
            }

            return EndReason.IgnoreEndReason;
        }

        public static Player[] getLovers()
        {
            List<Player> p = new List<Player>();
            foreach (Player player in PlayerController.players)
            {
                if (player.hasComponent("Love"))
                {
                    p.Add(player);
                }
            }
            return p.ToArray();

        }

    

    }
}
