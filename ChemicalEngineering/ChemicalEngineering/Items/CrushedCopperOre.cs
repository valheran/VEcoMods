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
    public partial class CrushedCopperOreRecipe : Recipe
    {
        public CrushedCopperOreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CrushedCopperOreItem>(),
            
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperOreItem>(typeof(MetallurgyEfficiencySkill), 1, MetallurgyEfficiencySkill.AdditiveStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CrushedCopperOreRecipe), Item.Get<CrushedCopperOreItem>().UILink(), 3, typeof(MetallurgySpeedSkill));
            this.Initialize("Crushed Copper Ore", typeof(CrushedCopperOreRecipe));

            CraftingComponent.AddRecipe(typeof(GrindingMillObject), this);
        }
    }


    [Serialized]
    [Weight(30000)]
    [Currency]
    public partial class CrushedCopperOreItem :
    Item
    {
        public override string FriendlyName { get { return "Crushed Copper Ore"; } }
        public override string Description { get { return "Copper Ore ground down for better processing efficiency."; } }

    }

}