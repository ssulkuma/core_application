using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Buttons : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip Button;

    // Switches to desired scene on button click.
    public void SwitchScene(string scene)
    {
        StartCoroutine(PlaySoundEffect(scene));
    }

    IEnumerator PlaySoundEffect(string scene)
    {
        if (Button)
            AudioSource.PlayOneShot(Button);
        else
            Debug.Log("Error // No selected button sound effect.");
        yield return new WaitForSeconds(Button.length);
        if (scene != "\0")
            SceneManager.LoadScene(scene);
        else
            Debug.Log("Error // No scene given.");
    }

    // Quits the game on button click.
    public void QuitGame()
    {
        Application.Quit();
    }
}
