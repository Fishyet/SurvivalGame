using System;
using System.Collections.Generic;
using System.Xml.Linq;
using QFramework;
using Scripts.ItemModule;
using UnityEngine;

namespace Scripts
{
    public class Database : Singleton<Database>
    {
        private const string ItemInfoPath = "Config/Item";
        public readonly Dictionary<int, ItemInfo> ItemInfoDic = new Dictionary<int, ItemInfo>();
        
        private Database()
        {
            LoadItemInfos();
        }
        
        private void LoadItemInfos()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(ItemInfoPath);
            XDocument doc = XDocument.Parse(textAsset.text);
            
            foreach (XElement itemElement in doc.Descendants("Item"))
            {
                try
                {
                    ItemInfo item = new ItemInfo(
                        int.Parse(itemElement.Element("ID").Value),
                        itemElement.Element("ItemName").Value,
                        float.Parse(itemElement.Element("Value").Value),
                        (ItemQuality)Enum.Parse(typeof(ItemQuality), itemElement.Element("Quality").Value),
                        int.Parse(itemElement.Element("MaxStackSize").Value),
                        float.Parse(itemElement.Element("Weight").Value),
                        itemElement.Element("Description").Value,
                        (ItemType)Enum.Parse(typeof(ItemType), itemElement.Element("ItemType").Value)
                    );
                    ItemInfoDic.Add(item.ID, item);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Parse item {itemElement} info failed: " + e.Message);
                }
            }
        }
        
        public override void Dispose()
        {
            base.Dispose();
            ItemInfoDic.Clear();
        }
    }
}