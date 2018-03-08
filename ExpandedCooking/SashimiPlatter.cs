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
    public partial class SashimiPlatterItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Sashimi Platter"; } }
        public override string Description                      { get { return "Carefully selected slices of the best quality fish available."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 10, Protein = 18, Vitamins = 4};
        public override float Calories                          { get { return 720; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(HomeCookingSkill), 4)] 
    public class SashimiPlatterRecipe : Recipe
    {
        public SashimiPlatterRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SashimiPlatterItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(HomeCookingEfficiencySkill), 10, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BeansItem>(typeof(HomeCookingEfficiencySkill), 1, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Sashimi Platter", typeof(SashimiPlatterRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SashimiPlatterRecipe), this.UILink(), 9, typeof(HomeCookingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}