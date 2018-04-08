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

    [RequiresSkill(typeof(MetallurgySkill), 3)]
    public partial class ModernSmeltCopperRecipe : Recipe
    {
        public ModernSmeltCopperRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(6),
                new CraftingElement<SlagItem>(4),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperConItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(MetallurgyEfficiencySkill), 8, MetallurgyEfficiencySkill.MultiplicativeStrategy),

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ModernSmeltCopperRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Modern Smelt Copper", typeof(ModernSmeltCopperRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [RequiresSkill(typeof(MetallurgySkill), 2)]
    public partial class ImprovedSmeltCopperRecipe : Recipe
    {
        public ImprovedSmeltCopperRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(3),
                new CraftingElement<TailingsItem>(5),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedCopperOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltCopperRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Improved Smelt Copper", typeof(ImprovedSmeltCopperRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
    [RequiresSkill(typeof(MetallurgySkill), 2)]
    public partial class ImprovedSmeltIronRecipe : Recipe
    {
        public ImprovedSmeltIronRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(3),
                new CraftingElement<SlagItem>(4),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedIronOreItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(MetallurgyEfficiencySkill), 8, MetallurgyEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltIronRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Improved Smelt Iron", typeof(ImprovedSmeltIronRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}
