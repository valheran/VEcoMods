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

    [RequiresSkill(typeof(MetallurgySkill), 4)]
    public partial class RefineGoldRecipe : Recipe
    {
        public RefineGoldRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(5),
                

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldLeachateItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RefineGoldRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Refine Gold", typeof(RefineGoldRecipe));

            CraftingComponent.AddRecipe(typeof(ElectrowinCellObject), this);
        }
    }
}
