using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour, IHittable
{
    [SerializeField] float moveSpeed;
    [SerializeField] private int xPos;
    [SerializeField] private int yPos;
    [SerializeField] Animator animator;
 
    private Vector2 basePos;
    private bool isMoving;

    private enum MoveDir { Up = 1, Left, Down, Right } 
    
    private void Awake()
    {
        basePos = GameManager.Data.map[xPos, yPos];
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Player.PlayerPos = basePos;
        gameObject.transform.position = GameManager.player.PlayerPos;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isMoving)
            return;

        Vector2 startPos = GameManager.Player.PlayerPos;
        Vector2 endPos;

        if(Input.GetKeyDown(KeyCode.UpArrow) && yPos > 0)
        {
            yPos--;
            if (!GameManager.data.tileMap[xPos, yPos])
            {
                yPos++;
                return;
            }
            animator.SetInteger("Move", (int)MoveDir.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && yPos < 3)
        {
            yPos++;
            if (!GameManager.data.tileMap[xPos, yPos])
            {
                yPos--;
                return;
            }
            animator.SetInteger("Move", (int)MoveDir.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && xPos < 3)
        {
            xPos++;
            if (!GameManager.data.tileMap[xPos, yPos])
            {
                xPos--;
                return;
            }
            animator.SetInteger("Move", (int)MoveDir.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && xPos > 0)
        {
            xPos--;
            if (!GameManager.data.tileMap[xPos, yPos])
            {
                xPos++;
                return;
            }
            animator.SetInteger("Move", (int)MoveDir.Left);
        }
        else return;

        GameManager.player.PlayerPos = GameManager.Data.map[xPos, yPos];
        GameManager.Data.playerXPos = xPos;
        GameManager.Data.playerYPos = yPos;
        endPos = GameManager.player.PlayerPos;
        StartCoroutine(MoveCorutine(startPos, endPos));
    }

    IEnumerator MoveCorutine(Vector2 startPos, Vector2 endPos)
    {
        isMoving = true;
        float totalTime = Vector2.Distance(startPos, endPos) / moveSpeed;
        float rate = 0;
        while (rate < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, rate);
            rate += Time.deltaTime / totalTime;
            yield return null;
        }
        isMoving = false;
        animator.SetInteger("Move", 0);
    }

    public void Hit(int damage)
    {
        GameManager.Player.Hit(damage);
        animator.SetTrigger("Hit");
        if(GameManager.Player.CurHp < 0)
        {
            animator.SetBool("Die", true);
        }
    }
}
