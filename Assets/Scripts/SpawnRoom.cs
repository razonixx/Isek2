using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour {
    public GameObject[] rooms;
    public int rotationNeeded = 1;
    private bool checarColision;

    private GameObject newRoom;
    private GameObject[] walls;

    // Use this for initialization
    void Start () {
        if (rotationNeeded == 1)
        {
            rotationNeeded = (int)this.transform.parent.rotation.eulerAngles.y;
        }
        int i = (int)Mathf.Floor(Random.value * rooms.Length);
        GameObject roomToSpawn = rooms[i];
        Vector3 rotation = transform.rotation.eulerAngles;
        newRoom = Instantiate(roomToSpawn, transform.position, Quaternion.Euler(new Vector3(rotation.x, rotationNeeded, rotation.z)));
        checarColision = false;
    }

    // Update is called once per frame
    void Update () {
	}
}
