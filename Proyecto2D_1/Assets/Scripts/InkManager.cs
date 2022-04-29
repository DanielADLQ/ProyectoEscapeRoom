using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkManager : MonoBehaviour
{
    //[SerializeField] private TextAsset _inkJsonAsset;
    private Story _story;
    [SerializeField] private Text _textField;

    [SerializeField] private VerticalLayoutGroup _choiceButtonContainer;
    [SerializeField] private Button _choiceButtonPrefab;

    [SerializeField] private GameObject textContainer;

    [SerializeField] private GameObject player;
    private GameObject numGenerator;

    void Start()
    {
        //StartStory();
    }

    public void StartStory(TextAsset inkJsonAsset, string variableInk, string itemTag)
    {
        //Debug.Log(itemTag);
        _story = new Story(inkJsonAsset.text);
        textContainer.SetActive(true);

        if(variableInk != "")
        {
            numGenerator = GameObject.FindWithTag("SecretCodeGenerator");
            if(itemTag == "Bookshelf")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<SecretCodeGenerator>().n1;
            }
            if(itemTag == "Chair")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<SecretCodeGenerator>().n2;
            }
            if(itemTag == "Cup")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<SecretCodeGenerator>().n3;
            }
            if(itemTag == "Bed")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<SecretCodeGenerator>().n4;
            }
        }

        DisplayNextLine();

    }

    public void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            string text = _story.Continue(); // gets next line

            text = text?.Trim(); // removes white space from text

            _textField.text = text; // displays new text
        }
        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else if (!_story.canContinue)
        {
            EndStory();
        }
    }

    private void EndStory()
    {
        textContainer.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true; 
    }
    private void DisplayChoices()
    {
        // checks if choices are already being displaye
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {

            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates a choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<Text>();
        buttonText.text = text;

        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        _story.Continue();
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();
    }

    void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }

}