using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    public GameObject chest;

    // Use this for initialization
    void Start()
    {
        GameObject newChest = Instantiate(chest, this.transform.position, this.transform.rotation, gameObject.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
