namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;

    [Serialized]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(HousingComponent))]
    //[RequireComponent(typeof(SolidGroundComponent))]
    public partial class GrindingMillObject :
        WorldObject
    {
        public override string FriendlyName { get { return "GrindingMill"; } }


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");
            this.GetComponent<PowerConsumptionComponent>().Initialize(2500);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<HousingComponent>().Set(GrindingMillItem.HousingVal);


        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class GrindingMillItem : WorldObjectItem<GrindingMillObject>
    {
        public override string FriendlyName { get { return "GrindingMill"; } }
        public override string Description { get { return "A Semi Autogenous Grinding mill- mash things into very, very small pieces."; } }

        static GrindingMillItem()
        {




        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren]
        public static HousingValue HousingVal
        {
            get
            {
                return new HousingValue()
                {
                    Category = "Industrial",
                    TypeForRoomLimit = "",
                };
            }
        }
    }


    [RequiresSkill(typeof(MetallurgySkill), 1)]
    public partial class GrindingMillRecipe : Recipe
    {
        public GrindingMillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GrindingMillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ElectricMotorItem>(typeof(MetallurgyEfficiencySkill), 6, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(MetallurgyEfficiencySkill), 50, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(MetallurgyEfficiencySkill), 15, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(180, MetallurgySpeedSkill.MultiplicativeStrategy, typeof(MetallurgySpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(GrindingMillRecipe), Item.Get<GrindingMillItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<GrindingMillItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("GrindingMill", typeof(GrindingMillRecipe));
            CraftingComponent.AddRecipe(typeof(MachineShopObject), this);
        }
    }
}