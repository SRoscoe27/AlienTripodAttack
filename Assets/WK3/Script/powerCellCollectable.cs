using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCellCollectable : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate on the Y axis 
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
