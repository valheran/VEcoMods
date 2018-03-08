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
    public partial class ChirashiSushiItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Chirashi Sushi"; } }
        public override string Description                      { get { return "Cubes of sashimi seafood and assorted greens served on a bed of vinegared rice"; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat = 11, Protein = 12, Vitamins = 6};
        public override float Calories                          { get { return 540; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CulinaryArtsSkill), 3)] 
    public class ChirashiSushiRecipe : Recipe
    {
        public ChirashiSushiRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<ChirashiSushiItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SashimiFishItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<KelpItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FireweedShootsItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RiceItem>(typeof(CulinaryArtsEfficiencySkill), 10, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Chirashi Sushi", typeof(ChirashiSushiRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ChirashiSushiRecipe), this.UILink(), 6, typeof(CulinaryArtsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}