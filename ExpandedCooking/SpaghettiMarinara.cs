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
    public partial class SpaghettiMarinaraItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Spaghetti Marinara"; } }
        public override string Description                      { get { return "Fresh seafood with homemade pasta. Just like Nonna makes it!."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 7, Fat = 7, Protein = 11, Vitamins = 7};
        public override float Calories                          { get { return 1350; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(HomeCookingSkill), 4)] 
    public class SpaghettiMarinaraRecipe : Recipe
    {
        public SpaghettiMarinaraRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SpaghettiMarinaraItem>(2),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(HomeCookingEfficiencySkill), 16, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClamItem>(typeof(HomeCookingEfficiencySkill), 6, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<UrchinItem>(typeof(HomeCookingEfficiencySkill), 6, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FlourItem>(typeof(HomeCookingEfficiencySkill), 30, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<YeastItem>(typeof(HomeCookingEfficiencySkill), 15, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(HomeCookingEfficiencySkill), 4, HomeCookingEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Spaghetti Marinara", typeof(SpaghettiMarinaraRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SpaghettiMarinaraRecipe), this.UILink(), 27, typeof(HomeCookingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}