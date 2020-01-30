using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static playerController Instance { get; set; }

    public Vector3 targetPos;
    public Vector3 lastPos, followXonly;
    public float speed = 5.0f;

    private void Awake()
    {

        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {

        if (GameManager.Instance.isStarted == true)
        {
            float distance = transform.position.z - Camera.main.transform.position.z;
            targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);

            followXonly = new Vector3(-targetPos.x*speed, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, followXonly, speed * Time.deltaTime);
            lastPos = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "block")
        {

            GameManager.Instance.GameOver();
          //  spawner.instance.Dead();
            Application.LoadLevel(0);

        }
    }
}
