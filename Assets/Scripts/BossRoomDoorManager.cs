using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoorManager : MonoBehaviour {
    private bool playerIsInRoom;
    public GameObject chestSpawn = null;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject[] doors;
    public GameObject darkPlane;
    private AudioSource audioSource;
    private AudioClip audioClip;
    // Use this for initialization
    void Start()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
        darkPlane.SetActive(true);
        audioSource = transform.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //skeletons = transform.gameObject.FindGameObjectsWithTag("Skeleton");
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player>().hasBossKey)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                }
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.gameObject.tag == "UnityChan")
                    {
                        enemy.GetComponent<UnityChan>().playerIsInRoom = true;
                    }
                }
                darkPlane.SetActive(false);
            }
        }
    }
}