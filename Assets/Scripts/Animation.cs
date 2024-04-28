using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator CharacterAnimator;
    public Animator SpeechBubbleAnimator;
    public GameObject Tilemap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (CharacterAnimator && SpeechBubbleAnimator)
            {
                CharacterAnimator.SetTrigger("Disappear");
                SpeechBubbleAnimator.SetTrigger("Disappear");
                if (!Tilemap.activeInHierarchy)
                    Tilemap.SetActive(true);
            }
            else
                Debug.LogError("Error // Missing animators.");
        }
        
    }
}
