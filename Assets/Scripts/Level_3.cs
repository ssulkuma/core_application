using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Level_3 : MonoBehaviour
{
    public Canvas Canvas;
    public GameObject Character1;
    public GameObject SpeechBubble;
    private  TMP_Text Speech;
    private Animator CharacterAnimator;
    private Animator SpeechBubbleAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Character1 = Instantiate(Character1);
        SpeechBubble = Instantiate(SpeechBubble);
        Character1.transform.SetParent(Canvas.transform, false);
        SpeechBubble.transform.SetParent(Canvas.transform, false);
        Speech = SpeechBubble.transform.GetChild(0).GetComponent<TMP_Text>();
        Speech.text = "Look how far you've gotten! I hope the team at Metacore can see all this talent, potential and what a great fit you'd be for the company. Best of luck to the interview!";
        SpeechBubbleAnimator = SpeechBubble.GetComponent<Animator>();
        CharacterAnimator = Character1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CharacterAnimator.SetTrigger("Disappear");
            SpeechBubbleAnimator.SetTrigger("Disappear");
            GameData.CompletionLevel3 = true;
            SceneManager.LoadScene("GamePlay");
        }
        
    }
}
