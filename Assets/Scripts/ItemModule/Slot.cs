using System;
using System.Xml.Linq;
using QFramework;
using Scripts.Panel.Basic;
using UnityEngine;

namespace Scripts.ItemModule
{
    public class Slot
    {
        private int _number;
        private Item _item;
        public static readonly Slot NullSlot = new Slot(-1);
        
        public float TotalWeight { get; private set; } // 总重量
        
        private static void UpdateTotalWeight(SlotChange changeInfo) // 更新总重量的方法
        {
            Slot slot = changeInfo.CurSlot;
            slot.TotalWeight = slot.Item == null ? 0 : slot.Item.Weight * slot.Number;
            slot.WeightChange.Trigger(slot.TotalWeight);
        }
        
        public readonly int Index;
        
        public readonly EasyEvent<SlotChange> NumberChange = new EasyEvent<SlotChange>();
        public readonly EasyEvent<SlotChange> ItemChange = new EasyEvent<SlotChange>();
        public readonly EasyEvent<float> WeightChange = new EasyEvent<float>();
        
        
        /// <summary>
        /// 警告！警告！警告！
        /// 这个事件是我拉的一坨屎，按说Slot不应该知道ItemCell的存在，但是由于Inventory的设计问题，
        /// 即，Inventory只能查询到Slot，无法查询到ItemCell，所以只能通过这种方式来通知ItemCell触发动画
        /// </summary>
        public readonly EasyEvent<ItemCell> AddEvent = new EasyEvent<ItemCell>();
        
        public bool IsEmpty => this.Item == null;
        public bool IsFull => this.Item != null && this.Number >= this.Item.MaxStackSize;
        
        public Slot(int index, Item item = null, int number = 0)
        {
            this.Index = index;
            this.ItemChange.Register(UpdateTotalWeight); // 注册更新总重量的事件
            this.NumberChange.Register(UpdateTotalWeight); // 注册更新总重量的事件
            this.Set(item, number);
        }
        
        public int Number => this._number;
        
        /// <summary>
        /// 供外部访问使用的API: 物品数量
        /// 第二层封装，当物品数量变化时，触发NumberChange事件
        /// </summary>
        public int SetNumReturnOutRange(int number)
        {
            Item oldItem = this.Item;
            int oldNumber = this.Number;
            int excess = this.SetNumber(number);
            this.Check(oldItem, oldNumber);
            return excess;
        }
        
        /// <summary>
        /// 供外部访问使用的API: 物品
        /// 第二层封装，当物品UID变化时，触发ItemChange事件
        /// </summary>
        public Item Item
        {
            get => _item;
            set
            {
                int oldNumber = this.Number;
                Item oldItem = this.Item;
                this.SetItem(value);
                this.Check(oldItem, oldNumber);
            }
        }
        
        /// <summary>
        /// 第二层封装，设置物品和数量，如果物品或数量发生变化，则触发对应事件
        /// </summary>
        /// <param name="item">设置物品</param>
        /// <param name="number">设置数量</param>
        public int Set(Item item, int number)
        {
            Item oldItem = this.Item;
            int oldNumber = this.Number;
            this.SetItem(item);
            int excess = this.SetNumber(number);
            this.Check(oldItem, oldNumber);
            return excess;
        }
        
        /// <summary>
        /// 第一层封装，设置物品，同时保证物品为空时数量为0
        /// </summary>
        /// <param name="item"></param>
        private void SetItem(Item item)
        {
            this._item = item;
            if (this._item == null)
            {
                this._number = 0;
            }
        }
        
        /// <summary>
        /// 第一层封装，设置数量，同时保证数量不为负数, 不超过最大堆叠上限，且当数量减为0时，物品为空
        /// </summary>
        /// <param name="number">数量</param>
        /// <returns>超出的数量，如果为负数，则表示减少的数量</returns>
        private int SetNumber(int number)
        {
            int excess = 0;
            if (number < 0)
            {
                excess = number;
                number = 0;
            }
            
            if (this.Item != null && number > this.Item.MaxStackSize)
            {
                excess = number - this.Item.MaxStackSize;
                number = this.Item.MaxStackSize;
            }
            
            this._number = number;
            if (this._number == 0)
            {
                this._item = null;
            }
            
            return excess;
        }
        
        public XElement Serialize()
        {
            var element = new XElement("Slot",
                new XElement("Index", this.Index),
                this.Item == null ? new XElement("Item") : this.Item.Serialize(),
                new XElement("Number", this.Number)
            );
            return element;
        }
        
        public static Slot Deserialize(XElement serializedData)
        {
            int index = -1;
            Item item = null;
            int number = 0;
            try
            {
                index = int.Parse(serializedData.Element("Index").Value);
                item = Item.Deserialize(serializedData.Element("Item"));
                number = int.Parse(serializedData.Element("Number").Value);
            }
            catch (Exception e)
            {
                Debug.LogError($"Parse item ID({serializedData}) failed: {e.Message}");
            }
            
            return new Slot(index, item, number);
        }
        
        /// <summary>
        /// 供外部访问的API，用于清空Slot
        /// </summary>
        public void Clear()
        {
            this.Set(null, 0);
        }
        
        /// <summary>
        /// 注意：在一切改变之后再调用该方法，不要再改变中调用该方法
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="oldNumber"></param>
        private void Check(Item oldItem, int oldNumber)
        {
            if (Item.Equals(oldItem, this.Item) == false)
            {
                this.ItemChange.Trigger(new SlotChange(oldItem, oldNumber, this));
            }
            
            if (oldNumber != this.Number)
            {
                this.NumberChange.Trigger(new SlotChange(oldItem, oldNumber, this));
            }
        }
        
        public bool IsSameItem(Slot slot)
        {
            return Item.Equals(this.Item, slot.Item);
        }
    }
    
    public struct SlotChange
    {
        public Item OldItem;
        public int OldNumber;
        public Slot CurSlot;
        
        public SlotChange(Item oldItem, int oldNumber, Slot curSlot)
        {
            OldItem = oldItem;
            OldNumber = oldNumber;
            CurSlot = curSlot;
        }
    }
}