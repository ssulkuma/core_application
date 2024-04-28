using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class Level_1 : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip SoundEffectAlphabet;
    public AudioClip SoundEffectCorrect;
    public Animator GameAnimator;
    public Animator DeveloperAnimator;
    public Animator MetacoreAnimator;
    public Animator CharacterAnimator;
    public Animator SpeechBubbleAnimator;

    public Tilemap WordSearch;
    public TileBase[] AlphabetsOrange;
    public TileBase[] AlphabetsGreen;

    public Camera MainCamera;
    public  TMP_Text Speech;
    private Dictionary<TileBase, TileBase> AlphabetMapping;
    private Dictionary<Vector3Int, TileBase> OriginalTiles;
    private string SelectedWord;
    private BoundsInt TileBounds;
    private TileBase CurrentTile;
    private TileBase Tile;
    private TileBase GreenTile;
    private Vector3 MousePosition;
    private Vector3Int CellPosition;


    // Start is called before the first frame update
    void Start()
    {
        SelectedWord = "";
        InitializeMapping();
        SaveOriginalTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && WordSearch.isActiveAndEnabled)
        {
            MousePosition = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            CellPosition = WordSearch.WorldToCell(MousePosition);
            if (WordSearch.HasTile(CellPosition))
            {
                CurrentTile = WordSearch.GetTile(CellPosition);
                if (AlphabetMapping.ContainsKey(CurrentTile))
                {
                    GreenTile = AlphabetMapping[CurrentTile];
                    SelectedWord += GreenTile.name;
                    WordSearch.SetTile(CellPosition, GreenTile);
                    AudioSource.PlayOneShot(SoundEffectAlphabet);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheckSelectedWord();
            RevertToOriginalTiles();
            if (MetacoreAnimator.GetBool("MetacoreFound") && GameAnimator.GetBool("GameFound") && DeveloperAnimator.GetBool("DeveloperFound"))
            {
                GameData.CompletionLevel1 = true;
                Speech.text = "Great work! It seems that we have stumbled upon our dream job ad - Game developer Intern at Metacore. We should hurry and start preparing our application!";
                CharacterAnimator.SetTrigger("Appear");
                SpeechBubbleAnimator.SetTrigger("Appear");
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && GameData.CompletionLevel1)
            SceneManager.LoadScene("GamePlay");
        
    }

    // Initialize the mapping between orange and green alphabet tiles.
    void InitializeMapping()
    {
        AlphabetMapping = new Dictionary<TileBase, TileBase>();
        for (int i = 0; i < AlphabetsOrange.Length; i++)
            AlphabetMapping.Add(AlphabetsOrange[i], AlphabetsGreen[i]);
    }

    // Saves the placement of the orange tiles before they are changed by the mouse drag.
    void SaveOriginalTiles()
    {
        OriginalTiles = new Dictionary<Vector3Int, TileBase>();
        TileBounds = WordSearch.cellBounds;
        foreach (var position in TileBounds.allPositionsWithin)
        {
            if (WordSearch.HasTile(position))
            {
                Tile = WordSearch.GetTile(position);
                if (AlphabetMapping.ContainsKey(Tile))
                    OriginalTiles[position] = Tile;
            }
        }
    }

    // Reverts the Word Search Tilemap to its original state and clears the Selected word variable.
    void RevertToOriginalTiles()
    {
        foreach (var kvp in OriginalTiles)
            WordSearch.SetTile(kvp.Key, kvp.Value);
        SelectedWord = "";
    }

    // Checks if the dragged over alphabets form any of the target words.
    void CheckSelectedWord()
    {
        switch (SelectedWord)
        {
            case "game":
                GameAnimator.SetBool("GameFound", true);
                AudioSource.PlayOneShot(SoundEffectCorrect);
                break;
            case "developer":
                DeveloperAnimator.SetBool("DeveloperFound", true);
                AudioSource.PlayOneShot(SoundEffectCorrect);
                break;
            case "metacore":
                MetacoreAnimator.SetBool("MetacoreFound", true);
                AudioSource.PlayOneShot(SoundEffectCorrect);
                break;
            default:
                break;
        }
    }
}
