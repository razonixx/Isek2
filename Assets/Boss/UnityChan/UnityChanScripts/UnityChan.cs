using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityChan : MonoBehaviour {
    public Animator animator;
    public GameObject player;
    public bool playerIsInRoom = false;

    public int HP = 300;
    public int attack = 25;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        animator.SetBool("playerIsInRoom", playerIsInRoom);
        animator.SetFloat("distance", 10);
        transform.gameObject.AddComponent<UnityChanIdle>();
    }

    // Update is called once per frame
    void Update () {
        if(HP <= 0)
        {
            player.GetComponent<Player>().score += 1500;
            SceneManager.LoadScene("UC2");
            Destroy(this.gameObject);
        }
        if(!playerIsInRoom)
        {
            if(transform.GetComponent<UnityChanWalking>() != null)
            {
                Destroy(transform.GetComponent<UnityChanWalking>());
            }
            if (transform.GetComponent<UnityChanAttacking>() != null)
            {
                Destroy(transform.GetComponent<UnityChanAttacking>());
            }
            if (transform.GetComponent<UnityChanIdle>() == null)
            {
                transform.gameObject.AddComponent<UnityChanIdle>();
            }
        }
	}
}
