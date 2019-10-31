using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    private Vector3 rotate;
    
    // Start is called before the first frame update
    void Start()
    {
        rotate = 60f * Time.deltaTime * Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(rotate);
    }
}
