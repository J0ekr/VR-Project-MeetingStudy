using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Leap.Unity;
using Photon.Pun;
using UnityEngine;


// Class for saving positional data of the palm, together with the condition and co.
public class SaveHandPosition
{
    private string[] _rowDataTemp;
    private static SaveHandPosition _instance = null;


    private static readonly string CsvSeparator = ",";


    string FileName;
    private StreamWriter sw;


    private SaveHandPosition()
    {
        Debug.Log("Starting Record boneData");
        this.FileName = GetPath();
        FileInfo f = new FileInfo(FileName);
        if (f.Directory != null) f.Directory.Create();
        bool writeHeader = !File.Exists(FileName);


        sw = new StreamWriter(FileName, true);
        if (writeHeader)
        {
            sw.Write(GetCsvHeader() + "\r\n");
            sw.Flush();
        }
    }

    private string GetPath()
    {
        return Application.dataPath + "/CSV/UserID" + StudySetup.studySetup.ParticipantID + "/UserID" +
               StudySetup.studySetup.ParticipantID +
               StudySetup.studySetup.GetCurrentUnixTimestampMillis().ToString() + ".csv";
    }

    private string GetCsvHeader()
    {
        string header = "";

        header += "Condition" + CsvSeparator + "timestamp" + CsvSeparator + "SubjectID" + CsvSeparator +
                  "GameObjectName" +
                  CsvSeparator + "viewID" + CsvSeparator + "Hand" + CsvSeparator + "isStudy" + CsvSeparator + "PosX" +
                  CsvSeparator + "PosY" +
                  CsvSeparator + "PosZ" + CsvSeparator + "RotX" + CsvSeparator + "RotY" + CsvSeparator + "RotZ";
        return header;
    }

    public static void Save(GameObject palm, Chirality w, int P)
    {
        if (_instance == null)
        {
            _instance = new SaveHandPosition();
        }


        var timestamp = StudySetup.studySetup.GetCurrentUnixTimestampMillis();
        var Condition = MySceneManager.sceneManager.currentScene;
        var SubjectID = StudySetup.studySetup.ParticipantID;
        var GameObjectName = palm.name;
        var Hand = w;
        var viewID = P;
        var isStudy = MySceneManager.sceneManager.isStudy;

        string output = "";

        var PosX = palm.transform.position.x;
        var PosY = palm.transform.position.y;
        var PosZ = palm.transform.position.z;
        var RotX = palm.transform.position.x;
        var RotY = palm.transform.position.y;
        var RotZ = palm.transform.position.z;

        output += Condition + CsvSeparator + timestamp + CsvSeparator + SubjectID + CsvSeparator + GameObjectName +
                  CsvSeparator + viewID + CsvSeparator + Hand + CsvSeparator + isStudy + CsvSeparator + PosX +
                  CsvSeparator + PosY +
                  CsvSeparator + PosZ + CsvSeparator + RotX + CsvSeparator + RotY + CsvSeparator + RotZ;


        _instance.sw.Write(output + "\r\n");
        _instance.sw.Flush();
    }


    // parse left hand rotation


    public static string FloatToString(float value)
    {
        return String.Format("{0:0.####################}", value);
    }

    public static string DoubleToString(double value)
    {
        return String.Format("{0:0.####################}", value);
    }

    //call if study is over
    public static void closeIfOpen()
    {
        if (_instance != null)
        {
            _instance.sw.Close();
            Debug.Log("Finished Record boneData");
            _instance = null;
        }
    }
}