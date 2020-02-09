using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Parameters")]
    public GameObject objectToSpawn;
    [Range(1, 5)]
    public int amount = 3;
    [Range(0.0f, 10.0f)]
    public float radius = 3.0f;
    public Color color;

    private List<GameObject> spawnedObjectList = new List<GameObject>();


    public void Init(string name, float positionX, float positionZ, int amount, float radius, string color)
    {
        this.gameObject.name = name;
        this.transform.position = new Vector3(positionX, this.transform.position.y, positionZ);
        this.amount = amount;
        this.radius = radius;

        switch(color)
        {
            case "Blue":
                this.color = Color.blue;
                break;
            case "Red":
                this.color = Color.red;
                break;
            case "Yellow":
                this.color = Color.yellow;
                break;
            case "Green":
                this.color = Color.green;
                break;
            default:
                this.color = Color.black;
                break;
        }
    }


    private void Start()
    {
        InputController.instance.spawningCallbacks.Add(Spawn);
    }

    private void Spawn()
    {
        if (spawnedObjectList == null)
            spawnedObjectList = new List<GameObject>();
        else if (spawnedObjectList.Count > 0)
        {
            foreach(GameObject gameObject in spawnedObjectList)
            {
                Destroy(gameObject);
            }
            spawnedObjectList.Clear();
        }

        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject spawnedObject = SpawnObject();
                if (spawnedObject != null)
                    spawnedObjectList.Add(spawnedObject);
            }

        }

        LogManager.instance.Log(System.DateTime.Now.ToString("[yyyyMMdd_HHmmss]") + " Spawning object of " + this.gameObject.name);

    }

    private GameObject SpawnObject()
    {
        GameObject spawnedObject = null;
        if (objectToSpawn != null)
        {
            spawnedObject = Instantiate(objectToSpawn, radius* new Vector3(Random.value, Random.value, Random.value), Quaternion.identity, this.transform);
            Renderer renderer = spawnedObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = this.color;
        }
        return spawnedObject;
    }





}
