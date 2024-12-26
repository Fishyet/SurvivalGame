using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Scripts.ItemModule
{
    public abstract class Inventory
    {
        public const int DefaultMaxSlot = 30;
        public readonly int MaxSlot;
        
        // 顺序存储物品, 方便随机访问
        public List<Slot> Slots;
        
        // 方便快速通过UID查找物品在哪个Slot
        public readonly Dictionary<string, LinkedList<Slot>> SlotDic;
        private readonly SortedSet<int> _emptySlotIndices;
        
        protected Inventory(int maxSlot = DefaultMaxSlot)
        {
            this.MaxSlot = maxSlot;
            this.Slots = new List<Slot>(MaxSlot);
            this.SlotDic = new Dictionary<string, LinkedList<Slot>>();
            this._emptySlotIndices = new SortedSet<int>();
            for (int i = 0; i < MaxSlot; i++)
            {
                Slot slot = new Slot(i);
                Slots.Add(slot);
                this._emptySlotIndices.Add(i);
                slot.ItemChange.Register(this.OnSlotItemChange);
            }
        }
        
        protected Inventory(int maxSlot, List<Slot> slots)
        {
            this.MaxSlot = maxSlot;
            this.Slots = slots;
            this.SlotDic = new Dictionary<string, LinkedList<Slot>>();
            this._emptySlotIndices = new SortedSet<int>();
            foreach (var slot in slots)
            {
                if (slot.Item != null)
                {
                    this.AddSlotToDic(slot.Item.Uid, slot);
                }
                else
                {
                    this._emptySlotIndices.Add(slot.Index);
                }
                
                slot.ItemChange.Register(this.OnSlotItemChange);
            }
        }
        
        private void OnSlotItemChange(SlotChange changeInfo)
        {
            // slot中的物品变化时，更新SlotDic，方便通过UID查找物品在哪个Slot
            if (changeInfo.OldItem != null)
            {
                this.RemoveSlotInDic(changeInfo.OldItem.Uid, changeInfo.CurSlot);
            }
            
            if (changeInfo.CurSlot.Item != null)
            {
                this.AddSlotToDic(changeInfo.CurSlot.Item.Uid, changeInfo.CurSlot);
                this._emptySlotIndices.Remove(changeInfo.CurSlot.Index);
            }
            else
            {
                this._emptySlotIndices.Add(changeInfo.CurSlot.Index);
            }
        }
        
        private void AddSlotToDic(string uid, Slot slot)
        {
            if (this.SlotDic.TryGetValue(uid, out var slots))
            {
                slots.AddLast(slot);
            }
            else
            {
                var temp = new LinkedList<Slot>();
                temp.AddLast(slot);
                this.SlotDic.Add(uid, temp);
            }
        }
        
        private void RemoveSlotInDic(string uid, Slot slot)
        {
            var slots = this.SlotDic[uid];
            if (slots == null)
            {
                return;
            }
            
            foreach (var s in slots)
            {
                if (s == slot)
                {
                    slots.Remove(s);
                    break;
                }
            }
            
            if (slots.Count == 0)
            {
                this.SlotDic.Remove(uid);
            }
        }
        
        public Slot GetEmptySlot()
        {
            if (this._emptySlotIndices.Count > 0)
            {
                int minIndex = this._emptySlotIndices.Min;
                return Slots[minIndex];
            }
            
            return null;
        }
        
        /// <summary>
        /// 添加常规物品（uid和id相等），如果物品已存在，直接叠加，否则寻找空Slot添加，如果没有空Slot，返回false
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <returns>添加成功，返回true，否则返回false</returns>
        public bool AddItem(int id, int number)
        {
            if (SlotDic.TryGetValue(id.ToString(), out var slots)) // 物品已存在
            {
                foreach (Slot slot in slots) // 遍历所有相同物品的Slot
                {
                    if ((number = slot.SetNumReturnOutRange(slot.Number + number)) == 0) // 当前Slot可以容纳
                    {
                        return true;
                    }
                }
                
                // 遍历完所有Slot，仍有剩余数量，寻找空Slot
                return this.AddItemToEmptySlot(id, number);
            }
            else // 物品不存在
            {
                return this.AddItemToEmptySlot(id, number);
            }
        }
        
        private bool AddItemToEmptySlot(int id, int number)
        {
            while (number > 0)
            {
                Slot emptySlot = GetEmptySlot();
                if (emptySlot != null)
                {
                    Item item = Item.CreateItem(id);
                    if (item == null)
                    {
                        return false;
                    }
                    
                    number = emptySlot.Set(item, 0 + number);
                }
                else
                {
                    return false;
                }
            }
            
            return true;
        }
        
        /// <summary>
        /// 添加特殊物品（uid和id不同，是某种物品的变体），如果物品已存在，直接叠加，否则寻找空Slot添加，如果没有空Slot，返回false
        /// </summary>
        /// <param name="item"></param>
        /// <param name="number"></param>
        /// <returns>添加成功，返回true，否则返回false</returns>
        public bool AddItem(Item item, int number)
        {
            if (SlotDic.TryGetValue(item.Uid, out var slots)) // 物品已存在
            {
                foreach (Slot slot in slots) // 遍历所有相同物品的Slot
                {
                    if ((number = slot.SetNumReturnOutRange(slot.Number + number)) == 0) // 当前Slot可以容纳
                    {
                        return true;
                    }
                }
                
                // 遍历完所有Slot，仍有剩余数量，寻找空Slot
                Slot emptySlot = GetEmptySlot(); // 寻找空Slot
                if (emptySlot != null)
                {
                    emptySlot.Item = item; // 添加物品
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // 物品不存在
            {
                Slot emptySlot = GetEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.Item = item; // 添加物品
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        /// <summary>
        /// 移除常规物品（uid和id相等），如果物品数量大于number，直接减少数量，否则移除物品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <returns>如果移除成功，返回true，若没有找到这个物品，返回false</returns>
        public bool RemoveItem(int id, int number)
        {
            if (SlotDic.TryGetValue(id.ToString(), out var slots))
            {
                foreach (Slot slot in slots.ToArray())
                {
                    if ((number = -slot.SetNumReturnOutRange(slot.Number - number)) == 0)
                    {
                        return true;
                    }
                }
                
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// 移除特殊物品（uid和id不同，是某种物品的变体），如果物品数量大于number，直接减少数量，否则移除物品
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="number"></param>
        /// <returns>如果移除成功，返回true，若没有找到这个物品，返回false</returns>
        public bool RemoveItem(string uid, int number)
        {
            if (SlotDic.TryGetValue(uid, out var slots))
            {
                foreach (Slot slot in slots.ToArray())
                {
                    if ((number = -slot.SetNumReturnOutRange(slot.Number - number)) == 0)
                    {
                        return true;
                    }
                }
                
                return true;
            }
            
            return false;
        }
        
        public virtual string Serialize()
        {
            var slotsElement = new XElement("Slots", Slots.Select(slot => slot.Serialize()));
            var element = new XElement("Inventory", slotsElement);
            return element.ToString();
        }
        
        public static Inventory Deserialize<T>(string serializedData) where T : Inventory
        {
            var element = XElement.Parse(serializedData);
            var slots = element.Element("Slots").Elements().Select(e => Slot.Deserialize(e)).ToList();
            
            var ctor = typeof(T).GetConstructor(new[] { typeof(int), typeof(List<Slot>) });
            if (ctor != null)
            {
                var inventory = ctor.Invoke(new object[] { DefaultMaxSlot, slots }) as Inventory;
                return inventory;
            }
            else
            {
                Debug.LogError(
                    $"Deserialize inventory failed: {typeof(T)} has no constructor with parameters (int, List<Slot>)");
                return null;
            }
        }
        
        public static void SwapItemInSlot(Slot slot1, Slot slot2)
        {
            if (slot1 == null || slot2 == null)
            {
                return;
            }
            
            Item tempItem = slot1.Item;
            int tempNumber = slot1.Number;
            slot1.Set(slot2.Item, slot2.Number);
            slot2.Set(tempItem, tempNumber);
        }
    }
}