using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudySetup : MonoBehaviour
{

    public static StudySetup studySetup;
    public int ParticipantID;

    private void Start()
    {
        if (studySetup == null) studySetup = this;
    }
    
    private static readonly System.DateTime UnixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    
    public long GetCurrentUnixTimestampMillis()
    {
        return (long)(System.DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
    }
}
