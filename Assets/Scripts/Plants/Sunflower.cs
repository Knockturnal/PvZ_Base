using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
	[SerializeField]
	private int givesThisMuchCash;
	[SerializeField]
	private float givesCashThisOften;
	[SerializeField]
	private GameObject spawnParticles;

	private void OnEnable()
	{
		InvokeRepeating("GiveCash", givesCashThisOften, givesCashThisOften);
	}

	private void GiveCash() 
	{
		BuildController.control.AddMoney(givesThisMuchCash);
		GameObject newParticles = Instantiate(spawnParticles, transform.position + Vector3.back, Quaternion.identity);
		Destroy(newParticles, 3f);
	}
}
