using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectWidget : MonoBehaviour
{
    private Image self;

    private void Awake()
    {
        self = gameObject.GetComponent<Image>();
        self.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAppliedEffect(Typings.Effect effect, int level)
    {
        if (self != null)
        {
            Optional<Sprite> sprite = Typings.GetSpriteOf(effect, level);
            if (sprite.IsPresent())
            {
                self.sprite = sprite.Get();
            }
            else
            {
                self.sprite = Typings.Unset;
            }
        }

        if (!self.sprite.name.Equals(Typings.Unset.name))
        {
            self.enabled = true;
        }
        else self.enabled = false;
    }
}
