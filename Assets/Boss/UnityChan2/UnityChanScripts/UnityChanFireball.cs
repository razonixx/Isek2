using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanFireball : MonoBehaviour
{
    public GameObject bullet;
    private Player playerScript;
    private GameObject player;
    public bool fire = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        StartCoroutine(ShootFireball());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ShootFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.3f);
            Vector3 pos = player.transform.position - this.transform.position;
            if(fire)
            {
                GameObject bulletInst = Instantiate<GameObject>(bullet, transform.position, transform.rotation);
                bulletInst.transform.LookAt(player.transform);
                Destroy(bulletInst, 4);
            }
        }
    }
}
