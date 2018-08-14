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

    [RequiresSkill(typeof(ChloralkaliSkill), 1)]   
    public partial class CrudeSoapRecipe : Recipe
    {
        public CrudeSoapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SoapItem>(1),
                new CraftingElement<GlycerolItem>(1),
           
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PotashItem>(typeof(ChloralkaliEfficiencySkill), 5, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BeansItem>(typeof(ChloralkaliEfficiencySkill), 50, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CrudeSoapRecipe), Item.Get<SoapItem>().UILink(), 2, typeof(ChloralkaliSpeedSkill));    
            this.Initialize("Soap", typeof(CrudeSoapRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    public partial class VegetableSoapRecipe : Recipe
    {
        public VegetableSoapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SoapItem>(1),
                new CraftingElement<GlycerolItem>(2),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PotashItem>(typeof(ChloralkaliEfficiencySkill), 2, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<OilItem>(typeof(ChloralkaliEfficiencySkill), 5, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(VegetableSoapRecipe), Item.Get<SoapItem>().UILink(), 2, typeof(ChloralkaliSpeedSkill));
            this.Initialize("Soap", typeof(VegetableSoapRecipe));

            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
    public partial class TallowSoapRecipe : Recipe
    {
        public TallowSoapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SoapItem>(2),
                new CraftingElement<GlycerolItem>(3),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PotashItem>(typeof(ChloralkaliEfficiencySkill), 2, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(ChloralkaliEfficiencySkill), 5, ChloralkaliEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TallowSoapRecipe), Item.Get<SoapItem>().UILink(), 2, typeof(ChloralkaliSpeedSkill));
            this.Initialize("Soap", typeof(TallowSoapRecipe));

            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class SoapItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Soap"; } }
        public override string Description { get { return "Mix with water to make a cleansing lather"; } }

    }

}