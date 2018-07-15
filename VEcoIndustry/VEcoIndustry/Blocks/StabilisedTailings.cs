namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Eco.Gameplay.Items;
    using Eco.Shared.Math;
    using Eco.World.Blocks;
    using Eco.World;
    using Eco.Shared.Serialization;
    using Eco.Simulation.WorldLayers;
    using Eco.Gameplay.Pipes;
    using Eco.Gameplay.Items.SearchAndSelect;
    using World = World.World;


    [Serialized]
    [Solid, Diggable]
    public class StabilisedTailingsBlock : Block
    { }

    [Serialized, Weight(30000), StartsDiscovered]
    [MaxStackSize(10)]
    [RequiresTool(typeof(ShovelItem))]
    public class StabilisedTailingsItem : BlockItem<StabilisedTailingsBlock>
    {
        public override string FriendlyName { get { return "Stabilised Tailings"; } }
        public override string FriendlyNamePlural { get { return "Stabilised Tailings"; } }
        public override string Description { get { return "Processed tailings where most of the metals have been extracted and reactive components neutralised"; } }
        public override bool CanStickToWalls { get { return false; } }
    }
}