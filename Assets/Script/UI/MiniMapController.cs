using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField]
    Camera miniMapCamera;
    [SerializeField]
    GameObject MinimapUIObject;

    public void ToggleMiniMap()
    {
        ToggleMiniMap(!MinimapUIObject.activeSelf);
    }

    public void ToggleMiniMap(bool toggle)
    {
        if (!toggle)
        {
            MinimapUIObject.SetActive(false);
            miniMapCamera.enabled = false;
        }
        else
        {
            MinimapUIObject.SetActive(true);
            miniMapCamera.enabled = true;
        }
    }

    public void OnLeaveOverworld()
    {
        if (MinimapUIObject.activeSelf)
        {
            ToggleMiniMap(false);
        }
    }

    public void OnReEnterOverworld()
    {
        if (!MinimapUIObject.activeSelf)
        {
            ToggleMiniMap(true);
        }
    }
}
