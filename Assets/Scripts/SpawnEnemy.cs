using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        int i = (int)Mathf.Floor(Random.value * enemies.Length);
        GameObject enemyToSpawn = enemies[i];
        GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, transform.rotation, gameObject.transform.parent);
        this.gameObject.transform.GetComponentInParent<ManageDoor>().enemies.Add(newEnemy);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
