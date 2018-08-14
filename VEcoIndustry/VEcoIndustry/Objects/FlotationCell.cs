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
    //[RequireComponent(typeof(SolidGroundComponent))]
    public partial class FlotationCellObject :
        WorldObject
    {
        public override string FriendlyName { get { return "FlotationCell"; } }

        static FlotationCellObject()
        {
            WorldObject.AddOccupancy<FlotationCellObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(-2, 0, -2)),
            new BlockOccupancy(new Vector3i(-2, 0, -1)),
            new BlockOccupancy(new Vector3i(-2, 0, 0)),
            new BlockOccupancy(new Vector3i(-2, 0, 1)),
            new BlockOccupancy(new Vector3i(-2, 0, 2)),
            new BlockOccupancy(new Vector3i(-2, 1, -2)),
            new BlockOccupancy(new Vector3i(-2, 1, -1)),
            new BlockOccupancy(new Vector3i(-2, 1, 0)),
            new BlockOccupancy(new Vector3i(-2, 1, 1)),
            new BlockOccupancy(new Vector3i(-2, 1, 2)),
            new BlockOccupancy(new Vector3i(-2, 2, -2)),
            new BlockOccupancy(new Vector3i(-2, 2, -1)),
            new BlockOccupancy(new Vector3i(-2, 2, 0)),
            new BlockOccupancy(new Vector3i(-2, 2, 1)),
            new BlockOccupancy(new Vector3i(-2, 2, 2)),
            new BlockOccupancy(new Vector3i(-2, 3, -2)),
            new BlockOccupancy(new Vector3i(-2, 3, -1)),
            new BlockOccupancy(new Vector3i(-2, 3, 0)),
            new BlockOccupancy(new Vector3i(-2, 3, 1)),
            new BlockOccupancy(new Vector3i(-2, 3, 2)),
            new BlockOccupancy(new Vector3i(-2, 4, -2)),
            new BlockOccupancy(new Vector3i(-2, 4, -1)),
            new BlockOccupancy(new Vector3i(-2, 4, 0)),
            new BlockOccupancy(new Vector3i(-2, 4, 1)),
            new BlockOccupancy(new Vector3i(-2, 4, 2)),
            new BlockOccupancy(new Vector3i(-1, 0, -2)),
            new BlockOccupancy(new Vector3i(-1, 0, -1)),
            new BlockOccupancy(new Vector3i(-1, 0, 0)),
            new BlockOccupancy(new Vector3i(-1, 0, 1)),
            new BlockOccupancy(new Vector3i(-1, 0, 2)),
            new BlockOccupancy(new Vector3i(-1, 1, -2)),
            new BlockOccupancy(new Vector3i(-1, 1, -1)),
            new BlockOccupancy(new Vector3i(-1, 1, 0)),
            new BlockOccupancy(new Vector3i(-1, 1, 1)),
            new BlockOccupancy(new Vector3i(-1, 1, 2)),
            new BlockOccupancy(new Vector3i(-1, 2, -2)),
            new BlockOccupancy(new Vector3i(-1, 2, -1)),
            new BlockOccupancy(new Vector3i(-1, 2, 0)),
            new BlockOccupancy(new Vector3i(-1, 2, 1)),
            new BlockOccupancy(new Vector3i(-1, 2, 2)),
            new BlockOccupancy(new Vector3i(-1, 3, -2)),
            new BlockOccupancy(new Vector3i(-1, 3, -1)),
            new BlockOccupancy(new Vector3i(-1, 3, 0)),
            new BlockOccupancy(new Vector3i(-1, 3, 1)),
            new BlockOccupancy(new Vector3i(-1, 3, 2)),
            new BlockOccupancy(new Vector3i(-1, 4, -2)),
            new BlockOccupancy(new Vector3i(-1, 4, -1)),
            new BlockOccupancy(new Vector3i(-1, 4, 0)),
            new BlockOccupancy(new Vector3i(-1, 4, 1)),
            new BlockOccupancy(new Vector3i(-1, 4, 2)),
            new BlockOccupancy(new Vector3i(0, 0, -2)),
            new BlockOccupancy(new Vector3i(0, 0, -1)),
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(0, 1, -2)),
            new BlockOccupancy(new Vector3i(0, 1, -1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(0, 2, -2)),
            new BlockOccupancy(new Vector3i(0, 2, -1)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(0, 3, -2)),
            new BlockOccupancy(new Vector3i(0, 3, -1)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(0, 4, -2)),
            new BlockOccupancy(new Vector3i(0, 4, -1)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 0, -2)),
            new BlockOccupancy(new Vector3i(1, 0, -1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 1, -2)),
            new BlockOccupancy(new Vector3i(1, 1, -1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 2, -2)),
            new BlockOccupancy(new Vector3i(1, 2, -1)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 3, -2)),
            new BlockOccupancy(new Vector3i(1, 3, -1)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 4, -2)),
            new BlockOccupancy(new Vector3i(1, 4, -1)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 0, -2)),
            new BlockOccupancy(new Vector3i(2, 0, -1)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 1, -2)),
            new BlockOccupancy(new Vector3i(2, 1, -1)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 2, -2)),
            new BlockOccupancy(new Vector3i(2, 2, -1)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 3, -2)),
            new BlockOccupancy(new Vector3i(2, 3, -1)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 4, -2)),
            new BlockOccupancy(new Vector3i(2, 4, -1)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            });

        }
        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<HousingComponent>().Set(FlotationCellItem.HousingVal);

            /* ideal world this produces two slurries - concentrate and tailings
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
    public partial class FlotationCellItem : WorldObjectItem<FlotationCellObject>
    {
        public override string FriendlyName { get { return "FlotationCell"; } }
        public override string Description { get { return "A complex set of machinery for creating equally complex things."; } }

        static FlotationCellItem()
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


    [RequiresSkill(typeof(MetallurgySkill), 2)]
    public partial class FlotationCellRecipe : Recipe
    {
        public FlotationCellRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FlotationCellItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ElectricMotorItem>(typeof(MetallurgyEfficiencySkill), 3, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ServoItem>(typeof(MetallurgyEfficiencySkill), 10, MetallurgyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(MetallurgyEfficiencySkill), 20, MetallurgyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(180, MetallurgySpeedSkill.MultiplicativeStrategy, typeof(MetallurgySpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(FlotationCellRecipe), Item.Get<FlotationCellItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<FlotationCellItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("FlotationCell", typeof(FlotationCellRecipe));
            CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
        }
    }
}