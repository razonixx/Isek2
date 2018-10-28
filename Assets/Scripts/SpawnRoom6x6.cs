using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom6x6 : MonoBehaviour {
    public GameObject[] rooms;
    public int rotationNeeded;
	// Use this for initialization
	void Start () {
        int i = (int)Mathf.Floor(Random.value * rooms.Length);
        GameObject roomToSpawn = rooms[i];
        Vector3 rotation = transform.rotation.eulerAngles;
        GameObject newRoom = Instantiate(roomToSpawn, transform.position, Quaternion.Euler(new Vector3(rotation.x, rotationNeeded + this.transform.parent.rotation.eulerAngles.y, rotation.z)));
    }

    // Update is called once per frame
    void Update () {
	}
}
