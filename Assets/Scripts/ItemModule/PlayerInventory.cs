using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using QFramework;

namespace Scripts.ItemModule
{
    public class PlayerInventory : Inventory, IModel
    {
        # region AbstractModel
        
        private IArchitecture _mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => _mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => _mArchitecture = architecture;
        
        public bool Initialized { get; set; }
        void ICanInit.Init() => OnInit();
        public void Deinit() => OnDeinit();
        
        private void OnDeinit()
        {
        }
        
        private void OnInit()
        {
        }
        
        # endregion
        
        public const int DefaultPlayerInvCapacity = 35;
        public Slot LeftHandSlot { get; private set; }
        public Slot RightHandSlot => Slots[0];
        public Slot HeadSlot { get; private set; }
        public Slot BodySlot { get; private set; }
        public Slot LegSlot { get; private set; }
        public Slot FootSlot { get; private set; }
        
        public PlayerInventory(int maxSlot = DefaultPlayerInvCapacity) : base(maxSlot)
        {
        }
        
        public PlayerInventory(int maxSlot, List<Slot> slots) : base(maxSlot, slots)
        {
        }
        
        public override string Serialize()
        {
            var slotsElement = new XElement("Slots", Slots.Select(slot => slot.Serialize()));
            var equipmentElement = new XElement("Equipment",
                new XElement("LeftHandSlot", LeftHandSlot?.Serialize()),
                new XElement("HeadSlot", HeadSlot?.Serialize()),
                new XElement("BodySlot", BodySlot?.Serialize()),
                new XElement("LegSlot", LegSlot?.Serialize()),
                new XElement("FootSlot", FootSlot?.Serialize())
            );
            var element = new XElement("PlayerInventory", slotsElement, equipmentElement);
            return element.ToString();
        }
        
        public static PlayerInventory Deserialize(string xml)
        {
            var element = XElement.Parse(xml);
            var slots = element.Element("Slots").Elements().Select(e => Slot.Deserialize(e)).ToList();
            var equipmentElement = element.Element("Equipment");
            
            var leftHandSlot = equipmentElement.Element("LeftHandSlot")?.Elements().Select(e => Slot.Deserialize(e))
                .FirstOrDefault();
            var headSlot = equipmentElement.Element("HeadSlot")?.Elements().Select(e => Slot.Deserialize(e))
                .FirstOrDefault();
            var bodySlot = equipmentElement.Element("BodySlot")?.Elements().Select(e => Slot.Deserialize(e))
                .FirstOrDefault();
            var legSlot = equipmentElement.Element("LegSlot")?.Elements().Select(e => Slot.Deserialize(e))
                .FirstOrDefault();
            var footSlot = equipmentElement.Element("FootSlot")?.Elements().Select(e => Slot.Deserialize(e))
                .FirstOrDefault();
            
            var inventory = new PlayerInventory(DefaultPlayerInvCapacity, slots)
            {
                LeftHandSlot = leftHandSlot ?? new Slot(-2),
                HeadSlot = headSlot ?? new Slot(-2),
                BodySlot = bodySlot ?? new Slot(-2),
                LegSlot = legSlot ?? new Slot(-2),
                FootSlot = footSlot ?? new Slot(-2)
            };
            return inventory;
        }
    }
}