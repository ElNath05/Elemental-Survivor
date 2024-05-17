using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    private Vector3 playerPos;  //플레이어 좌표 저장용 변수
    private Vector3 pos;    //오브젝트 본체의 좌표
    private Vector3 moveDir;    //오브젝트가 이동할 벡터
    private bool getPos = false;    //플레이어의 위치를 가져왔는지 확인할 변수

    [HideInInspector] public float eHp = 1;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case 1: //플레이어를 쫓아가는 적
                playerPos = GameManager.Instance.player.transform.position; //플레이어의 실시간 좌표를 받아냄
                pos = transform.position;   //오브젝트의 좌표를 받아냄
                moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함

                transform.Translate(moveDir * eSpeed * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

                if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
                {
                    sprite.flipX = false;
                }
                else if (moveDir.x < 0 && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                break;
            case 2: //플레이어를 매우 느리게 따라오는 적 (플레이어를 둘러싸는 적)
                playerPos = GameManager.Instance.player.transform.position; //플레이어의 실시간 좌표를 받아냄
                pos = transform.position;   //오브젝트의 좌표를 받아냄
                moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함

                transform.Translate(moveDir * (eSpeed/10) * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

                if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
                {
                    sprite.flipX = false;
                }
                else if (moveDir.x < 0 && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                break;
            case 3: //생성된 시점의 플레이어 좌표로 빠르게 날아가는 적
                if (!getPos)    //플레이어의 좌표를 가져오지않았다면 실행
                {
                    getPos = true;  //플레이어의 좌표를 한번만(실시간X) 가져오도록 변수값을 변환
                    playerPos = GameManager.Instance.player.transform.position;
                    pos = transform.position;   //오브젝트의 좌표를 받아냄
                    moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함
                    if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
                    {
                        sprite.flipX = false;
                    }
                    else if (moveDir.x < 0 && !sprite.flipX)
                    {
                        sprite.flipX = true;
                    }
                }

                transform.Translate(moveDir * (eSpeed*2) * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

                break;
        }
    }
}
