using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{

    #region Known Bugs

    /*
     * refresh rate isn't displayed. This makes it appear to take several button clicks to switch video rate.
     *
     *
     * 
     */

    #endregion
    
    #region Attributes

    #region Player Pref Key Constants

    private const string RESOLUTION_PREF_KEY = "resulution";
    private const string FULLSCREEN_PREF_KEY = "fullscreen";

    #endregion

    #region Resolution

    [SerializeField] private Text resolutionText;
    [SerializeField] private Toggle fullscreen;
    //[SerializeField] private Toggle windowed;
    
    private Resolution[] _resolutions;

    private int currentResolutionIndex = 0;
    
    
    private bool isFullscreen = true;

    #endregion
    
    #endregion
    
    
    // Start is called before the first frame update
    void Start()
    {
        fullscreen.isOn = CheckWindowed(PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY));
        _resolutions = Screen.resolutions;
        currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);
        SetResolutionText(_resolutions[currentResolutionIndex]);
        ToggleWindowed();
    }

    
    #region Resolution cycling

    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = resolution.width + "x" + resolution.height;
    }

    public void SetNextResolution()
    {
        currentResolutionIndex = GetNextWrappedIndex(_resolutions, currentResolutionIndex);
        SetResolutionText(_resolutions[currentResolutionIndex]);
    }
    
    public void SetPreviousResolution()
    {
        currentResolutionIndex = GetPreviousWrappedIndex(_resolutions, currentResolutionIndex);
        SetResolutionText(_resolutions[currentResolutionIndex]);
    }
    
    #endregion

  
    
    #region Apply Resolution

    private void SetAndApplyResolution(int newResolutionIndex)
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(_resolutions[currentResolutionIndex]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);
        SetResolutionPrefs();
        Screen.SetResolution(resolution.width, resolution.height, CheckWindowed(PlayerPrefs.GetInt(FULLSCREEN_PREF_KEY)));


        
    }

    private void SetResolutionPrefs()
    {
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
        PlayerPrefs.SetInt(FULLSCREEN_PREF_KEY, (fullscreen.isOn ? 1 : 0));
    }

    public void ToggleWindowed()
    {
        if (fullscreen.isOn)
        {
            isFullscreen = true;
        }
        else
        {
            isFullscreen = false;
        }
    }

    private bool CheckWindowed(int x)
    {
        if (x == 1)
        {
            isFullscreen = true;
        }
        if (x == 0)
        {
            isFullscreen = false;
        }

        if (x != 1 && x != 0)
        {
            isFullscreen = fullscreen.isOn;
        }
       
        return isFullscreen;
    }
    
    #endregion
    
    #region MISC Helpers

    #region Index Wrap Helpers

    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
        {
            return 0;
        }

        // % means modulo or returns remainder
        return (currentIndex + 1) % collection.Count;
    }
    
    private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
        {
            return 0;
        }

        if ((currentIndex - 1) < 0)
        {
            return collection.Count - 1;
        }
        
        // % means modulo or returns remainder
        return (currentIndex - 1) % collection.Count;
    }

    #endregion
    
    
    #endregion

    public void ApplyChanges()
    {
        SetAndApplyResolution(currentResolutionIndex);
    }
}
