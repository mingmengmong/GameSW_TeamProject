using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    
    
    public bool isMoving = false;
    public float movingSpeed = 3.5f;
    public float movingTime = 800;
    public float restTime = 800;
    private float mTime = 0f;
    // false : 휴식, true : 이동
    private bool mBool = false;
    
    private Vector2 direction;
    private bool isLeft = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isRandom) {
            moveOrderIndex = Random.Range(0, moveOrderSpriteArr.Length);
        }

        if (isMoving)
        {
            direction = getDirection();
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

        
        
        
        // 이동 처리
        if (isMoving)
        {
            mTime += Time.deltaTime * 1000;
            if (mBool)
            {
                if (mTime > movingTime)
                {
                    mTime = 0;
                    mBool = false;
                }
                else
                {
                    Vector2 objPosition = this.gameObject.transform.position;
                    if (objPosition.x > StaticData.OBJECT_X_POS_MAX)
                    {
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        isLeft = true;
                    }

                    if (objPosition.x < StaticData.OBJECT_X_POS_MIN)
                    {
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        isLeft = false;
                    }

                    if (objPosition.y > StaticData.OBJECT_Y_POS_MAX)
                    {
//                        this.gameObject.transform.transform.Translate(Vector2.down * Time.deltaTime * 5);
                        direction.y = -1 * Math.Abs(direction.y);
                    }

                    if (objPosition.y < StaticData.OBJECT_Y_POS_MIN)
                    {
//                        this.gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5);
                        direction.y = Math.Abs(direction.y);
                    }

                    if (isLeft)
                    {
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    else
                    {
                        this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    this.gameObject.transform.Translate(direction * Time.deltaTime * movingSpeed);
                }
            }
            else
            {
                if (mTime > restTime)
                {
                    mTime = 0;
                    mBool = true;
                    direction = getDirection();
                }
            }
        }

        
    }

    private Vector2 getDirection() {
        Vector3 nowPosition = this.gameObject.transform.position;
        
        float x = Random.Range(-1f, 1f);
        if (x <= 0)
        {
            isLeft = true;
        }
        else
        {
            x *= -1;
            isLeft = false;
        }

        float y = Random.Range(-1f, 1f);
        return new Vector2(x,y).normalized;
    }
}
