using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public WeaponSpawn weapon;
    public Equipments equip;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; //자식으로부터 이미지를 가져옴
        icon.sprite = data.itemIcon;    //데이터로부터 아이템 아이콘 스프라이트를 가져옴

        Text[] texts = GetComponentsInChildren<Text>(); //자식으로부터 텍스트를 받아옴
        textLevel = texts[0];   //텍스트 초기화
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
        textDesc.text = data.itemDesc;
    }

    private void LateUpdate()   
    {
        textLevel.text = "Lv." + (level + 1); //레벨 텍스트 갱신
    }

    public void OnClick()   
    {
        switch(data.itemType)   //아이템 타입에 따른 레벨업
        {
            case ItemData.ItemType.Eball:
            case ItemData.ItemType.Fball:
            case ItemData.ItemType.Fground:
            case ItemData.ItemType.ElectRay:
            case ItemData.ItemType.IceSpike:
                if(level == 0)  //아이템 레벨이 0이면 새 오브젝트를 만들고 무기의 init함수에 아이템 데이터를 넣어 실행
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponSpawn>();
                    weapon.Init(data);

                }
                else //레벨업 하면 데이터에 저장된 값들을 레벨에 따라 가져옴
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);  //다음 레벨업시의 데미지와 카운트값을 가져옴
                }

                level++;
                break;
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    GameObject newEquip = new GameObject();
                    equip = newEquip.AddComponent<Equipments>();
                    equip.Init(data);
                }
                else
                {
                    float nextDamage = data.damages[level];
                    equip.LevelUp(nextDamage);
                }

                level++;
                break;

            case ItemData.ItemType.Heal:    //체력회복
                GameManager.Instance.playerCtrl.pHp += 20;
                if(GameManager.Instance.playerCtrl.pHp > GameManager.Instance.playerCtrl.maxHp) //회복량이 최대체력을 초과하면
                {
                    GameManager.Instance.playerCtrl.pHp = GameManager.Instance.playerCtrl.maxHp; //현재 체력을 최대체력으로
                }
                break;
        }

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
