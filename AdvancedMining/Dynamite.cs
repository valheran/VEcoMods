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
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(BlastingSkill), 2)]   
    public partial class DynamiteRecipe : Recipe
    {
        public DynamiteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DynamiteItem>(),          
           
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BlastingPowderItem>(typeof(BlastingEfficiencySkill), 5, BlastingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<DirtItem>(typeof(BlastingEfficiencySkill), 2, BlastingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(BlastingEfficiencySkill), 5, BlastingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(DynamiteRecipe), Item.Get<DynamiteItem>().UILink(), 2, typeof(BlastingSpeedSkill));    
            this.Initialize("Dynamite", typeof(DynamiteRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(2000)]      
    [Fuel(1)]     //using this as a proxy for blast radius, not becuase its intended as a fuel         
    public partial class DynamiteItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Dynamite"; } }
        public override string Description { get { return "Stabilised explosive in convenient stick form"; } }

    }

}