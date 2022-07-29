using UnityEngine;
using UnityEngine.EventSystems;

namespace MuckShields
{

	public class UpdateShield : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		private InventoryCell cell;
		private GameObject inventoryShield;
		public GameObject handheldShield;

		private void Awake()
		{
			this.cell = base.GetComponent<InventoryCell>();

			inventoryShield = new GameObject();
			inventoryShield.AddComponent<MeshFilter>();
			inventoryShield.AddComponent<MeshRenderer>();
			inventoryShield.transform.SetParent(GameObject.Find("Hand.L_end").transform);
			inventoryShield.transform.localPosition = new Vector3(0, 0, 0);
			inventoryShield.transform.localEulerAngles = new Vector3(80, 275, 170);
			inventoryShield.transform.localScale = new Vector3(2, 2, 2);
			inventoryShield.layer = LayerMask.NameToLayer("Player");
			// inventoryShield.SetActive(false);

			handheldShield = new GameObject();
			handheldShield.AddComponent<MeshFilter>();
			handheldShield.AddComponent<MeshRenderer>();
			handheldShield.transform.SetParent(GameObject.Find("Main Camera").transform);
			handheldShield.transform.localPosition = new Vector3(-1.729f, -1.7651f, 1.1921f);
			handheldShield.transform.localEulerAngles = new Vector3(275, 0, 0);
			handheldShield.transform.localScale = new Vector3(1, 1, 1);
			handheldShield.layer = LayerMask.NameToLayer("WeaponSelf");
			handheldShield.AddComponent<AnimateShield>();
			handheldShield.AddComponent<Gun2>();
			// handheldShield.SetActive(false);
		}
		public void OnPointerDown(PointerEventData eventData)
		{
			int itemId;
			if (this.cell.currentItem == null)
			{
				itemId = -1;
			}
			else
			{
				itemId = this.cell.currentItem.id;
			}
			ShowInInventory(itemId);
			ShowInHand(itemId);
			UiSfx.Instance.PlayArmor();
		}
		public void ShowInInventory(int itemId) {
			if (itemId >= 0)
			{
				inventoryShield.GetComponent<MeshFilter>().mesh = ItemManager.Instance.allItems[itemId].mesh;
				inventoryShield.GetComponent<MeshFilter>().sharedMesh = ItemManager.Instance.allItems[itemId].mesh;
				inventoryShield.GetComponent<Renderer>().material = ItemManager.Instance.allItems[itemId].material;
				// inventoryShield.SetActive(true);
			}
			else
			{
				inventoryShield.GetComponent<MeshFilter>().mesh = null;
				inventoryShield.GetComponent<MeshFilter>().sharedMesh = null;
				inventoryShield.GetComponent<Renderer>().material = null;
				// inventoryShield.SetActive(false);
			}
		}
		public void ShowInHand(int itemId) {
			if (itemId >= 0)
			{
				handheldShield.GetComponent<MeshFilter>().mesh = ItemManager.Instance.allItems[itemId].mesh;
				handheldShield.GetComponent<MeshFilter>().sharedMesh = ItemManager.Instance.allItems[itemId].mesh;
				handheldShield.GetComponent<Renderer>().material = ItemManager.Instance.allItems[itemId].material;
				// handheldShield.SetActive(true);
			}
			else
			{
				handheldShield.GetComponent<MeshFilter>().mesh = null;
				handheldShield.GetComponent<MeshFilter>().sharedMesh = null;
				handheldShield.GetComponent<Renderer>().material = null;
				// handheldShield.SetActive(false);
			}
		}
	}
}
