using System;
using System.Xml.Linq;
using Scripts.ItemModule.ConcreteItem;
using UnityEngine;

namespace Scripts.ItemModule
{
    public abstract class Item
    {
        protected readonly ItemInfo Info;
        public virtual int ID => Info.ID;
        public virtual string Uid => Info.ID.ToString();
        public virtual string ItemName => Info.ItemName;
        public virtual float Value => Info.Value;
        public virtual ItemQuality Quality => Info.Quality;
        public virtual int MaxStackSize => Info.MaxStackSize;
        public virtual float Weight => Info.Weight;
        public virtual string Description => Info.Description;
        public virtual ItemType ItemType => Info.ItemType;
        
        protected Item(ItemInfo info)
        {
            Info = info;
        }
        
        public static bool Equals(Item item1, Item item2)
        {
            if (item1 == null && item2 == null) // 两个都是null, 返回true
            {
                return true;
            }
            
            if (item1 == null || item2 == null) // 一个是null, 一个不是null, 返回false
            {
                return false;
            }
            
            return item1.Uid == item2.Uid; // 两个都不是null, 比较UID
        }
        
        public virtual XElement Serialize()
        {
            var element = new XElement("Item",
                new XElement("ID", ID),
                new XElement("Uid", Uid),
                new XElement("ItemType", ItemType)
            );
            return element;
        }
        
        public static Item Deserialize(XElement serializedData)
        {
            if (serializedData.Value == "")
            {
                return null;
            }
            
            int id = 0;
            try
            {
                id = int.Parse(serializedData.Element("ID").Value);
                return CreateItem(id);
            }
            catch (Exception e)
            {
                Debug.LogError($"Parse item ID({serializedData}) failed: {e.Message}");
            }
            
            return null;
        }
        
        public static Item CreateItem(int itemId)
        {
            Database.Instance.ItemInfoDic.TryGetValue(itemId, out ItemInfo info);
            if (info == null)
            {
                return null;
            }
            
            return info.ItemType switch
            {
                ItemType.消耗品 => new ConsumableItem(info),
                ItemType.装备 => new EquipmentItem(info),
                ItemType.材料 => new MateriaItem(info),
                _ => null
            };
        }
    }
}