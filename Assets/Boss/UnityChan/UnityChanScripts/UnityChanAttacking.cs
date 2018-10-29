using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAttacking : MonoBehaviour {
    private GameObject player;
    private Animator animator;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        animator = this.GetComponent<Animator>();
        StartCoroutine(damage());
        player.GetComponent<Player>().HP -= 15;
        player.GetComponent<Player>().UpdateHPText();
    }

    // Update is called once per frame
    void Update () {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        //animator.SetFloat("distance", dist);
        if (dist > 2)
        {
            transform.gameObject.AddComponent<UnityChanWalking>();
            Destroy(this);
        }
    }

    IEnumerator damage()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.1f);
            player.GetComponent<Player>().HP -= 15;
            player.GetComponent<Player>().UpdateHPText();
        }
    }
}
