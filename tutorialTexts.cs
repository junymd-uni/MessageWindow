using UnityEngine;

public class tutorialTexts : MonoBehaviour
{
    //Store the text to be displayed.
    //Put each amount that can be displayed at a time into an array.
    public string[] mainsentencesA;
    public string[] mainsentencesB;
    public string[] subsentencesA;
    public string[] subsentencesB;

    // Start is called before the first frame update
    void Start()
    {
        masseageSet();
    }

    void SetA()
    {
        mainsentencesA = new string[]{
            "Hi GE Planet.",
            "We are experimenting with displaying the text of the tutorial.",
        };
    }

    void SetB()
    {
        mainsentencesB = new string[]{
            "Sub-blocks can contain tips and other information.",
            "If you have a lot of text, create a separate text management class."
        };

    }

    void SetSubA()
    {
        subsentencesA = new string[]
        {
            "Example1",
        };
    }

    void SetSubB()
    {
        subsentencesB = new string[]
        {
            "Example2",
            "You can also supplement it."
        };
    }

    void masseageSet()
    {
        SetA();
        SetB();
        SetSubA();
        SetSubB();
    }
}
