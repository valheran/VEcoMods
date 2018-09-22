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
                new CraftingElement<CopperIngotItem>(5),
                new CraftingElement<SlagItem>(typeof(BasicSmeltingEfficiencySkill), 6, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperConItem>(typeof(BasicSmeltingEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 8, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ModernSmeltCopperRecipe), this.UILink(), 1, typeof(BasicSmeltingSpeedSkill));
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
                new CraftingElement<CopperIngotItem>(2),
                new CraftingElement<TailingsItem>(typeof(BasicSmeltingEfficiencySkill), 3, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SlagItem>(typeof(BasicSmeltingEfficiencySkill), 5, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedCopperOreItem>(typeof(MetallurgyEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 7, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltCopperRecipe), this.UILink(), 1, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Improved Smelt Copper", typeof(ImprovedSmeltCopperRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
    [RequiresSkill(typeof(BasicSmeltingSkill), 4)]
    public partial class ImprovedSmeltGoldRecipe : Recipe
    {
        public ImprovedSmeltGoldRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(2),
                new CraftingElement<TailingsItem>(typeof(BasicSmeltingEfficiencySkill), 3, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SlagItem>(typeof(BasicSmeltingEfficiencySkill), 5, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedGoldOreItem>(typeof(MetallurgyEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 7, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltGoldRecipe), this.UILink(), 1, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Improved Smelt Gold", typeof(ImprovedSmeltGoldRecipe));

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
                new CraftingElement<IronIngotItem>(4),
                new CraftingElement<SlagItem>(typeof(BasicSmeltingEfficiencySkill), 7, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CrushedIronOreItem>(typeof(BasicSmeltingEfficiencySkill), 10, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 6, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ImprovedSmeltIronRecipe), this.UILink(), 0.5f, typeof(BasicSmeltingSpeedSkill));
            this.Initialize("Improved Smelt Iron", typeof(ImprovedSmeltIronRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}
