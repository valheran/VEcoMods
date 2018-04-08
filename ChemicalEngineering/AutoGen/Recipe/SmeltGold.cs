namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    /*//silenced as is hax and doesnt sit with mod recipes well (lack of tailings in the chain) replaced with "refine gold" in mod
    [RequiresSkill(typeof(BasicSmeltingSkill), 4)]
    
    public class SmeltGoldRecipe : Recipe
    {
        public SmeltGoldRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<GoldIngotItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldOreItem>(typeof(BasicSmeltingEfficiencySkill), 4, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Smelt Gold", typeof(SmeltGoldRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmeltGoldRecipe), this.UILink(), 0.5f, typeof(BasicSmeltingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }*/
}