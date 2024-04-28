using UnityEngine;
using UnityEngine.UI;

public class SystemFont : MonoBehaviour
{
    public Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        if (CheckSystemFont("Wawati SC Regular"))
        {
            Font WawatiSCRegular = Font.CreateDynamicFontFromOSFont("Wawati SC Regular", 12);
            textComponent.font = WawatiSCRegular;
        }
        else
        {
            Debug.LogError("Error // Could not find font 'Wawati SC Regular on the system.");
        }
        
    }

    bool CheckSystemFont(string FontName)
    {
        foreach (string font in Font.GetOSInstalledFontNames())
        {
            Debug.Log(font + "\n");
            if (font == FontName)
                return (true);
        }
        return (false);
    }
}
