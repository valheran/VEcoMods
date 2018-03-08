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
    public partial class SushiItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Nigiri Sushi"; } }
        public override string Description                      { get { return "A nutritious variety of fresh fish on rice."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 5, Protein = 13, Vitamins = 8};
        public override float Calories                          { get { return 1080; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(HomeCookingSkill), 2)] 
    public class SushiRecipe : Recipe
    {
        public SushiRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SushiItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(HomeCookingEfficiencySkill), 6, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RiceItem>(typeof(HomeCookingEfficiencySkill), 6, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<KelpItem>(typeof(HomeCookingEfficiencySkill), 6, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Nigiri Sushi", typeof(SushiRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SushiRecipe), this.UILink(), 18, typeof(HomeCookingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}