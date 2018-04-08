namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(MetallurgySkill), 1)]
    public partial class CrushedIronOreRecipe : Recipe
    {
        public CrushedIronOreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CrushedIronOreItem>(),
            
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronOreItem>(typeof(MetallurgyEfficiencySkill), 1, MetallurgyEfficiencySkill.AdditiveStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CrushedIronOreRecipe), Item.Get<CrushedIronOreItem>().UILink(), 3, typeof(MetallurgySpeedSkill));
            this.Initialize("Crushed Iron Ore", typeof(CrushedIronOreRecipe));

            CraftingComponent.AddRecipe(typeof(GrindingMillObject), this);
        }
    }


    [Serialized]
    [Weight(30000)]
    public partial class CrushedIronOreItem :
    Item
    {
        public override string FriendlyName { get { return "Crushed Iron Ore"; } }
        public override string Description { get { return "Iron Ore ground down for better processing efficiency"; } }

    }

}