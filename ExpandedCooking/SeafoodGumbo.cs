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
    public partial class SeafoodGumboItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Seafood Gumbo"; } }
        public override string Description                      { get { return "Spicy seafood stew with all the trimmings."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 7, Fat = 14, Protein = 18, Vitamins = 10};
        public override float Calories                          { get { return 1080; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CulinaryArtsSkill), 4)] 
    public class SeafoodGumboRecipe : Recipe
    {
        public SeafoodGumboRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SeafoodGumboItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<InfusedOilItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FishScrapsItem>(typeof(CulinaryArtsEfficiencySkill), 20, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<VegetableMedleyItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SashimiFishItem>(typeof(CulinaryArtsEfficiencySkill), 8, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SashimiFishItem>(typeof(CulinaryArtsEfficiencySkill), 8, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClamItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<UrchinItem>(typeof(CulinaryArtsEfficiencySkill), 4, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize("Seafood Gumbo", typeof(SeafoodGumboRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SeafoodGumboRecipe), this.UILink(), 18, typeof(CulinaryArtsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}