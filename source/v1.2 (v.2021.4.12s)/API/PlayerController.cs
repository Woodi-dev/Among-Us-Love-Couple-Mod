using System.Collections.Generic;



namespace LoveCoupleMod
{
    public class PlayerController
    {
        public static List<Player> players;
        public static Player LocalPlayer;
        public static void Update()
        {
            if (players != null)
            {
                foreach (Player player in players)
                {
                    if(player.playerdata!=null)
                    player.Update();
                }
            }
        }

        public static void InitPlayers()
        {
            players = new List<Player>();
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {

                Player p = new Player(player);
                if (player.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    LocalPlayer = p;
                }
                players.Add(p);
            }

        }

        public static Player getPlayerById(byte id)
        {
            if (players == null) return null;
            foreach (Player player in players)
            {
                if (player.PlayerId == id)
                {
                    return player;
                }
            }
            return null;
        }

        public static Player getPlayerByName(string name)
        {
            if (players == null) return null;
            foreach (Player player in players)
            {
                if (player.playerdata.name==name)
                {
                    return player;
                }
            }
            return null;
        }

        public static Player getLocalPlayer()
        {
            if (players == null) return null;
            foreach (Player player in players)
            {
                if (player.playerdata == PlayerControl.LocalPlayer)
                {
                    return player;
                }
            }
            return null;
        }
        public static Player[] getPlayersWithComponent(string name)
        {
            List<Player> p = new List<Player>();
            if(players==null)return p.ToArray();
            foreach (Player player in players)
            {
                foreach (PlayerComponent component in player.components)
                    {
                        if (component.name == name)
                        {
                            p.Add(player);
                        }
                    }
            }
            return p.ToArray();


        }

        public static Player[] getImpostors()
        {
            List<Player> p = new List<Player>();
            if (players == null) return p.ToArray();
            foreach (Player player in players)
            {
                if (player.playerdata != null)
                {
                    if (player.isImpostor()) p.Add(player);
                }
            }
            return p.ToArray();

        }

        public static Player[] getAlivePlayers()
        {

            List<Player> p = new List<Player>();
            if (players == null) return p.ToArray();
            foreach (Player player in players)
            {
                if (player.isAlive())
                {
                    p.Add(player);
                }
            }
            return p.ToArray();
        }

    }
    
}
