using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameManager.Instance.player.GetComponent<PlayerCtrl>();    //플레이어의 스크립트를 가져옴
    }
    void OnTriggerExit2D(Collider2D collision)  //플레이어의 특정범위를 벗어나면 실행
    {
        if (collision.CompareTag("Area"))
        {
            Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어 오브젝트의 위치를 게임매니저를 통해 가져옴
            Vector3 thisPos = transform.position;   //이 오브젝트의 위치값을 가져옴
            float distX = Mathf.Abs(playerPos.x - thisPos.x);   //플레이어와 타일의 거리값을 계산(절대값으로)
            float distY = Mathf.Abs(playerPos.y - thisPos.y);

            Vector3 playerDir = playerCtrl.moveDir; //플레이어의 입력 방향을 받아옴
            float dirX;
            float dirY;
            if(playerDir.x < 0) //플레이어의 입력방향중 x값을 알아냄
            {
                dirX = -1;
            }
            else
            {
                dirX = 1;
            }
            if(playerDir.y < 0) //플레이어 입력방향중 y값을 알아냄
            {
                dirY = -1;
            }
            else
            {
                dirY = 1;
            }

            switch (transform.tag)
            {
                case "Ground":
                    if(distX > distY)   //플레이어와의 거리 중 y축 간의 거리보다 x축간의 거리가 더 멀면 실행
                    {
                        transform.Translate(Vector3.right * dirX * 40); //타일을 플레이어 x축 이동방향쪽으로 재배치
                    }
                    else if (distX < distY) //플레이어와의 거리 중 x축 간의 거리보다 y축간의 거리가 더 멀면 실행
                    {
                        transform.Translate(Vector3.up * dirY * 40); //타일을 플레이어 y축 이동방향쪽으로 재배치
                    }
                    break;

            }
        }
    }
}
