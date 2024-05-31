using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;    //무기의 데미지
    public int per;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Erase();
    }

    public void Init(float damage, int per, Vector3 dir)    //초기화 함수
    {
        this.damage = damage; 
        this.per = per;

        if(per > -1)
        {
            rb.velocity = dir * 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)    //적과 닿은게 아니거나 무한관통타입이라면 이벤트발생X 
            return;

        per--;  //관통가능횟수 -1
        if(per == -1)   //무기가 더이상 관통이 불가능하다면 비활성화
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void Erase()
    {
        Transform target = GameManager.Instance.player.transform;
        Vector3 targetPos = target.position;
        float dist = Vector3.Distance(targetPos, transform.position);
        if (dist > 20f)
            this.gameObject.SetActive(false);
    }
}
