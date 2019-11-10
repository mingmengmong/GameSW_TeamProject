using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Moving : MonoBehaviour
{
    public Sprite[] move;
    int animIndex;

    // Start is called before the first frame update
    void Start()
    {
        animIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animIndex ++;
        if (animIndex >= move.Length)
        {
            animIndex = 0;
        }
        GetComponent<SpriteRenderer>().sprite = move[animIndex];
    }
}
