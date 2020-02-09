using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogManager
{

    public string folderName = "Log";
    private string logName;

    #region Singleton
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


    

    public void Log(string logString)
    {
        string path = Application.dataPath + "/" + folderName + "/" + logName;

        using (TextWriter textWriter = new StreamWriter(path, true))
        {
            textWriter.WriteLine(logString);
        }
         

        //Debug
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
        string[] filenameArray = Directory.GetFiles(Application.dataPath + "/" + folderName);
        logName = "Log_" + filenameArray.Length/2 + System.DateTime.Now.ToString("_yyyyMMdd_HHmm")+".txt";
        string path = Application.dataPath + "/" + folderName + "/" + logName;
        if (!File.Exists(path))
        {
            FileStream fileStream = File.Create(path);
            fileStream.Close();
        }
    }

}
