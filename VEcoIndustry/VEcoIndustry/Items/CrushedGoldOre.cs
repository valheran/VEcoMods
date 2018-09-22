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
    public partial class CrushedGoldOreRecipe : Recipe
    {
        public CrushedGoldOreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CrushedGoldOreItem>(6),
            
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.AdditiveStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CrushedGoldOreRecipe), Item.Get<CrushedGoldOreItem>().UILink(), 3, typeof(MetallurgySpeedSkill));
            this.Initialize("Crushed Gold Ore", typeof(CrushedGoldOreRecipe));

            CraftingComponent.AddRecipe(typeof(StampingBatteryObject), this);
        }
    }

    [RequiresSkill(typeof(MetallurgySkill), 4)]
    public partial class GrindGoldOreRecipe : Recipe
    {
        public GrindGoldOreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CrushedGoldOreItem>(8),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.AdditiveStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GrindGoldOreRecipe), Item.Get<CrushedGoldOreItem>().UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Crushed Gold Ore", typeof(GrindGoldOreRecipe));

            CraftingComponent.AddRecipe(typeof(GrindingMillObject), this);
        }
    }

    [Serialized]
    [Weight(30000)]
    public partial class CrushedGoldOreItem :
    Item
    {
        public override string FriendlyName { get { return "Crushed Gold Ore"; } }
        public override string Description { get { return "Gold Ore ground down for better processing efficiency"; } }

    }

}