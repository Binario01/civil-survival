using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnerManager : MonoBehaviour {

	public RockSpawner rockSpawner;
	public GameObject[] rocks;

	private int[] spawnerFreq;
	private float timeToSpawn = 1.5f;
	private float spawnCounter = 0f;

	private Camera mainCamera;
	private List<RockSpawner> spawners = new List<RockSpawner>();
	private Vector3 topLeft,topRight;
	private int numberOfBlocks = 6;
	private float blockLength;



	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0,Screen.height,0));
		topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));

		float midPos = (topRight.x + topLeft.x) / 2;
		blockLength = (topRight.x - midPos) / numberOfBlocks;

		spawners.Add(GameObject.Instantiate(rockSpawner,new Vector3(midPos,topLeft.y,topLeft.z),Quaternion.identity));
		Vector3 spawnPosLeft, spawnPosRight;

		for(int i = 1 ; i < numberOfBlocks ;i++){
			spawnPosLeft = new Vector3(midPos - i*blockLength,topLeft.y,topLeft.z);
			spawnPosRight = new Vector3(midPos + i*blockLength,topLeft.y,topLeft.z);
			spawners.Add(GameObject.Instantiate(rockSpawner,spawnPosLeft,Quaternion.identity));
			spawners.Add(GameObject.Instantiate(rockSpawner,spawnPosRight,Quaternion.identity));
		}

		spawnerFreq = new int[spawners.Count];

	}

	// Update is called once per frame
	void Update () {
		if(!GameManager.Instance.gameEnded){
			spawnCounter += Time.deltaTime;
			if(spawnCounter > timeToSpawn){
				spawnCounter -= timeToSpawn;
				int index = getIndex();
				// Spawn
				spawners[index].Spawn(rocks[Random.Range(0,3)]);
			}
		}
	}

	int getIndex(){
		int index = Random.Range(0,spawners.Count);
		spawnerFreq[index]++;
		return index;
	}

}
