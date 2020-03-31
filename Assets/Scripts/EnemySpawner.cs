using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
	private int currentWave;
	[SerializeField]
	private float baseDifficulty, difficultyMultiplier, timeBetweenEnemies;
	private float thisWaveDifficulty;

	[SerializeField]
	private List<Transform> spawnLocations;
	[SerializeField]
	private List<GameObject> enemyPrefabs;
	private List<Enemy> enemyScripts;

	[SerializeField]
	private TextMeshProUGUI waveNumberText;

	private void Start()
	{
		enemyScripts = new List<Enemy>();

		for (int i = 0; i < enemyPrefabs.Count; i++)
		{
			enemyScripts.Add(enemyPrefabs[i].GetComponent<Enemy>());
		}
	}


	public void SpawnWave() 
	{
		thisWaveDifficulty = baseDifficulty + (baseDifficulty * (difficultyMultiplier - 1f) * currentWave);	//We calculate the current difficulty budget

		Dictionary<int, float> enemyPool = new Dictionary<int, float>();    //These enemies have a minimum difficulty under the current difficulty
		float totalDifficulty = 0f;

		for (int i = 0; i < enemyPrefabs.Count; i++)	//We populate the list with the different creeps
		{
			if(enemyScripts[i].minimumDifficultyToSpawn <= thisWaveDifficulty) 
			{
				float inverseDifficulty = 1f / enemyScripts[i].difficulty;	//We do 1 divided by the difficuty because their chance to spawn should be smaller for more difficult enemies
				enemyPool.Add(i, totalDifficulty + inverseDifficulty);
				totalDifficulty += inverseDifficulty;
			}
		}

		if(enemyPool.Count == 0) 
		{
			Debug.LogWarning("No enemy was eligible for spawning");
			return;
		}

		List<int> enemiesToSpawn = new List<int>(); //These enemies are the ones we spawn afterwards

		while (thisWaveDifficulty > 0f) //We choose new enemies to spawn in this loop, then deduct the difficulty cost from the budget, and run the loop again
		{
			float nextEnemy = Random.Range(0f, totalDifficulty);
			int nextEnemyIndex = 0;

			for (int i = 0; i < enemyPool.Count; i++)
			{
				if(nextEnemy < enemyPool[i]) 
				{
					nextEnemyIndex = i;
					break;
				}
			}

			enemiesToSpawn.Add(nextEnemyIndex);
			thisWaveDifficulty -= enemyScripts[nextEnemyIndex].difficulty;

		}

		for (int i = 0; i < enemiesToSpawn.Count; i++)
		{
			StartCoroutine(SpawnEnemy(timeBetweenEnemies * ((float)i), enemiesToSpawn[i]));
		}

		currentWave++;
		waveNumberText.text = currentWave.ToString();
	}

	IEnumerator SpawnEnemy(float inSeconds, int enemyIndex) 
	{
		yield return new WaitForSeconds(inSeconds);
		Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
		Instantiate(enemyPrefabs[enemyIndex], spawnLocation.position, Quaternion.identity);
	}
}
