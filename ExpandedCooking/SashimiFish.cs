namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(500)]                                          
    public partial class SashimiFishItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Shashimi Fish"; } }
        public override string Description                      { get { return "Delicate slivers of the choicest fish"; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 9, Protein = 4, Vitamins = 0};
        public override float Calories                          { get { return 540; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MeatPrepSkill), 4)]    
    public partial class SashimiFishRecipe : Recipe
    {
        public SashimiFishRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(),
               
               new CraftingElement<FishScrapsItem>(10), 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(MeatPrepEfficiencySkill), 50, MeatPrepEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SashimiFishRecipe), Item.Get<SashimiFishItem>().UILink(), 2, typeof(MeatPrepSpeedSkill)); 
            this.Initialize("Sashimi Fish", typeof(SashimiFishRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}