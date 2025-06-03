using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaveEditorButton
{
    public void UpdateStatus(bool slotExists, string slot);
    public void SetInteractible(bool interactible);

    public bool IsInteractible();

    public void OnClick();
}
