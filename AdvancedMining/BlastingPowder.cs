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

    [RequiresSkill(typeof(BlastingSkill), 1)]   
    public partial class BlastingPowderRecipe : Recipe
    {
        public BlastingPowderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BlastingPowderItem>(),          
            
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PotashItem>(typeof(BlastingEfficiencySkill), 10, BlastingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CoalItem>(typeof(BlastingEfficiencySkill), 5, BlastingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BlastingPowderRecipe), Item.Get<BlastingPowderItem>().UILink(), 2, typeof(BlastingSpeedSkill));    
            this.Initialize("Blasting Powder", typeof(BlastingPowderRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
               
    public partial class BlastingPowderItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Blasting Powder"; } }
        public override string Description { get { return "The basic explosive."; } }

    }

}