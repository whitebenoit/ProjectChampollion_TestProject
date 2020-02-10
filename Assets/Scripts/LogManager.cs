using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogManager
{

    public string folderName = "Log";
    private string logName;

    #region Singleton
    // This is a singleton (design pattern). The singleton is an object that can only be instantiated once.
    // All further call will return the first instance, so you now that any instance from anywhere in the code
    // of a singleton is the same instance.
    private LogManager()
    {
        Init();
    }

    private static LogManager _instance;
    public static LogManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new LogManager();
            return _instance;
        }
    }
    #endregion Singleton


    
    /// <summary>
    /// Log the provided string into the file for this game
    /// </summary>
    /// <param name="logString"></param>
    public void Log(string logString)
    {
        string path = Application.dataPath + "/" + folderName + "/" + logName;

        using (TextWriter textWriter = new StreamWriter(path, true))
        {
            textWriter.WriteLine(logString);
        }
         

        //Debug - Read all lines from the file at path
        //using (StreamReader streamReader = new StreamReader(path))
        //{
        //    int i = 0;
        //    string line = "";
        //    while ((line = streamReader.ReadLine()) != null)
        //    {
        //        Debug.Log(i+" - "+line);
        //        i++;
        //    }
        //}

    }



    private void Init()
    {
        // Create a new filename for the log file
        string[] filenameArray = Directory.GetFiles(Application.dataPath + "/" + folderName);
        logName = "Log_" + filenameArray.Length/2 + System.DateTime.Now.ToString("_yyyyMMdd_HHmm")+".txt";
        string path = Application.dataPath + "/" + folderName + "/" + logName;

        // Then create said file (only if it doesn't exist)
        if (!File.Exists(path))
        {
            FileStream fileStream = File.Create(path);
            fileStream.Close();
        }
    }

}
