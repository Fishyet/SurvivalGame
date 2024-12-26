namespace Scripts.ItemModule
{
    public class ItemInfo
    {
        public int ID; // 唯一ID
        public string ItemName; // 物品名称
        public float Value; // 物品价值（是否可出售）
        public ItemQuality Quality; // 物品品质（枚举类型）
        public int MaxStackSize; // 最大可堆叠数量
        public float Weight; // 物品重量
        public string Description; // 物品描述
        public ItemType ItemType; // 枚举物品类型（例如：Consumable, Equipment等）
        
        public ItemInfo(int id, string itemName, float value, ItemQuality quality, int maxStackSize, float weight,
            string description, ItemType itemType)
        {
            ID = id;
            ItemName = itemName;
            Value = value;
            Quality = quality;
            MaxStackSize = maxStackSize;
            Weight = weight;
            Description = description;
            ItemType = itemType;
        }
    }
    
    
    // 物品品质枚举
    public enum ItemQuality
    {
        劣质,
        普通,
        稀有,
        史诗,
        传说
    }
    
    public enum ItemType
    {
        消耗品,
        装备,
        材料
    }
}