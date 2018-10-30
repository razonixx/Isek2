using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour {
    public GameObject bullet;
    public int HP;
    public int attackDelay;
    private bool isShooting;
    public bool poisoned;
    private int dot;
    // Use this for initialization
    void Start () {
        isShooting = false;
        //StartCoroutine(ShootFireball());
    }

    // Update is called once per frame
    void Update () {
        if (HP <= 0)
        {
            this.gameObject.transform.parent.GetComponentInParent<ManageDoor>().enemies.Remove(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }
        if(!isShooting && IsInView(transform.gameObject, GameObject.Find("Player")))
        {
            StartCoroutine(ShootFireball());
        }
        else if(isShooting)
        {
            StopAllCoroutines();
            isShooting = false;
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
    
    IEnumerator ShootFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(.01f * attackDelay);
            Vector3 pos = GameObject.Find("Player").transform.position - this.transform.position;
            //GameObject bulletInst = Instantiate<GameObject>(bullet, transform.position + Vector3.right, Quaternion.LookRotation(pos - transform.position));
            GameObject bulletInst = Instantiate<GameObject>(bullet, transform.position, transform.rotation);
            bulletInst.transform.LookAt(GameObject.Find("Player").transform);
            Destroy(bulletInst, 3);
            isShooting = true;
        }
    }

    IEnumerator DOT()
    {
        while (poisoned)
        {
            yield return new WaitForSecondsRealtime(5f);
            HP -= 1;
            dot += 1;
            if (!(dot <= 20))
            {
                poisoned = false;
                dot = 0;
            }

        }

    }
}
