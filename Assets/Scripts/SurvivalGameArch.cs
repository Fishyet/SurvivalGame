using System.IO;
using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;
using UnityEngine;

namespace Scripts
{
    public class SurvivalGameArch : Architecture<SurvivalGameArch>
    {
        protected override void Init()
        {
            this.RegisterSystem<ItemCellSystem>(new ItemCellSystem());
            PlayerInventory inventory;
            if (File.Exists(Application.persistentDataPath + "/inventory.xml"))
            {
                inventory = PlayerInventory.Deserialize(
                    File.ReadAllText(Application.persistentDataPath + "/inventory.xml"));
            }
            else
            {
                inventory = new PlayerInventory();
            }
            
            this.RegisterModel<PlayerInventory>(inventory);
        }
    }
}