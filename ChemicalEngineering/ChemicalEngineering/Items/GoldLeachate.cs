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

    [RequiresSkill(typeof(MetallurgySkill), 2)]
    public partial class GoldLeachateRecipe : Recipe
    {
        public GoldLeachateRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldLeachateItem>(8),
                new CraftingElement<StabilisedTailingsItem>(5),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldConItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.AdditiveStrategy),
                new CraftingElement<LeachingLiquorItem>(typeof(MetallurgyEfficiencySkill), 3, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GoldLeachateRecipe), Item.Get<GoldLeachateItem>().UILink(), 4, typeof(MetallurgySpeedSkill));
            this.Initialize("Gold Leachate", typeof(GoldLeachateRecipe));

            CraftingComponent.AddRecipe(typeof(LeachingVatObject), this);
        }
    }


    [Serialized]
    [Weight(10000)]
    [Currency]
    public partial class GoldLeachateItem :
    Item
    {
        public override string FriendlyName { get { return "Gold Leachate"; } }
        public override string Description { get { return "Solution pregnant with Gold"; } }

    }

}