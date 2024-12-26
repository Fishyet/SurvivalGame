/****************************************************************************
 * 2024.12 DESKTOP-STANAOI
 ****************************************************************************/

using Scripts.Panel.PlayerBagPanel.Component;
using UnityEngine;

namespace Scripts.Panel.PlayerBagPanel
{
	public partial class InfoDisplay
	{
		[SerializeField] public DisplayCell DisplayCell;
		[SerializeField] public UnityEngine.UI.Text ItemName;
		[SerializeField] public UnityEngine.UI.Text ItemType;
		[SerializeField] public UnityEngine.UI.Text ItemQuality;
		[SerializeField] public UnityEngine.UI.Text ItemValue;
		[SerializeField] public UnityEngine.UI.Text ItemWeight;
		[SerializeField] public UnityEngine.UI.Text ItemDescription;

		public void Clear()
		{
			DisplayCell = null;
			ItemName = null;
			ItemType = null;
			ItemQuality = null;
			ItemValue = null;
			ItemWeight = null;
			ItemDescription = null;
		}

		public override string ComponentName
		{
			get { return "InfoDisplay";}
		}
	}
}
