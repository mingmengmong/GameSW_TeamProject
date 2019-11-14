using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Moving : MonoBehaviour
{
    // 이미지 스프라이트 리스트
    public Sprite[] move;
    // 이미지 순서 별 인덱스 리스트 ( 해당 인덱스는 스프라이트 리스트 인덱스를 나타낸다. )
    public int[] moveOrderSpriteArr;
    // 이미지 순서 별 시간 ( ms )
    public int[] moveOrderTimeArr;
    // 첫 이미지 랜덤 표기 여
    public bool isRandom = false;

    private int moveOrderIndex = 0;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (isRandom) {
            moveOrderIndex = Random.Range(0, moveOrderSpriteArr.Length);
        }
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime*1000;
        if (moveOrderTimeArr[moveOrderIndex] <= time) {
            time = 0;
            moveOrderIndex++;
            moveOrderIndex %= moveOrderSpriteArr.Length;
            
            GetComponent<SpriteRenderer>().sprite = move[moveOrderSpriteArr[moveOrderIndex]];
        }
        
        
        
        /*
        moveOrderIndex ++;
        if (moveOrderIndex >= move.Length)
        {
            moveOrderIndex = 0;
        }
        GetComponent<SpriteRenderer>().sprite = move[moveOrderIndex];
        */
    }
}
