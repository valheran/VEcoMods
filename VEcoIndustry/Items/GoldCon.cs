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
    public partial class GoldConRecipe : Recipe
    {
        public GoldConRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldConItem>(6),
                new CraftingElement<TailingsItem>(2),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedGoldOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.AdditiveStrategy),
                new CraftingElement<XanthateItem>(typeof(MetallurgyEfficiencySkill), 3, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GoldConRecipe), Item.Get<GoldConItem>().UILink(), 3, typeof(MetallurgySpeedSkill));
            this.Initialize("Gold Concentrate", typeof(GoldConRecipe));

            CraftingComponent.AddRecipe(typeof(FlotationCellObject), this);
        }
    }


    [Serialized]
    [Weight(15000)]
    [Currency]
    public partial class GoldConItem :
    Item
    {
        public override string FriendlyName { get { return "Gold Concentrate"; } }
        public override string Description { get { return "Concentrated Gold minerals for clean leaching"; } }

    }

}