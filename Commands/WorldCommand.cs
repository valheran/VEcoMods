
// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods
{
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Players;
    using Eco.WorldGenerator;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Shared.Math;
    using Eco.Simulation.WorldLayers;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Items;
    using System.Linq;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Garbage;
    using Eco.Mods.TechTree;

    public class WorldCommands : IChatCommandHandler
    {
        [ChatCommand("world composition", ChatAuthorizationLevel.Admin)]
        public static void Mycom(User user)
        {
            ChatManager.SendChat("my command works", user);
        }
        [ChatCommand("world composition", ChatAuthorizationLevel.Admin)]
        public static void WorldComp(User user)
        {
            int iron = 0;
            int gold = 0;
            int copper = 0;
            int total = 0;
            WorldSettings settings = new WorldSettings();
            ChatManager.SendChat("init ints", user);

            var x = settings.VoxelSize.x;
            var y = settings.VoxelSize.y;
            var z = settings.Height;
                {
                    ChatManager.SendChat("got settings", user);
                int u = 0;
                while (u < x)
                {
                    int v = 0;
                    while (v<y)
                    {
                        int w = 0;
                        while (w < z)
                        {
                            Vector3i pos =new Vector3i(u, v, w);
                            Block block = World.GetBlock(pos);
                            string name = block.ToString().Split('.').Last();
                                if (name == "IronOreBlock")
                            {
                                iron++;
                            }
                            if (name == "GoldOreBlock")
                            {
                                gold++;
                            }
                            if (name == "CopperOreBlock")
                            {
                                copper++;
                            }
                            else
                            {
                                total++;
                            }
                            w++;
                        }
                        v++;
                    }
                    u++;
                }
                    
                    
                }
                string message = "iron" + iron + " gold " + gold + " copper " + copper + "total" + total;
                ChatManager.SendChat(message, user);
        }



    }



}
