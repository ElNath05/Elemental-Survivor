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
        icon = GetComponentsInChildren<Image>()[1]; //�ڽ����κ��� �̹����� ������
        icon.sprite = data.itemIcon;    //�����ͷκ��� ������ ������ ��������Ʈ�� ������

        Text[] texts = GetComponentsInChildren<Text>(); //�ڽ����κ��� �ؽ�Ʈ�� �޾ƿ�
        textLevel = texts[0];   //�ؽ�Ʈ �ʱ�ȭ
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
        textDesc.text = data.itemDesc;
    }

    private void LateUpdate()   
    {
        textLevel.text = "Lv." + (level + 1); //���� �ؽ�Ʈ ����
    }

    public void OnClick()   
    {
        switch(data.itemType)   //������ Ÿ�Կ� ���� ������
        {
            case ItemData.ItemType.Eball:
            case ItemData.ItemType.Fball:
            case ItemData.ItemType.Fground:
            case ItemData.ItemType.ElectRay:
            case ItemData.ItemType.IceSpike:
                if(level == 0)  //������ ������ 0�̸� �� ������Ʈ�� ����� ������ init�Լ��� ������ �����͸� �־� ����
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponSpawn>();
                    weapon.Init(data);

                }
                else //������ �ϸ� �����Ϳ� ����� ������ ������ ���� ������
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);  //���� ���������� �������� ī��Ʈ���� ������
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

            case ItemData.ItemType.Heal:    //ü��ȸ��
                GameManager.Instance.playerCtrl.pHp += 20;
                if(GameManager.Instance.playerCtrl.pHp > GameManager.Instance.playerCtrl.maxHp) //ȸ������ �ִ�ü���� �ʰ��ϸ�
                {
                    GameManager.Instance.playerCtrl.pHp = GameManager.Instance.playerCtrl.maxHp; //���� ü���� �ִ�ü������
                }
                break;
        }

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
