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
    [SerializeField] private GameObject globalVariables;
    private GameObject numGenerator;
    private string variableInk;
    public string valorSituacionBath = ""; //MEJOR IMPLEMENTAR CON CORRUTINAS
    [SerializeField] private GameObject bath;
    private string itemTag;
    

    void Start()
    {
        //StartStory();
    }

    public void StartStory(TextAsset inkJsonAsset, string variableInk, string itemTag)
    {

        //Debug.Log(itemTag);
        //_story.ChoosePathString("");
        _story = new Story(inkJsonAsset.text);
        textContainer.SetActive(true);
        this.itemTag = itemTag;
        if(variableInk != "")
        {
            this.variableInk=variableInk;
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
            //Scene2
            if(itemTag == "Bath")
            {
                _story.variablesState[variableInk] = int.Parse(bath.GetComponent<waterPuzle>().valorVar);
            }


        }

        DisplayNextLine();

    }

    //Para textos que cambian tras la primera ejecucion
    public void StartStory(TextAsset inkJsonAsset, string variableInk, string itemTag, string checkedItem)
    {
            /*if(itemTag == "CheckOneTime")
            {
                //_story.variablesState[variableInk] = "0";
                //Aqui pasar la variable de checkFirstTime, vinculando el script
                Debug.Log("Empieza checkOne");
                Debug.Log(_story.variablesState[variableInk].ToString());
                if(_story.variablesState[variableInk].ToString() == "")
                {
                    _story.variablesState[variableInk] = "0";
                    //Mandar el valor a checkFirsftTime para recogerlo despues en startStory
                } 
                
            }*/
        

        //Debug.Log(itemTag);
        _story = new Story(inkJsonAsset.text);

        if(checkedItem == "1")
        {
            _story.ChoosePathString("After");
        }
        else
        {
            _story.ChoosePathString("First");
        }

        textContainer.SetActive(true);
        this.itemTag = itemTag;
        if(variableInk != "")
        {
            this.variableInk=variableInk;
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
            //Scene2
            if(itemTag == "Bath")
            {
                _story.variablesState[variableInk] = int.Parse(bath.GetComponent<waterPuzle>().valorVar);
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

        //OBJETOS CUYO TEXTO CAMBIA TRAS LA PRIMERA INTERACCION
        if(itemTag == "CheckOneTime")
        {
            globalVariables.GetComponent<GlobalVariables>().changeVariable(variableInk,"1");
            /*if(_story.variablesState[variableInk].ToString() == "0")
            {
                _story.variablesState[variableInk] = "1";
                //Mandar el valor a checkFirsftTime para recogerlo despues en startStory
            }*/ 
        }
        else //REVISAR ESTA PARTE-
        {
            if(itemTag=="Bath")
            {
                if(variableInk!="")
                {
                    if(_story.variablesState[variableInk].ToString() != "Null")
                    {
                        Debug.Log(_story.variablesState[variableInk].ToString());
                        valorSituacionBath = _story.variablesState[variableInk].ToString();
                        bath.GetComponent<waterPuzle>().valorVar = valorSituacionBath;
                    }
                }
            }
        }


    }
    private void DisplayChoices()
    {
        // checks if choices are already being displayed
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