using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Scriptable Object/ItemData")] //Item이라는 커스텀 메뉴를 생성
public class ItemData : ScriptableObject
{
    public enum ItemType {Eball, Fball, Fground, ElectRay, IceSpike, Shoe, Heal}  //아이템 종류를 각각 저장
    [Header("# Main Info")] //아이템의 주요 정보들을 저장하는 변수들
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc; //아이템 설명
    public Sprite itemIcon;

    [Header("# Level Data")]    //아이템의 레벨업에 따른 정보들을 저장하는 변수들
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]    //가져올 프리펩
    public GameObject projectile;
}
