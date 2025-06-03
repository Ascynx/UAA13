using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditorManager : AbstractSubGui
{
    [SerializeField]
    List<SaveEditorSlotManager> slots;
    [SerializeField]
    SubGUIExitButton exitButton;
    [SerializeField]
    AbstractGUI _parent;

    public bool open = false;
    private sbyte currentIndex = 0; //0 = exit button, 1-4 = slots
    private sbyte currentSlotIndex = 0; //0 = load, 1 = delete (if not disabled)

    public void Init()
    {
        currentIndex = 0;
        currentSlotIndex = 0;
        UpdateSelection();
    }

    public void LockSlots()
    {
        foreach(SaveEditorSlotManager slot in slots)
        {
            slot.LockSlot(open);
        }
        exitButton.SetInteractible(false);
    }

    public void UnlockSlots()
    {
        foreach (SaveEditorSlotManager slot in slots)
        {
            slot.UnlockSlot(open);
            slot.UpdateStatus();
        }
        exitButton.SetInteractible(true);
    }

    public override void LoadEaseInAnimation()
    {
        Animation anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.Play("EaseIn");
        }
        open = true;
        Init();
    }

    public override void LoadEaseOutAnimation()
    {
        Animation anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.Play("EaseOut");
        }
        open = false;
        _parent.OnSubGuiClosed();
    }

    public void UpdateSelection()
    {
        if (currentIndex == 0)
        {
            exitButton.selected = true;
        } else
            exitButton.selected = false;

        for (int i = 0; i < slots.Count; i++)
        {
            int index = currentIndex - 1;
            if (i == index)
            {
                slots[i].selected = true;
                int slotIndex = currentSlotIndex;
                if (slotIndex == 0) //load
                {
                    slots[i].loadButton.selected = true;
                    slots[i].deleteButton.selected = false;
                }
                else if (slotIndex == 1 && !slots[i].deleteButton.isEmptySlot) //delete
                {
                    slots[i].loadButton.selected = false;
                    slots[i].deleteButton.selected = true;
                }
                else
                {
                    slots[i].loadButton.selected = false;
                    slots[i].deleteButton.selected = false;
                }
            }
            else
            {
                slots[i].selected = false;
            }
        }
    }

    public override void OnGuiMoved(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            currentSlotIndex = 0; //switch to first button
            //switch to previous slot
        }
        else if (dir == Vector2.down)
        {
            if (currentIndex <= 4) //exit button (0) slots (1-4)
            {
                currentIndex++;
            }
            currentSlotIndex = 0; //switch to first button
            //switch to next slot
        }
        else if (dir == Vector2.left)
        {
            //switch in same slot (to load/new)
            if (currentSlotIndex > 0 && currentIndex > 0)
            {
                currentSlotIndex--;
            }
        }
        else if (dir == Vector2.right)
        {
            //switch in same slot (to delete (if not disabled))
            if (currentSlotIndex < 1 && currentIndex > 0) //load (0) delete (1)
            {
                if (!slots[currentIndex - 1].deleteButton.isEmptySlot)
                {
                    currentSlotIndex++;
                }
            }
        }
        UpdateSelection();
    }

    public override void OnGuiSelect()
    {
        if (currentIndex == 0) //exit button
        {
            exitButton.OnClick();
        }
        else if (currentIndex > 0 && currentIndex <= slots.Count) //slots
        {
            SaveEditorSlotManager slot = slots[currentIndex - 1];
            if (currentSlotIndex == 0) //load
            {
                slot.Load();
            }
            else if (currentSlotIndex == 1 && slot.deleteButton.IsInteractible()) //delete
            {
                slot.Delete();
            }
        }
    }
}
