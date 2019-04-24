using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemForSale : MonoBehaviour {
	public string itemName;
	public Item.TYPE type;
	public Text priceText;
	public int price;
    public GameObject ownedText;
	ConfirmButtonManager confirmPurchaseMng;

	private GameObject purchaseConfirmBox;
	// Use this for initialization
	void Start () {
        ownedText.SetActive(false);
        print(PlayerProgress.playerData.playerName);
		if(CheckIfPurchased() == true){
			HasBeenPurchasedEffect();
			return;
		}
		
		Transform canvasTrans = transform.parent.parent.parent;
		print(canvasTrans.name);
		purchaseConfirmBox = canvasTrans.GetComponent<ShopManagement>().purchaseConfirmBox;
		confirmPurchaseMng = purchaseConfirmBox.GetComponent<ConfirmButtonManager>();

		priceText.text = price.ToString();
        priceText.color = new Color(0, 0, 0, 1);

        GetComponent<Button>().onClick.AddListener(() => { ItemSelected();});

	}
	
	void ItemSelected(){
		ShowPurchaseConfirm();
		confirmPurchaseMng.ConfirmItem(this, type, itemName, price);
	}

	void ShowPurchaseConfirm(){
		purchaseConfirmBox.SetActive(true);
	}

	bool CheckIfPurchased(){
		bool isPurchased = false;
		switch(type){
			case Item.TYPE.CHARACTER:
			if(PlayerProgress.playerData.availableCharacters.Contains(itemName)){
				isPurchased = true;
				break;
			}
			break;
			case Item.TYPE.MAP:
			if(PlayerProgress.playerData.availableMaps.Contains(itemName)){
				isPurchased = true;
				break;
			}
			break;
			case Item.TYPE.SHOT:
			if(PlayerProgress.playerData.allAvailableskills.Contains(itemName)){
				isPurchased = true;
				break;
			}
			break;
			case Item.TYPE.VEHICLE:
			if(PlayerProgress.playerData.availableVehicles.Contains(itemName)){
				isPurchased = true;
				break;
			}
			break;

			default:
			break;
		}

		return isPurchased;
	}

	void HasBeenPurchasedEffect(){
        ownedText.SetActive(true);
        GetComponent<RawImage>().color = new Color(1,0.5f,1,0.5f);
	}

	public void SetIsPurchased(){
		GetComponent<Button>().interactable = false;
        priceText.color = new Color(1, 0, 0.35f, 1);
        priceText.text = "SOLD";
        HasBeenPurchasedEffect();
	}


}
