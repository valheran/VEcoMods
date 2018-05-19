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
    public partial class MillSandRecipe : Recipe
    {
        public MillSandRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SandItem>(1),
                


            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MetallurgyEfficiencySkill), 3, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                

            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(MillSandRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Mill Sand", typeof(MillSandRecipe));

            CraftingComponent.AddRecipe(typeof(GrindingMillObject), this);
        }
    }

    [RequiresSkill(typeof(MetallurgySkill), 1)]
    public partial class CrushSandRecipe : Recipe
    {
        public CrushSandRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SandItem>(1),



            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MetallurgyEfficiencySkill), 4, MetallurgyEfficiencySkill.MultiplicativeStrategy),


            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(MillSandRecipe), this.UILink(), 2, typeof(MetallurgySpeedSkill));
            this.Initialize("Mill Sand", typeof(MillSandRecipe));

            CraftingComponent.AddRecipe(typeof(StampingBatteryObject), this);
        }
    }



}
