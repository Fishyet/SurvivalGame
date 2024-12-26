/****************************************************************************
 * 2024.12 DESKTOP-STANAOI
 ****************************************************************************/

using UnityEngine;

namespace Scripts.Panel.PlayerBagPanel
{
	public partial class ItemCells
	{
		[SerializeField] public UnityEngine.UI.Button Equipment;
		[SerializeField] public UnityEngine.UI.Button Material;
		[SerializeField] public UnityEngine.UI.Button Consumable;
		[SerializeField] public RectTransform CellContainer;

		public void Clear()
		{
			Equipment = null;
			Material = null;
			Consumable = null;
			CellContainer = null;
		}

		public override string ComponentName
		{
			get { return "ItemCells";}
		}
	}
}
