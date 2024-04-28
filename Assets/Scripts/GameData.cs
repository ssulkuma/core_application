using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    static public bool CompletionLevel1;
    static public bool CompletionLevel2;
    static public bool CompletionLevel3;

    void Start()
    {
        CompletionLevel1 = false;
        CompletionLevel2 = false;
        CompletionLevel3 = false;
    }
}
