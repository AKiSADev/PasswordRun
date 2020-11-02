using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

    public Dialog dialog;

    private static ILogger log = Debug.unityLogger;
    private static string tag = "DIALOG_SCENE";

    private void Awake()
    {
        //inizio il dialogo
        log.Log(tag, dialog.sentences.GetValue(0));
        FindObjectOfType<DialogManager>().StartDialogue(dialog);
        
    }
}
