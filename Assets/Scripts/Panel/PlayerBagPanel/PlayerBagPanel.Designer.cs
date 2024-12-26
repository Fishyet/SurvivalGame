using UnityEngine;

namespace Scripts.Panel.PlayerBagPanel
{
	// Generate Id:91b9fcda-d3a2-48ea-a953-7f95ad03a70d
	public partial class PlayerBagPanel
	{
		public const string Name = "PlayerBagPanel";
		
		[SerializeField]
		public InfoDisplay InfoDisplay;
		[SerializeField]
		public ItemCells ItemCells;
		[SerializeField]
		public EquipmentCells EquipmentCells;
		
		private PlayerBagPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			InfoDisplay = null;
			ItemCells = null;
			EquipmentCells = null;
			
			mData = null;
		}
		
		public PlayerBagPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		PlayerBagPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new PlayerBagPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
