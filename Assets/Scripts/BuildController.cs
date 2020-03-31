using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildController : MonoBehaviour
{
	public static BuildController control;

	[SerializeField]
	private List<GameObject> plants, plantButtons;  //Button has the same index as the plant it corresponds to
	[HideInInspector]
	public int currentPlant = -1;
	[SerializeField]
	private GameObject cancelButton;

	[SerializeField]
	private TextMeshProUGUI moneyText;
	private int currentMoney = 50;

	private void Awake()
	{
		control = this;
		UpdateUIBasedOnMoney();
	}

	public void MenuPlantButtonPressed(int plantID)
	{
		currentPlant = plantID;
		if (plantID > -1)
		{
			cancelButton.SetActive(true);
		}
		else
		{
			cancelButton.SetActive(false);
		}
	}

	public GameObject PlantPressed()
	{
		currentMoney -= plants[currentPlant].GetComponent<Plant>().cost;
		UpdateUIBasedOnMoney();
		return plants[currentPlant];
	}

	void UpdateUIBasedOnMoney()	//I would reccommend to cache some of the references to components here and not use so much GetComponent() as it is not very performant
	{
		moneyText.text = currentMoney.ToString();

		for (int i = 0; i < plants.Count; i++)
		{
			if (currentMoney < plants[i].GetComponent<Plant>().cost)
			{
				plantButtons[i].GetComponent<Button>().interactable = false;
			}
			else
			{
				plantButtons[i].GetComponent<Button>().interactable = true;
			}
		}
	}

	public void AddMoney(int amount) 
	{
		currentMoney += amount;
		UpdateUIBasedOnMoney();
	}
}
