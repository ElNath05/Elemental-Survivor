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
        playerCtrl = GameManager.Instance.player.GetComponent<PlayerCtrl>();    //�÷��̾��� ��ũ��Ʈ�� ������
    }
    void OnTriggerExit2D(Collider2D collision)  //�÷��̾��� Ư�������� ����� ����
    {
        if (collision.CompareTag("Area"))
        {
            Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾� ������Ʈ�� ��ġ�� ���ӸŴ����� ���� ������
            Vector3 thisPos = transform.position;   //�� ������Ʈ�� ��ġ���� ������
            float distX = Mathf.Abs(playerPos.x - thisPos.x);   //�÷��̾�� Ÿ���� �Ÿ����� ���(���밪����)
            float distY = Mathf.Abs(playerPos.y - thisPos.y);

            Vector3 playerDir = playerCtrl.moveDir; //�÷��̾��� �Է� ������ �޾ƿ�
            float dirX;
            float dirY;
            if(playerDir.x < 0) //�÷��̾��� �Է¹����� x���� �˾Ƴ�
            {
                dirX = -1;
            }
            else
            {
                dirX = 1;
            }
            if(playerDir.y < 0) //�÷��̾� �Է¹����� y���� �˾Ƴ�
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
                    if(distX > distY)   //�÷��̾���� �Ÿ� �� y�� ���� �Ÿ����� x�ణ�� �Ÿ��� �� �ָ� ����
                    {
                        transform.Translate(Vector3.right * dirX * 40); //Ÿ���� �÷��̾� x�� �̵����������� ���ġ
                    }
                    else if (distX < distY) //�÷��̾���� �Ÿ� �� x�� ���� �Ÿ����� y�ణ�� �Ÿ��� �� �ָ� ����
                    {
                        transform.Translate(Vector3.up * dirY * 40); //Ÿ���� �÷��̾� y�� �̵����������� ���ġ
                    }
                    break;

            }
        }
    }
}
