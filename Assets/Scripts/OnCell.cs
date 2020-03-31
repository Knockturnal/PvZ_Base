using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCell : MonoBehaviour
{
	private GameObject currentPlant;
	private void OnMouseDown()
	{
		if (currentPlant == null)
		{
			if (BuildController.control.currentPlant > -1)
			{
				currentPlant = Instantiate(BuildController.control.PlantPressed(), transform.position, Quaternion.identity);
				BuildController.control.MenuPlantButtonPressed(-1);	//This equates to pressing the cancel button, we "simulate" that we cancel building
			}
		}
	}
}
