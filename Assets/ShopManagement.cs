using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagement : MonoBehaviour {

	public Button charBtn;
	public Button shellBtn;
	public Button skillBtn;
	public Button itemBtn;

	public GameObject charShop;
	public GameObject shellShop;
	public GameObject skillShop;
	public GameObject itemShop;

	public GameObject purchaseConfirmBox;
	public GameObject saleItemPref;
	// Use this for initialization
	void Start () {
		charBtn.GetComponent<Button>().onClick.AddListener(() => { charBtnClicked();});
		shellBtn.GetComponent<Button>().onClick.AddListener(() => { shellBtnClicked();});
		skillBtn.GetComponent<Button>().onClick.AddListener(() => { skillBtnClicked();});
		itemBtn.GetComponent<Button>().onClick.AddListener(() => { itemBtnClicked();});

		charBtnClicked();
	}

	private void charBtnClicked(){
		charBtn.image.color = new Color(1,1,1,1);
		shellBtn.image.color  = new Color(1,1,1,0.3f);
		skillBtn.image.color = new Color(1,1,1,0.3f);
		itemBtn.image.color = new Color(1,1,1,0.3f);

		charShop.SetActive(true);
		shellShop.SetActive(false);
		skillShop.SetActive(false);
		itemShop.SetActive(false);
	}

	private void shellBtnClicked(){
		charBtn.image.color = new Color(1,1,1,0.3f);
		shellBtn.image.color  = new Color(1,1,1,1);
		skillBtn.image.color = new Color(1,1,1,0.3f);
		itemBtn.image.color = new Color(1,1,1,0.3f);

		charShop.SetActive(false);
		shellShop.SetActive(true);
		skillShop.SetActive(false);
		itemShop.SetActive(false);
	}

	private void skillBtnClicked(){
		charBtn.image.color = new Color(1,1,1,0.3f);
		shellBtn.image.color  = new Color(1,1,1,0.3f);
		skillBtn.image.color = new Color(1,1,1,1);
		itemBtn.image.color = new Color(1,1,1,0.3f);

		charShop.SetActive(false);
		shellShop.SetActive(false);
		skillShop.SetActive(true);
		itemShop.SetActive(false);
	}

	private void itemBtnClicked(){
		itemBtn.image.color = new Color(1,1,1,0.3f);
		shellBtn.image.color  = new Color(1,1,1,0.3f);
		skillBtn.image.color = new Color(1,1,1,0.3f);
		itemBtn.image.color = new Color(1,1,1,1);

		charShop.SetActive(false);
		shellShop.SetActive(false);
		skillShop.SetActive(false);
		itemShop.SetActive(true);
	}
}
