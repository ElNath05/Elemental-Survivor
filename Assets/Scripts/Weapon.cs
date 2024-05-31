using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;    //������ ������
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

    public void Init(float damage, int per, Vector3 dir)    //�ʱ�ȭ �Լ�
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
        if (!collision.CompareTag("Enemy") || per == -1)    //���� ������ �ƴϰų� ���Ѱ���Ÿ���̶�� �̺�Ʈ�߻�X 
            return;

        per--;  //���밡��Ƚ�� -1
        if(per == -1)   //���Ⱑ ���̻� ������ �Ұ����ϴٸ� ��Ȱ��ȭ
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
