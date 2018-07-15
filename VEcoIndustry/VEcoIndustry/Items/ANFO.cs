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

    [RequiresSkill(typeof(NitroChemSkill), 4)]   
    public partial class ANFORecipe : Recipe
    {
        public ANFORecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ANFOItem>(),          
           
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<NitratesItem>(typeof(NitroChemEfficiencySkill), 3, NitroChemEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PetroleumItem>(typeof(NitroChemEfficiencySkill), 5, NitroChemEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ANFORecipe), Item.Get<ANFOItem>().UILink(), 5, typeof(NitroChemSpeedSkill));    
            this.Initialize("ANFO", typeof(ANFORecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(4000)]      
    [Fuel(10)]        //using this as a proxy for blast radius, not becuase its intended as a fuel  
    public partial class ANFOItem :
    Item                                     
    {
        public override string FriendlyName { get { return "ANFO"; } }
        public override string Description { get { return "Stable emulsion of Nitrate and fuel oil extremely effective at breaking rock"; } }

    }

}