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
    public partial class PoachedSalmonItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Poached Salmon"; } }
        public override string Description                      { get { return "Sous vide salmon belly."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 14, Protein = 21, Vitamins = 0};
        public override float Calories                          { get { return 540; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(HomeCookingSkill), 3)] 
    public class PoachedSalmonRecipe : Recipe
    {
        public PoachedSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<PoachedSalmonItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PreparedFishItem>(typeof(HomeCookingEfficiencySkill), 5, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FishStockItem>(typeof(HomeCookingEfficiencySkill), 2, HomeCookingEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.Initialize("Poached Salmon", typeof(PoachedSalmonRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(PoachedSalmonRecipe), this.UILink(), 9, typeof(HomeCookingSpeedSkill));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}