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
    public partial class StampingBatteryObject :
        WorldObject
    {
        public override string FriendlyName { get { return "StampingBattery"; } }


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");
            this.GetComponent<PowerConsumptionComponent>().Initialize(400);
            this.GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());
            this.GetComponent<HousingComponent>().Set(StampingBatteryItem.HousingVal);


        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class StampingBatteryItem : WorldObjectItem<StampingBatteryObject>
    {
        public override string FriendlyName { get { return "Stamping Battery"; } }
        public override string Description { get { return "Reciprocating Weighted pistons for smashing rocks"; } }

        static StampingBatteryItem()
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
    public partial class StampingBatteryRecipe : Recipe
    {
        public StampingBatteryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StampingBatteryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MetallurgyEfficiencySkill), 25, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(MetallurgyEfficiencySkill), 20, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HewnLogItem>(typeof(MetallurgyEfficiencySkill), 20, MetallurgyEfficiencySkill.MultiplicativeStrategy),

            };
            SkillModifiedValue value = new SkillModifiedValue(180, MetallurgySpeedSkill.MultiplicativeStrategy, typeof(MetallurgySpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(StampingBatteryRecipe), Item.Get<StampingBatteryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<StampingBatteryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("StampingBattery", typeof(StampingBatteryRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}