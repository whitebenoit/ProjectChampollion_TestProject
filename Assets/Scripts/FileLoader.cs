using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class FileLoader : MonoBehaviour
{

    public string folderName = "Spawner";
    public GameObject SpawnerPrefab;
    string[] filenameArray;


    List<GameObject> gameObjectList = new List<GameObject>();


    private void Start()
    {
        InputController.instance.loadingCallbacks.Add(LoadAllFilesInFolder);
    }


    /// <summary>
    /// Load all files (except .meta) from the folder folderName and spawn SpawnerPrefab for each one using their content
    /// </summary>
    public void LoadAllFilesInFolder()
    {


        if (gameObjectList == null)
            gameObjectList = new List<GameObject>();
        else if (gameObjectList.Count > 0)
        {
            foreach (GameObject gameObject in gameObjectList)
            {
                Destroy(gameObject);
            }
            gameObjectList.Clear();
        }

        List<string> contentList = new List<string>();

        filenameArray = Directory.GetFiles(Application.dataPath + "/" + folderName);
        foreach(string fileName in filenameArray)
        {
            string[] filenameSplited = fileName.Split('.');
            if(filenameSplited[filenameSplited.Length -1] != "meta") // We only take txt files and not meta
                contentList.Add(LoadFileFromPath(fileName));
        }

        if (contentList.Count > 0)
        {
            foreach (string content in contentList)
                gameObjectList.Add(SpawnFromContent(content));
        }

    }

    private string LoadFileFromPath(string path)
    {
        string content;
        using (StreamReader streamReader = new StreamReader(path))
        {
            content = streamReader.ReadToEnd();
        }
        return content;
    }

    private GameObject SpawnFromContent(string content)
    {
        string[] contentArray = content.Split(' ');

        GameObject returnedGameobject = Instantiate(SpawnerPrefab);
        SpawnerController spawnerController = SpawnerPrefab.GetComponent<SpawnerController>();
        spawnerController.Init(contentArray[0], float.Parse(contentArray[1]), float.Parse(contentArray[2]), int.Parse(contentArray[3]), float.Parse(contentArray[4]), contentArray[5]);

        return returnedGameobject;
    }


}
