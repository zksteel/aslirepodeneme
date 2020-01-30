using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner instance { get; private set; }
    float spawnValues =2.7f;
   float NegativespawnValues=3f;
    //public Vector3 NegativespawnValues=3f;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLastWait;
    public int startWait;
    //public GameObject[] enemies, destroyEnemy;
    //public float spawnWait;
    //public float spawnMostWait;
    //public float spawnLastWait;
    //public int startWait;
    //public bool stop = true;
    //int randEnemy;
    //GameObject clone;
    //Transform firstPos;
    //public Rigidbody rb;
    //private void Awake()
    //{

    //    Application.targetFrameRate = 60;
    //    QualitySettings.vSyncCount = 0;

    //    if (!Instance)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(waitSpawner());
    //    rb = gameObject.GetComponent<Rigidbody>();
    //    //   stop = true;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (GameManager.Instance.isStarted)
    //    {
    //        stop = false;
    //    }

    //    spawnWait = Random.Range(spawnLastWait, spawnMostWait);


    //}
    //IEnumerator waitSpawner()
    //{

    //    yield return new WaitForSeconds(startWait);

    //    while (stop == false)
    //    {
    //        randEnemy = Random.Range(0, 2);
    //        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, 6.5f);

    //        clone = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, Time.deltaTime * speed), transform.rotation * Quaternion.Euler(0f, 180f, 0f));


    //        Destroy(clone, speed + 10f);

    //        yield return new WaitForSeconds(spawnWait);

    //    }
    //}

    //public void Dead()
    //{
    //    clone.transform.position = new Vector3(4.23f, .75f, 7.1f);
    //}

    public float speed = 5f;
    public List<GameObject> prefabList = new List<GameObject>();
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;

    public Transform firstPosition1, firstPosition2, firstPosition3;
    private void Awake()
    {
        if (!instance)
        {
            instance = null;
        }
    }
    void Start()
    {
        MakePrefab();
            }
  
    public void MakePrefab()
    {
        prefabList.Add(Prefab1);
        prefabList.Add(Prefab2);
        prefabList.Add(Prefab3);
        firstPosition1.transform.position = Prefab1.transform.position;
        firstPosition2.transform.position = Prefab2.transform.position;
        firstPosition3.transform.position = Prefab3.transform.position;
        Debug.Log(firstPosition1.transform.position);
        InvokeRepeating("Spawned", .05f, 1.0f);

    }


    void Spawned()
    {
        
        if (GameManager.Instance.isStarted == true)
        {
            Debug.Log("şimdi çalışıyporrrrrr");
            int prefabIndex = Random.Range(0, 2);
            Vector3 spawnPosition = new Vector3(Random.Range(-NegativespawnValues, spawnValues), 1, 6.5f);
            GameObject instObj = 
            Instantiate(prefabList[prefabIndex], spawnPosition + transform.TransformPoint(0, 0, Time.deltaTime * speed), transform.rotation * Quaternion.Euler(0f, 180f, 0f));

            //float rRange = Random.Range(-2.16f, 2.16f);
            ////Debug.Log(rRange);

            ////  Vector3 temp = new Vector3(rRange, 13.6f, 26f);
            //Vector3 temp = new Vector3(rRange, 15.6f, 16f);
            //if (transform.position.x < -1)
            //{
            //    transform.position = new Vector3(0, transform.position.y, transform.position.z);
            //}
            //transform.position = temp;
           
        }

        else
        {
        }

    }

}
