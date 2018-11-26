using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDoor : MonoBehaviour {
    private bool playerIsInRoom;
    public GameObject chestSpawn = null;
    public List<GameObject> enemies = new List<GameObject>();

    public List<GameObject> skeletons = new List<GameObject>();
    public GameObject[] doors;
    public GameObject darkPlane;
    private AudioSource audioSource;
    private AudioClip audioClip;
    // Use this for initialization
    void Start () {
        foreach(GameObject door in doors)
        {
            door.SetActive(false);
        }
        darkPlane.SetActive(true);
        audioSource = transform.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update () {
        if (enemies.Count == 0)
        {
            if(chestSpawn != null)
            {
                chestSpawn.SetActive(true);
            }
            audioSource.Play();
            foreach (GameObject door in doors)
            {
                if (door.activeSelf)
                {
                    door.SetActive(false);
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        //skeletons = transform.gameObject.FindGameObjectsWithTag("Skeleton");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().currentRoom = this.gameObject;
            foreach (GameObject enemy in enemies)
            {
                if (enemy.gameObject.tag == "Skeleton")
                {
                    skeletons.Add(enemy);
                }
            }
            foreach (GameObject skeleton in skeletons)
            {
                if (skeleton.transform.IsChildOf(this.transform))
                {
                    skeleton.GetComponent<Skeleton>().playerIsInRoom = true;
                }
            }
            //Debug.Log("Player Entered Room");
            foreach(GameObject door in doors)
            {
                door.SetActive(true);
            }
            darkPlane.SetActive(false);
        }
    }
}
