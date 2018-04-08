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
    public partial class ScavengeMetalsRecipe : Recipe
    {
        public ScavengeMetalsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(1),
                new CraftingElement<CopperIngotItem>(7),
                new CraftingElement<IronIngotItem>(7),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TailingsLeachateItem>(typeof(MetallurgyEfficiencySkill), 100, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ScavengeMetalsRecipe), this.UILink(), 40, typeof(MetallurgySpeedSkill));
            this.Initialize("Scavenge Metals", typeof(ScavengeMetalsRecipe));

            CraftingComponent.AddRecipe(typeof(ElectrowinCellObject), this);
        }
    }
}
