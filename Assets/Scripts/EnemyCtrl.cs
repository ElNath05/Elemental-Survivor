using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 실시간 좌표를 받아냄
        Vector3 pos = transform.position;   //오브젝트의 좌표를 받아냄
        Vector3 moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함

        transform.Translate(moveDir*eSpeed*Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

        if(moveDir.x > 0)
        {

        }
        else if(moveDir.x < 0)
        {

        }
    }
}
