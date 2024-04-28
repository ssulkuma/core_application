using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class Level_2 : MonoBehaviour
{
    public Canvas Canvas;
    public GameObject Character1;
    public GameObject SpeechBubble;
    public TMP_Text Timer;
    public TMP_Text CountdownTimer;
    public TMP_Text ScoreDisplay;
    public GameObject[] Alphabets;
    public Sprite[] GreenAlphabets;
    public AudioSource AudioSource;
    public AudioClip Twinkle;
    private float CurrentTime;
    private int Score;
    private int HighlightIndex;
    private string HighlightedAlphabet;
    private KeyCode PressedKey;
    private  TMP_Text Speech;
    private Animator CharacterAnimator;
    private Animator SpeechBubbleAnimator;
    private Sprite OriginalAlphabet;
    private SpriteRenderer OrangeAlphabets;
    private SpriteRenderer OriginalSprite;
    private float Seconds;
    private float Milliseconds;

    // Start is called before the first frame update
    void Start()
    {
        Character1 = Instantiate(Character1);
        SpeechBubble = Instantiate(SpeechBubble);
        Character1.transform.SetParent(Canvas.transform, false);
        SpeechBubble.transform.SetParent(Canvas.transform, false);
        Speech = SpeechBubble.transform.GetChild(0).GetComponent<TMP_Text>();
        SpeechBubbleAnimator = SpeechBubble.GetComponent<Animator>();
        CharacterAnimator = Character1.GetComponent<Animator>();
        CurrentTime = -1f;
        Score = 0;
        HighlightIndex = -1;
        Speech.text = "Stretch those fingers, we got an application to write! You're given a 20s window to press as many matching keys on the keyboard as highlighted on the screen. Be sure to score more than 8 to get chosen to the interview!";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CharacterAnimator.SetTrigger("Disappear");
            SpeechBubbleAnimator.SetTrigger("Disappear");
            StartCoroutine(Countdown());
        }
        if (HighlightIndex < 0)
            HighlightRandomKey();
        if (CurrentTime > 0.00f)
        {
            CurrentTime -= Time.deltaTime;
            Seconds = Mathf.Floor(CurrentTime);
            Milliseconds = Mathf.Floor((CurrentTime - Seconds) * 1000f);
            if (CurrentTime <= 0.00f)
            {
                Timer.text = "00.00";
                EndGame();
            }
            else
                Timer.text = string.Format("{0:00.00}", Seconds + Milliseconds / 1000f);
            if (Input.anyKeyDown)
            {
                PressedKey = GetPressedKey();
                if (CheckIfCorrectKey())
                {
                    ScoreDisplay.text = Score.ToString();
                    HighlightRandomKey();
                }
            }
        }
        if (GameData.CompletionLevel2 && Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("GamePlay");
        
    }

    // Displays a countdown before the game starts.
    IEnumerator Countdown()
    {
        CountdownTimer.text = "3";
        yield return new WaitForSeconds(1f);

        CountdownTimer.text = "2";
        yield return new WaitForSeconds(1f);

        CountdownTimer.text = "1";
        yield return new WaitForSeconds(1f);

        CountdownTimer.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        CountdownTimer.gameObject.SetActive(false);
        CurrentTime = 20.00f;
        foreach (GameObject alphabet in Alphabets)
            alphabet.SetActive(true);
    }


    // Gets which key was pressed on the keyboard.
    KeyCode GetPressedKey()
{
    foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
    {
        if (Input.GetKeyDown(Key))
            return (Key);
    }
    return (KeyCode.None);
}

    // Checks if the pressed key matches to the highlighted alphabet.
    bool CheckIfCorrectKey()
    {
        if (PressedKey >= KeyCode.A && PressedKey <= KeyCode.Z)
        {
            if (PressedKey.ToString() == HighlightedAlphabet)
            {
                Score += 1;
                AudioSource.PlayOneShot(Twinkle);
                return (true);
            }
        }
        return (false);
    }

    // Highlights a random alphabet from the keyboard visual.
    void HighlightRandomKey()
    {
        if (OrangeAlphabets != null)
            OrangeAlphabets.sprite = OriginalAlphabet;
        HighlightIndex = Random.Range(0, Alphabets.Length);
        OriginalSprite = Alphabets[HighlightIndex].GetComponent<SpriteRenderer>();
        OriginalAlphabet = OriginalSprite.sprite;
        OrangeAlphabets = Alphabets[HighlightIndex].GetComponent<SpriteRenderer>();
        OrangeAlphabets.sprite = GreenAlphabets[HighlightIndex];
        HighlightedAlphabet = Alphabets[HighlightIndex].name;
    }

    // Checks the score and handles the gaming ending correctly.
    void EndGame()
    {
        if (Score >= 8)
        {
            GameData.CompletionLevel2 = true;
            foreach (GameObject alphabet in Alphabets)
                alphabet.SetActive(false);
            Speech.text = "Amazing! With an application like this, Metacore will surely take notice. Better start practising for the interview to sweep everyone off their feet with your talent! ";
            CharacterAnimator.SetTrigger("Appear");
            SpeechBubbleAnimator.SetTrigger("Appear");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
