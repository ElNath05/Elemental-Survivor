using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //Ž������
    public LayerMask targetLayer;    //��ǥ����� ���̾�
    public LayerMask targetLayer2;
    public RaycastHit2D[] targets;  //������ ���� ������ ��� �迭
    public Transform nearTarget;    //����� ���� Ʈ������

    void FixedUpdate()
    {
        //(����ĳ��Ʈ�� ������ġ, ����, ĳ���ù���, ĳ���� ����, ���� ���̾�) ����ĳ��Ʈ�� ������ �ʿ���⶧���� Vectro2.zero�� ���
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);  //������ ���̹��� ���� ���� ������Ʈ���� ����Ʈ�� ����
        //targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer2);
        nearTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float dist = 100;   //�ּҰŸ� �񱳿����� ����� ����

        //���̿� ���� Ÿ���߿��� �÷��̾���� �Ÿ��� ���� ����� ������Ʈ�� transform�� result�� ����
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDist = Vector3.Distance(myPos, targetPos); //Vector3.Distance�� �÷��̾�� Ÿ�ٻ����� �Ÿ��� ����

            if(curDist < dist)
            {
                dist = curDist;
                result = target.transform;
            }
        }
        return result;
    }
}
