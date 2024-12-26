using System.IO;
using QFramework;
using Scripts.ItemModule;
using UnityEngine;

namespace Scripts
{
    public class DebugPanel : MonoSingleton<DebugPanel>, IController
    {
        private string _itemIdInput = "";
        private string _itemNumberInput = "";
        private Inventory _playerInventory;
        
        void Start()
        {
            // Assuming the player inventory is already initialized in the GameManager
            _playerInventory = this.GetModel<PlayerInventory>();
        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 200, 200));
            GUILayout.Label("物品id:");
            _itemIdInput = GUILayout.TextField(_itemIdInput);
            GUILayout.Label("物品数量:");
            _itemNumberInput = GUILayout.TextField(_itemNumberInput);
            
            if (GUILayout.Button("添加物品"))
            {
                if (int.TryParse(_itemIdInput, out int itemId) && int.TryParse(_itemNumberInput, out int itemNumber))
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    bool success = _playerInventory.AddItem(itemId, itemNumber);
                    stopwatch.Stop();
                    Debug.Log($"AddItem execution time: {stopwatch.ElapsedMilliseconds} ms");
                    
                    if (success)
                    {
                        Debug.Log($"成功添加 {itemNumber} 个物品(id={itemId})");
                    }
                    else
                    {
                        Debug.LogError("添加失败，请检查物品id是否正确或者背包是否已满");
                    }
                }
                else
                {
                    Debug.LogError("无效输入，请输入数字");
                }
            }
            
            if (GUILayout.Button("删除物品"))
            {
                if (int.TryParse(_itemIdInput, out int itemId) && int.TryParse(_itemNumberInput, out int itemNumber))
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    bool success = _playerInventory.RemoveItem(itemId, itemNumber);
                    stopwatch.Stop();
                    Debug.Log($"RemoveItem execution time: {stopwatch.ElapsedMilliseconds} ms");
                    
                    if (success)
                    {
                        Debug.Log($"成功删除 {itemNumber} 个物品(id={itemId})");
                    }
                    else
                    {
                        Debug.LogError("删除失败，请检查物品id是否正确或者物品数量是否足够");
                    }
                }
                else
                {
                    Debug.LogError("无效输入，请输入数字");
                }
            }
            
            if (GUILayout.Button("保存背包"))
            {
                string path = Application.persistentDataPath + "/inventory.xml";
                string xml = _playerInventory.Serialize();
                File.WriteAllText(path, xml);
            }
            
            GUILayout.EndArea();
        }
        
        public IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
    }
}