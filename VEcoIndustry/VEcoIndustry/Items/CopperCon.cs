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
    public partial class CopperConRecipe : Recipe
    {
        public CopperConRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperConItem>(6),
                new CraftingElement<TailingsItem>(typeof(MetallurgyEfficiencySkill), 6, MetallurgyEfficiencySkill.MultiplicativeStrategy),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedCopperOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.AdditiveStrategy),
                new CraftingElement<XanthateItem>(typeof(MetallurgyEfficiencySkill), 3, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CopperConRecipe), Item.Get<CopperConItem>().UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Copper Concentrate", typeof(CopperConRecipe));

            CraftingComponent.AddRecipe(typeof(FlotationCellObject), this);
        }
    }


    [Serialized]
    [Weight(15000)]
    [Currency]
    public partial class CopperConItem :
    Item
    {
        public override string FriendlyName { get { return "Copper Concentrate"; } }
        public override string Description { get { return "Concentrated Copper minerals for clean smelting"; } }

    }

}