using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    //message box
    public GameObject mainWindow;
    //sub box
    public GameObject subWindow;
    //If you want to move the window to display
    public Animator mainAnim;
    public Animator subAnim;
    //Message Box Text
    public Text mainText;
    //Sub  Box Text
    public Text subText;
    //The actual Text to process
    public Text utilityText;
    //Image for Sub Box
    public Image tagetImage;
    //Sprites to be used. If there are many, make an array.
    private Sprite[] targets = new Sprite[2];
    //Flags to be used between additive scenes
    private int tipsChecker = 0;
    //Next button for the main box
    public Button nextButton;
    //The Next button, which is actually used
    public Button utilityButton;
    //Class for storing text
    public tutorialTexts tutoText;
    //For tutorial progress
    private bool chapterflag = false;
    //Current message number
    private int textCount;
    //今見ている文字番号
    private int nowTextNum = 0;
    //Whether you have displayed one message
    private bool isOneMessage = false;
    //Whether you have displayed all the messages.
    private bool isEndMessage = true;


    // Start is called before the first frame update
    void Start()
    {
        //If there are any buttons you want to disable in the tutorial, add them to the process.

        //If you want to include an Image in the subbox.
        SetTargets();
        StartCoroutine(ScenarioRegenerater());
    }

    public IEnumerator ScenarioRegenerater()
    {
        //============================ //
        //                                                         //
        //　Here are the steps of the tutorial.      //
        //                                                         //
        //============================ //

        //Example
        while (!chapterflag)
        {
            yield return MainTipsRegenerater(tutoText.mainsentencesA);
            yield return null;
        }
        chapterflag = false;
        yield return SubTipsRegenerater(tutoText.subsentencesA[0], 0);
        yield return new WaitForSeconds(2.0f);
        yield return EndSubTips();
        chapterflag = false;
        while (!chapterflag)
        {
            yield return MainTipsRegenerater(tutoText.mainsentencesB);
            yield return null;
        }
        chapterflag = false;
        yield return SubTipsRegenerater(tutoText.subsentencesB[0], 1);
        yield return new WaitForSeconds(2.0f);
        yield return EndSubTips();
        chapterflag = false;
        yield return SubTipsRegenerater(tutoText.subsentencesB[1], 1);
        yield return new WaitForSeconds(2.0f);
        yield return EndSubTips();
    }

    IEnumerator MainTipsRegenerater(string[] sentences)
    {
        while (isEndMessage || sentences == null)
        {
            //If you want to use it in an additive scene, set tipsChecker to 1 
            //and set utilityText or utilityButton to Start() of the loaded scene.
            switch (tipsChecker)
            {
                case 0:
                    if (!mainWindow.activeSelf)
                    {
                        mainWindow.SetActive(true);
                        yield return new WaitForSeconds(0.5f);

                        //For animation
                        mainAnim.SetBool("onAir",true);
                        yield return new WaitForSeconds(0.5f);
                        utilityText = mainText;
                        utilityButton = nextButton;

                        //When tipsChecker is set to 0, isEndMessage is set to false and the main part works.
                        isEndMessage = false;
                    }
                    break;
                case 1:
                    tipsChecker = 2;
                    isEndMessage = false;
                    break;
                case 2:
                    break;
            }


            yield return null;
        }

        while (!isEndMessage)
        {
            //No message to be displayed at one time
            if (!isOneMessage)
            {
                //If all messages are displayed, the game objects are hidden
                if (textCount >= sentences.Length)
                {
                    textCount = 0;
                    
                    switch (tipsChecker)
                    {
                        case 0:
                            //For animation
                            mainAnim.SetBool("onAir", false);
                            yield return new WaitForSeconds(0.5f);
                            mainWindow.SetActive(false);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    chapterflag = true;
                    isOneMessage = false;
                    isEndMessage = true;
                    yield break;
                }
                //Otherwise, initialize the text processing related items and display them from the next character.

                //Add one character after the text display time has elapsed.
                utilityText.text += sentences[textCount][nowTextNum];
                nowTextNum++;

                //The full message was displayed, or the maximum number of lines were displayed.
                if (nowTextNum >= sentences[textCount].Length)
                {
                    isOneMessage = true;
                }

                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                //Message to be displayed at one time.
                yield return new WaitForSeconds(0.5f);

                //Press the Next button.
                utilityButton.interactable = true;
                isEndMessage = true;
            }
        }
    }

    IEnumerator SubTipsRegenerater(string sentence, int tar)
    {
        //Show subboxes.
        subWindow.SetActive(true);
        tagetImage.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        tagetImage.sprite = targets[tar];
        yield return new WaitForSeconds(0.5f);

        //For animation
        subAnim.SetBool("onAir", true);
        yield return new WaitForSeconds(0.5f);

        isEndMessage = false;

        while (!isEndMessage)
        {
            //No message to be displayed at one time	
            if (!isOneMessage)
            {
                //Add one character after the text display time has elapsed.
                subText.text += sentence[nowTextNum];
                nowTextNum++;

                //The full message was displayed, or the maximum number of lines were displayed.
                if (nowTextNum >= sentence.Length)
                {
                    isOneMessage = true;
                }

                yield return new WaitForSeconds(0.01f);
            }
            //Message to be displayed at one time.
            else
            {
                yield return new WaitForSeconds(0.5f);

                nowTextNum = 0;
                isEndMessage = true;
                yield break;
            }
        }
    }

    IEnumerator EndSubTips()
    {
        subText.text = "";
        isOneMessage = false;

        //For animation
        subAnim.SetBool("onAir", false);
        chapterflag = true;
        yield return new WaitForSeconds(0.5f);
        subWindow.SetActive(false);
    }

    public void NextButton()
    {
        //Initialize the message function when you press the Next button.
        utilityButton.interactable = false;
        utilityText.text = "";
        nowTextNum = 0;
        //When displaying multiple times in a row, the number of Text is counted.
        textCount++;
        isOneMessage = false;
        isEndMessage = false;
    }

    void SetTargets()
    {
        targets[0] = Resources.Load<Sprite>("TutorialSprite/example1");
        targets[1] = Resources.Load<Sprite>("TutorialSprite/example2");
    }
}
