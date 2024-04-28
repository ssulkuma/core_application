using UnityEngine.UI;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Button ButtonLevel1;
    public Button ButtonLevel2;
    public Button ButtonLevel3;
    public Sprite ButtonCompleted;
    public GameObject Click;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.CompletionLevel1)
        {
            ButtonLevel1.image.sprite = ButtonCompleted;
            Click.transform.localPosition = new Vector3(743.0f, -155.0f, 0.0f);
        }
        if (GameData.CompletionLevel2)
        {
            ButtonLevel2.image.sprite = ButtonCompleted;
            Click.transform.localPosition = new Vector3(-135.0f, -95.0f, 0.0f);
        }
        if (GameData.CompletionLevel3)
        {
            ButtonLevel3.image.sprite = ButtonCompleted;
            Click.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
