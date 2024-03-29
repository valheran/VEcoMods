namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
    using Eco.Mods;

    [Serialized]
    [RequiresSkill(typeof(MiningEfficiencySkill), 3)]
    public partial class AdvancedMiningSkill : Skill
    {
        public override string FriendlyName { get { return "Advanced Mining"; } }
        public override string Description { get { return "Get digging.... in style"; } }

        public static ModificationStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.2f, 1 - 0.35f, 1 - 0.5f, 1 - 0.65f, 1 - 0.8f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
        public static int[] SkillPointCost = { 4, 6, 8, 8, 10 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 4; } }

        /*[Serialized]
        public partial class AdvancedMiningSkillBook : SkillBook<AdvancedMiningSkill, AdvancedMiningSkillScroll>
        {
            public override string FriendlyName { get { return "Advanced Mining Skill Book"; } }
        }

        [Serialized]
        public partial class AdvancedMiningSkillScroll : SkillScroll<AdvancedMiningSkill, AdvancedMiningSkillBook>
        {
            public override string FriendlyName { get { return "Advanced Mining Skill Scroll"; } }
        }

        [RequiresSkill(typeof(BasicEngineeringSkill), 0)] 
        public partial class AdvancedMiningSkillBookRecipe : Recipe
        {
            public AdvancedMiningSkillBookRecipe()
            {
                this.Products = new CraftingElement[]
                {
                    new CraftingElement<AdvancedMiningSkillBook>(),
                };
                this.Ingredients = new CraftingElement[]
                {
                    new CraftingElement<IronOreItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy),
                    new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy),
                    new CraftingElement<CopperOreItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy),
                    new CraftingElement<CoalItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy), 
                };
                this.CraftMinutes = new ConstantValue(15);

                this.Initialize("AdvancedMining Skill Book", typeof(AdvancedMiningSkillBookRecipe));
                CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
            }
        }*/
    }
}
