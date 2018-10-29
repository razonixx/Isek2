using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanIdle : MonoBehaviour {
    private GameObject player;
    private Animator animator;
    private UnityChan uc;
    // Use this for initialization
    void Start () {
        uc = GetComponentInChildren<UnityChan>();
        animator = uc.animator;
    }

    // Update is called once per frame
    void Update () {
        if (uc.playerIsInRoom)
        {
            animator.SetBool("playerIsInRoom", uc.playerIsInRoom);
            transform.gameObject.AddComponent<UnityChanWalking>();
            Destroy(this);
        }
        else
        {
        }
    }

    IEnumerator UnFreeze()
    {
        yield return new WaitForSeconds(1);
        uc.playerIsInRoom = true;
    }
}
