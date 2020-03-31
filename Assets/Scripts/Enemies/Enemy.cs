using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float health, difficulty, minimumDifficultyToSpawn;
	[SerializeField]
	private int cashOnKill;

	private void Update()
	{
		transform.position += Vector3.left * Time.deltaTime;
	}

	private void Kill()	//Isn't called from anywhere but shows how you could get money when the enemy dies
	{
		BuildController.control.AddMoney(cashOnKill);
		Destroy(gameObject);
	}
}
