using UnityEngine;
using static PreferenceValueFactory;

public class PreferenceIntegration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ValueHandle<string> layoutHandle = PreferenceValueFactory.CreateHandleOf("PlayerLayout", "");
        if (layoutHandle.GetValue() == null)
        {

            layoutHandle.SetValue(getKeyboardDefaultLayout());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected string getKeyboardDefaultLayout()
    {
        return ""; //NOT YET IMPLEMENTED
    }
}
