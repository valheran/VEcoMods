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

    [RequiresSkill(typeof(BasicSmeltingSkill), 4)]
    public partial class ModernSmeltCopperRecipe : Recipe
    {
        public ModernSmeltCopperRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(4),
                new CraftingElement<SlagItem>(4),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperConItem>(typeof(BasicSmeltingEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 8, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ModernSmeltCopperRecipe), this.UILink(), 2, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Modern Smelt Copper", typeof(ModernSmeltCopperRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [RequiresSkill(typeof(BasicSmeltingSkill), 4)]
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
                new CraftingElement<CrushedCopperOreItem>(typeof(MetallurgyEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltCopperRecipe), this.UILink(), 2, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Improved Smelt Copper", typeof(ImprovedSmeltCopperRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
    [RequiresSkill(typeof(BasicSmeltingSkill), 3)]
    public partial class ImprovedSmeltIronRecipe : Recipe
    {
        public ImprovedSmeltIronRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(3),
                new CraftingElement<SlagItem>(5),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedIronOreItem>(typeof(BasicSmeltingEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 8, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltIronRecipe), this.UILink(), 2, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Improved Smelt Iron", typeof(ImprovedSmeltIronRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}
