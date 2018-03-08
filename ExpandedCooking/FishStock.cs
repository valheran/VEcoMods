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
    public partial class FishStockItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Fish Stock"; } }
        public override string Description                      { get { return "Tasty, but mostly used as a base for more complex dishes"; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 4, Protein = 6, Vitamins = 4};
        public override float Calories                          { get { return 630; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(HomeCookingSkill), 2)] 
    public class FishStockRecipe : Recipe
    {
        public FishStockRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<FishStockItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FishScrapsItem>(typeof(HomeCookingEfficiencySkill), 20, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Fish Stock", typeof(FishStockRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishStockRecipe), this.UILink(), 18, typeof(HomeCookingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}