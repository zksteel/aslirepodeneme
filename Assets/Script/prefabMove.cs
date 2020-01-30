using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabMove : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.isStarted)
        {
            this.gameObject.transform.position -= Vector3.forward * Time.deltaTime * speed;
        }

    }
}
