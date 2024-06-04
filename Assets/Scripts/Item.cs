using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public WeaponSpawn weapon;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()   
    {
        switch(data.itemType)   //아이템 타입에 따른 레벨업
        {
            case ItemData.ItemType.Eball:
            case ItemData.ItemType.Fball:
            case ItemData.ItemType.Fground:
                if(level == 0)  //아이템 레벨이 0이면 새 오브젝트를 만들고 무기의 init함수에 아이템 데이터를 넣어 실행
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponSpawn>();
                    weapon.Init(data);

                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
