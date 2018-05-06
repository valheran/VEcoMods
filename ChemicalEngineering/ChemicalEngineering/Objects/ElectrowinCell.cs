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
    [RequireComponent(typeof(PipeComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public partial class ElectrowinCellObject :
        WorldObject
    {
        public override string FriendlyName { get { return "Electrowinning Cell"; } }


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");
            this.GetComponent<PowerConsumptionComponent>().Initialize(3000);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<HousingComponent>().Set(ElectrowinCellItem.HousingVal);

            /* Ultimately would like this to be feed by a liquid instead of item
            var tankList = new List<LiquidTank>();

            tankList.Add(new LiquidProducer("Chimney", typeof(SmogItem), 100,
                    null,
                    this.Occupancy.Find(x => x.Name == "ChimneyOut"),
                        (float)(0.3f * SmogItem.SmogItemsPerCO2PPM) / TimeUtil.SecondsPerHour));



            this.GetComponent<PipeComponent>().Initialize(tankList);
            */
        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class ElectrowinCellItem : WorldObjectItem<ElectrowinCellObject>
    {
        public override string FriendlyName { get { return "ElectrowinCell"; } }
        public override string Description { get { return "A complex set of machinery for creating equally complex things."; } }

        static ElectrowinCellItem()
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


    [RequiresSkill(typeof(MetallurgySkill), 3)]
    public partial class ElectrowinCellRecipe : Recipe
    {
        public ElectrowinCellRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectrowinCellItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CopperWiringItem>(typeof(MetallurgyEfficiencySkill), 6, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ServoItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(MetallurgyEfficiencySkill), 30, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(180, MetallurgySpeedSkill.MultiplicativeStrategy, typeof(MetallurgySpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ElectrowinCellRecipe), Item.Get<ElectrowinCellItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ElectrowinCellItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("ElectrowinCell", typeof(ElectrowinCellRecipe));
            CraftingComponent.AddRecipe(typeof(MachineShopObject), this);
        }
    }
}