using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public enum InfoType {Exp, Level, Health}
    public InfoType type;

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp;
                float maxExp = 2*(GameManager.Instance.level+2);
                slider.value = curExp/maxExp;
                break;
            case InfoType.Health:

                break;
        }
    }
}
