using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wait02 = idle/thro fireball
//lose00 = summon adds

public class UnityChan2 : MonoBehaviour {
    private Animator animator;
    private GameObject player;
    public GameObject magic;
    public bool playerIsInRoom = false;
    public GameObject[] enemies;

    public bool fire;
    public bool summon;
    public int HP = 300;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player");
        RuntimeAnimatorController newController = (RuntimeAnimatorController)Resources.Load("UnityChan2/SummonAction");
        StartCoroutine(Cycle());
        fire = true;
        summon = false;
    }

    // Update is called once per frame
    void Update () {
        this.transform.LookAt(player.transform);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("SummonAction", typeof(RuntimeAnimatorController));
            magic.GetComponent<UnityChanFireball>().fire = false;
            StartCoroutine(Summon());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("FireballAction", typeof(RuntimeAnimatorController));
            magic.GetComponent<UnityChanFireball>().fire = true;
            StopCoroutine(Summon());
        }

        if (HP <= 0)
        {
            player.GetComponent<Player>().score += 5000;
            Destroy(this.gameObject);
        }
	}

    IEnumerator Cycle()
    {
        while (true)
        {
            if(fire)
            {
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("FireballAction", typeof(RuntimeAnimatorController));
                magic.GetComponent<UnityChanFireball>().fire = true;
                StopCoroutine(Summon());
                fire = false;
                summon = true;
                Debug.Log("FIRE");
            }
            else if(summon)
            {
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("SummonAction", typeof(RuntimeAnimatorController));
                magic.GetComponent<UnityChanFireball>().fire = false;
                StartCoroutine(Summon());
                fire = true;
                summon = false;
                Debug.Log("SUMMON");
            }
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator Summon()
    {
        while (true)
        {
            int i = (int)Mathf.Floor(Random.value * enemies.Length);
            GameObject enemyToSpawn = enemies[i];
            GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, transform.rotation, gameObject.transform.parent);
            newEnemy.transform.LookAt(player.transform);
            yield return new WaitForSeconds(4.05f);
        }
    }
}
