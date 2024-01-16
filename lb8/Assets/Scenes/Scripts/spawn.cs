using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject obj;

    private void Start()
    {

    }

    public void Spawn()
    {

        Instantiate(obj, transform);
        float randZ = Random.Range(0, 40);
        float randY = Random.Range(-5, -4);
        float randX = Random.Range(0, 40);

        float scaleObj = Random.Range(1f, 1.5f);

        Vector3 target = new Vector3(randX, randY, randZ);
        Vector3 scl = new Vector3(scaleObj, scaleObj, scaleObj);

        obj.transform.position = target;
        obj.transform.localScale = scl;
        obj.tag = "Target1";

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") == true)
        {
            DeleteAll("Target1");
            for (int i = 0; i <= 20; i++)
            {
                Spawn();
            }
            //Debug.Log("collider");
        }
        //Destroy(obj);

    }



    public void DeleteAll(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in gameObjects)
        {
            if (target != null)
                GameObject.Destroy(target);
        }
    }
}
