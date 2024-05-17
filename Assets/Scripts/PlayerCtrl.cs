using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float pSpeed;

    [HideInInspector] public Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDir = ((Vector3.up * v) + (Vector3.right * h));

        transform.Translate(moveDir * pSpeed * Time.deltaTime);

        Vector3 myPos = transform.position;
    }
}
