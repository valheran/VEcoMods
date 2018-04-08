namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    //modified to produce slag and require flux
    [RequiresSkill(typeof(BasicSmeltingSkill), 3)]
    public class SmeltIronRecipe : Recipe
    {
        public SmeltIronRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<IronIngotItem>(2),
               new CraftingElement<SlagItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronOreItem>(typeof(BasicSmeltingEfficiencySkill), 8, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(BasicSmeltingEfficiencySkill), 2, BasicSmeltingEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Smelt Iron", typeof(SmeltIronRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmeltIronRecipe), this.UILink(), 0.5f, typeof(BasicSmeltingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}