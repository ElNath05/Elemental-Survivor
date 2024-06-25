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

    void ApplyEquip()   //장비관련함수들 호출용 함수
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
        name = "Weapon " + data.itemId; //오브젝트 이름을 변경
        transform.parent = GameManager.Instance.player.transform;   //오브젝트의 부모를 플레이어로 지정
        transform.localPosition = Vector3.zero; //localposition을 원점으로 변경

        type = data.itemType; //아이템의 타입을 가져옴
        damage = data.damages[0]; //아이템의 데미지(효과 정도)를 가져옴
        ApplyEquip();
    }

    public void LevelUp(float damage) //레벨업 후의 데미지로 값을 바꿈
    {
        this.damage = damage;
    }

    void SpeedUp()
    {
        float speed =4;
        GameManager.Instance.playerCtrl.pSpeed = speed + speed*damage;   //장비의 비율만큼 플레이어 속도를 증가시킴
    }

}
