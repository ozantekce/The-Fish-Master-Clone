using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Fish : MonoBehaviour
{


    [SerializeField]
    private Fish.FishType type;

    private CircleCollider2D coll;

    private SpriteRenderer rend;

    private float screenLeft;

    private Tweener tweener;

    public FishType Type { get => type; 
        set { 
            
            type = value;
            coll.radius = type.colliderRadius;
            rend.sprite = type.sprite;

        } 
    }


    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }


    public void ResetFish()
    {

        if(tweener != null)
        {
            tweener.Kill(false);
        }


        float num = UnityEngine.Random.Range(type.minLenght,type.maxLenght);

        coll.enabled = true;
        Vector3 position = transform.position;
        position.y = num;
        position.x = screenLeft;
        transform.position = position;

        float num2 = 1;

        float y = UnityEngine.Random.Range((num - num2), (num+num2));
        Vector2 vector = new Vector2(-position.x, y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2*num3);

        tweener = transform.DOMove(vector, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;

        });


        

    }



    public void Hooked()
    {
        coll.enabled = false;
        tweener.Kill(false);

    }



    [Serializable]
    public class FishType
    {
        public int price;
        public float fishCount;
        public float minLenght;
        public float maxLenght;
        public float colliderRadius;
        public Sprite sprite;
    }



}
