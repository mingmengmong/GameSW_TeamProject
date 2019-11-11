using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreateor : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject leftField;
    private GameObject rightField;
    void Start() {
        leftField = this.transform.GetChild(0).gameObject;
        rightField = this.transform.GetChild(1).gameObject;

        Debug.Log(leftField);
        Debug.Log(rightField);

        BoxCollider2D left = leftField.GetComponent<BoxCollider2D>();
        Debug.Log(left.size);
        
        Vector3 m_leftDown = Camera.main.ScreenToWorldPoint( new Vector3( 0,0,0) );
        Vector3 m_rightUpper = Camera.main.ScreenToWorldPoint( new Vector3( Screen.width, Screen.height,0 ) );
        Debug.Log(m_leftDown);
        Debug.Log(m_rightUpper);
    }
}
