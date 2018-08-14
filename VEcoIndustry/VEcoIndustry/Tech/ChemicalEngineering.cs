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
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;

    [Serialized]
    [RequiresSkill(typeof(EngineerSkill), 0)]
    public partial class ChemicalEngineeringSkill : Skill
    {
        public override string FriendlyName { get { return "Chemical Engineering"; } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 2, 2, 4, 8 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class ChemicalEngineeringSkillBook : SkillBook<ChemicalEngineeringSkill, ChemicalEngineeringSkillScroll>
    {
        public override string FriendlyName { get { return "ChemicalEngineering Skill Book"; } }
    }

    [Serialized]
    public partial class ChemicalEngineeringSkillScroll : SkillScroll<ChemicalEngineeringSkill, ChemicalEngineeringSkillBook>
    {
        public override string FriendlyName { get { return "Chemical Engineering Skill Scroll"; } }
    }

    [RequiresSkill(typeof(EngineerSkill), 0)]
    public partial class ChemicalEngineeringSkillBookRecipe : Recipe
    {
        public ChemicalEngineeringSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ChemicalEngineeringSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperOreItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize("ChemicalEngineering Skill Book", typeof(ChemicalEngineeringSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
