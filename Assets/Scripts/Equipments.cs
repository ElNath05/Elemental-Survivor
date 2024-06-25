using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public ItemData.ItemType type;
    public float damage;

    PlayerCtrl playerCtrl;

    private void Start()
    {
        playerCtrl = GameManager.Instance.player.GetComponent<PlayerCtrl>();
    }

    void ApplyEquip()   //�������Լ��� ȣ��� �Լ�
    {
        switch (type)
        {
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    public void Init(ItemData data)
    {
        name = "Weapon " + data.itemId; //������Ʈ �̸��� ����
        transform.parent = GameManager.Instance.player.transform;   //������Ʈ�� �θ� �÷��̾�� ����
        transform.localPosition = Vector3.zero; //localposition�� �������� ����

        type = data.itemType; //�������� Ÿ���� ������
        damage = data.damages[0]; //�������� ������(ȿ�� ����)�� ������
        ApplyEquip();
    }

    public void LevelUp(float damage) //������ ���� �������� ���� �ٲ�
    {
        this.damage = damage;
    }

    void SpeedUp()
    {
        float speed =4;
        GameManager.Instance.playerCtrl.pSpeed = speed + speed*damage;   //����� ������ŭ �÷��̾� �ӵ��� ������Ŵ
    }

}
