using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmButtonManager : MonoBehaviour {
	public Button okBtn;
	public Button cancelBtn;
	public Transform purchaseBG;
	public Transform mainBox;
	public Transform subBox;
	public Button subBoxOkBtn;
	public Text purchaseNotif;
    public Transform GoldObject;
	private Item.TYPE itemTypeToConfirm;
	private int price;
	private ItemForSale selectedItem;
	private string itemNameToConfirm;
	// Use this for initialization
	void Start () {
        GoldObject.GetComponentInChildren<Text>().text = PlayerProgress.playerData.gold.ToString();


        subBox.gameObject.SetActive(false);
		CancelPurchase();

		okBtn.onClick.AddListener(() => { AcceptPurchase();});
		cancelBtn.onClick.AddListener(() => { CancelPurchase();});
		subBoxOkBtn.onClick.AddListener(() => { CancelPurchase();});
	}

	public void ConfirmItem(ItemForSale item, Item.TYPE itemType, string iTemName, int iTemPrice){
		itemTypeToConfirm = itemType;
		itemNameToConfirm = iTemName;
		price = iTemPrice;
		selectedItem = item;
	}

	void AcceptPurchase(){
		
		if(PlayerProgress.playerData.gold < price){
			purchaseNotif.text = "Not enough Gold.";
			mainBox.gameObject.SetActive(false);
			subBox.gameObject.SetActive(true);
			return;
		}

		PlayerProgress.playerData.gold -= price;
        GoldObject.GetComponentInChildren<Text>().text = PlayerProgress.playerData.gold.ToString();

        switch (itemTypeToConfirm){
			case Item.TYPE.CHARACTER:
				PlayerProgress.playerData.availableCharacters.Add(itemNameToConfirm);
				break;
			case Item.TYPE.MAP:
				PlayerProgress.playerData.availableMaps.Add(itemNameToConfirm);
				break;
			case Item.TYPE.SHOT:
				CheckShotPurchased();
				break;
			case Item.TYPE.VEHICLE:
				PlayerProgress.playerData.availableVehicles.Add(itemNameToConfirm);
				break;

			default:
			break;
		}

		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
		purchaseNotif.text = "Item purchase successful!";
		selectedItem.SetIsPurchased();
		subBox.gameObject.SetActive(true);
		mainBox.gameObject.SetActive(false);
	}

	void CancelPurchase(){
		mainBox.gameObject.SetActive(true);
		subBox.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}

	void CheckShotPurchased(){
		if(CommonData.PippoSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.pippo_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.JohnnySkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.johnny_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.MathialSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.mathial_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.MajaSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.maja_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.VieSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.vie_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.LynuSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.lynu_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.BapeSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.bape_availableskills.Add(itemNameToConfirm);

		}else if(CommonData.KolavSkills.Contains( itemNameToConfirm)){
			PlayerProgress.playerData.kolav_availableskills.Add(itemNameToConfirm);

		}
	}
}
