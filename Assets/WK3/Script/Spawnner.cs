using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public float spawnTime=10; //Spawn Time, change for later
    public GameObject zombie; //zombie prefab
    public int zombieCount =  0;
    // Start is called before the first frame update
    void Start()
    {
        //Start the spawn update
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn(){
        //Wait spawnTime
        yield return new WaitForSeconds(spawnTime);
        //Spawn prefab add random position
        GameObject go = Instantiate(zombie,transform.position, Quaternion.identity) as GameObject;
        zombieCount++;
        Debug.Log(zombieCount);
        // to do: needs to reduce zombie count if they die
        StartCoroutine("Spawn");
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
