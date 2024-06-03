using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public int id;  //���� id
    public int poolId;  //Ǯ �Ŵ����� �� �ִ� id
    public float damage;    //���� ������
    public int level;   //���� ����   
    public float wSpeed;    //���� �ӵ�

    float wTimer;   //���� �����ֱ� Ÿ�̸�

    PlayerCtrl playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameManager.Instance.player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * wSpeed * Time.deltaTime);   //���⸦ �ð�������� ȸ��
                break;
            default:
                wTimer += Time.deltaTime;

                if(wTimer > wSpeed) //Ÿ�̸Ӱ� �����ð��� ������ �߻��Լ� ����, *���ǵ带 ���ؽð����� ���*
                {
                    wTimer = 0;
                    Fire();
                }
                break;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            LevelUp(10, 5);
        }
    }
    
    public void LevelUp(float damage, int level)    
    {
        this.damage = damage;
        this.level += level;

        if(id == 0)
        {
            SetWeapon();
        }
    }
    public void Init(ItemData data)
    {
        name = "Weapon " + data.itemId; //������Ʈ �̸��� ����
        transform.parent = GameManager.Instance.player.transform;   //������Ʈ�� �θ� �÷��̾�� ����
        transform.localPosition = Vector3.zero; //localposition�� �������� ����

        id = data.itemId;   //������ ���̵�, ������ ���� �����Ϳ� ����Ȱ��� ������
        damage = data.baseDamage;
        level = data.baseCount;

        for(int i = 0; i < GameManager.Instance.pool.prefebs.Length; i++)
        {
            if(data.projectile == GameManager.Instance.pool.prefebs[i])
            {
                poolId = i;
                break;
            }
        }
        switch (id)
        {
            case 0:
                wSpeed = 150;   //������ ȸ���ӵ��� ����
                SetWeapon();
                break;
            default:
                wSpeed = 2; //������ ���� �ð��� ����
                break;
        }
    }

    void SetWeapon()
    {
        for(int i = 0; i < level; i++)
        {
            Transform weapon; 
            if(i < transform.childCount)    //�̹� ������ ���Ⱑ �ڽ����� ������
            {
                weapon = transform.GetChild(i);     //������ ���⸦ ��Ȱ��
            }
            else
            {
                weapon = GameManager.Instance.pool.Get(poolId).transform;   //���� �������� Ǯ���� ������
            }
            
            weapon.parent = transform; //�������� �θ� ���� �����ʷ� ����

            weapon.localPosition = Vector3.zero;    //������ ���� ��ġ �ʱ�ȭ
            weapon.localRotation = Quaternion.identity; //������ ���� �����̼� �ʱ�ȭ

            //������ ���ⰳ����ŭ ������ ����� �������� ��ġ
            Vector3 rotVec = Vector3.forward * 360 * i / level; 
            weapon.Rotate(rotVec);
            weapon.Translate(weapon.up*1.5f, Space.World);

            weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1�� ���� ����
        }
    }

    void Fire()
    {
        if (!playerCtrl.scanner.nearTarget) //����� Ÿ���� ������ �Լ� Ż��
            return;

        Vector3 targetPos = playerCtrl.scanner.nearTarget.position;
        Vector3 dir = targetPos-transform.position; //���� �ִ� ������ ���
        dir = dir.normalized;   //�������� ����� ���͸� ����ȭ

        Transform fBall = GameManager.Instance.pool.Get(poolId).transform;  //Ǯ���� ���� �������� ������
        fBall.position = transform.position;    //���� ��ġ ����
        fBall.rotation = Quaternion.FromToRotation(Vector3.up, dir);    //���⸦ vector.up���� �������� �ٶ󺸴� ������ ���ϰ� �����̼��� ����

        fBall.GetComponent<Weapon>().Init(damage, level, dir);
    }
}
