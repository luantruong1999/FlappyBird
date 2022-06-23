using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float delayTime;
    private float lastJumpTime;
    private bool isStart;
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BirdInStart();
    }

    private void BirdInStart()
    {
        rigidbody2D.isKinematic = true;
        transform.DOMove(new Vector3(0, 0.5f, 0), 0.5f).SetLoops(-1,LoopType.Yoyo);
    }

    private void BirdInGame()
    {
        rigidbody2D.isKinematic = false;
    }

    private void Jump()
    {
        rigidbody2D.velocity = Vector2.up * jumpForce;
        DOTween.KillAll();
        DOTween.To(() => transform.rotation, x => transform.rotation = x, new Vector3(0, 0, 40), 0.1f).OnComplete(() =>
        {
            DOTween.To(() => transform.rotation, x => transform.rotation = x, new Vector3(0, 0, -90), 2f);
            
        });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isStart)
            {
                isStart = true;
                BirdInGame();
                GameManager.instance.StartGame();
            }
            if(Time.time-lastJumpTime<delayTime) return;
            lastJumpTime = Time.time;
            Jump();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameManager.instance.AddScore();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Death"))
        {
            GameManager.instance.GameOver();
        }
    }
}
