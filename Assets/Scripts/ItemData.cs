using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Scriptable Object/ItemData")] //Item�̶�� Ŀ���� �޴��� ����
public class ItemData : ScriptableObject
{
    public enum ItemType {Eball, Fball, Fground, ElectRay, IceSpike, Shoe, Heal}  //������ ������ ���� ����
    [Header("# Main Info")] //�������� �ֿ� �������� �����ϴ� ������
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc; //������ ����
    public Sprite itemIcon;

    [Header("# Level Data")]    //�������� �������� ���� �������� �����ϴ� ������
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]    //������ ������
    public GameObject projectile;
}
