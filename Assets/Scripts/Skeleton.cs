using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Arreglar la animacion de ataque

public class Skeleton : MonoBehaviour {
    // Use this for initialization
    public int HP;
    public Camera cam;
    public bool playerIsInRoom;
    public bool poisoned;
    public AudioClip onHitSound;
    private AudioSource audioSource;
    private bool isAttacking;
    public  Animator animator;
    private int dot;
	void Start () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = onHitSound;
        dot = 0;
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(GameObject.Find("Player").transform);
        if(playerIsInRoom)
        {
            transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }
        if (HP <= 0)
        {
            this.gameObject.transform.parent.GetComponentInParent<ManageDoor>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if(isAttacking)
        {
            StopAllCoroutines();
            isAttacking = false;
        }
        if (poisoned)
        {
            StartCoroutine(DOT());
        }
    }

    private bool IsInView(GameObject origin, GameObject toCheck)
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, toCheck.transform.position, out hit))
        {
            if (hit.transform.tag == toCheck.tag)
            {
                //Debug.Log("Imp ray collisioned with: Player");
                return true;
            }
            //Debug.Log("Imp ray collisioned with: " + hit.transform.gameObject.name);
            return false;
        }
        return true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tear")
        {
            audioSource.Play();
        }
        if (other.gameObject.tag == "Player" && !isAttacking)
        {
            StartCoroutine(attack());
            GameObject.Find("Player").GetComponent<Player>().HP -= 15;
            GameObject.Find("Player").GetComponent<Player>().UpdateHPText();
        }
    }
    IEnumerator attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(.3f);
            animator.SetTrigger("IsNearPlayer");
            isAttacking = true;
        }
    }
    IEnumerator DOT()
    {
        while (poisoned)
        {
            HP -= 1;
            dot += 1;
            if (!(dot <= 15))
            {
                poisoned = false;
                dot = 0;
            }
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
