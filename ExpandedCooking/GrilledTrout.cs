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
    [Weight(300)]                                          
    public partial class GrilledTroutItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Grilled Trout"; } }
        public override string Description                      { get { return "Generous fillets of fish wrapped in fireweed cooked on the coals."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 9, Protein = 15, Vitamins = 2};
        public override float Calories                          { get { return 900; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
    [RequiresSkill(typeof(CampfireCreationsSkill), 3)] 
    public class GrilledTroutRecipe : Recipe
    {
        public GrilledTroutRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<GrilledTroutItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(typeof(CampfireCreationsEfficiencySkill), 6, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FireweedShootsItem>(typeof(CampfireCreationsEfficiencySkill), 1, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FiddleheadsItem>(typeof(CampfireCreationsEfficiencySkill), 1, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TomatoItem>(typeof(CampfireCreationsEfficiencySkill), 3, CampfireCreationsEfficiencySkill.MultiplicativeStrategy),
               
            };
            this.Initialize("Grilled Trout", typeof(GrilledTroutRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(GrilledTroutRecipe), this.UILink(), 9, typeof(CampfireCreationsSpeedSkill));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}