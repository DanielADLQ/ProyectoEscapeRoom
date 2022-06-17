using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InkManager : MonoBehaviour
{
    private Story _story;
    [SerializeField] private Text _textField;
    [SerializeField] private VerticalLayoutGroup _choiceButtonContainer;
    [SerializeField] private Button _choiceButtonPrefab;
    [SerializeField] private GameObject textContainer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject globalVariables;
    private GameObject numGenerator;
    private string variableInk;
    public string valorSituacion = "";
    [SerializeField] private GameObject item;
    private string itemTag;
    [SerializeField] private GameObject dbUserItem;

    string[] listaIng1 = { "Merluza", "Atún", "Salmón", "Dorada", "Lubina" };
    string[] listaIng2 = { "Cerdo", "Pollo", "Buey", "Ternera", "Cordero" };
    string[] listaIng3 = { "Manzana", "Plátano", "Naranja", "Melocotón", "Kiwi" };
    string[] listaIng4 = { "Natillas", "Tiramisú", "Flan", "Helado", "Tarta" };

    public void StartStory(TextAsset inkJsonAsset, string variableInk, string itemTag)
    {
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
                _story.variablesState[variableInk] = int.Parse(item.GetComponent<waterPuzle>().valorVar);
            }
            //Scene3
            if (itemTag == "Fridge")
            {
                _story.variablesState[variableInk] = int.Parse(item.GetComponent<fridgePuzle>().valorVar);
            }

            if (itemTag == "Microwave")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing2;
            }
            if (itemTag == "CatFood")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing1;
            }
            if (itemTag == "Tablecloth")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing3;
            }
            if (itemTag == "KitchenGame")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing4;
            }


        }

        DisplayNextLine();

    }

    //Para textos que cambian tras la primera ejecucion
    public void StartStory(TextAsset inkJsonAsset, string variableInk, string itemTag, string checkedItem)
    {
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
                _story.variablesState[variableInk] = int.Parse(item.GetComponent<waterPuzle>().valorVar);
            }
            //Scene3
            if (itemTag == "Fridge")
            {
                _story.variablesState[variableInk] = int.Parse(item.GetComponent<fridgePuzle>().valorVar);
            }

            if (itemTag == "Microwave")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing2;
            }
            if (itemTag == "CatFood")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing1;
            }
            if (itemTag == "Tablecloth")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing3;
            }
            if (itemTag == "KitchenGame")
            {
                _story.variablesState[variableInk] = numGenerator.GetComponent<Laptop>().ing4;
            }


        }

        DisplayNextLine();

    }

    public void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            string text = _story.Continue();

            text = text?.Trim();

            _textField.text = text;
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

        if(itemTag == "CheckOneTime")
        {
            globalVariables.GetComponent<GlobalVariables>().changeVariable(variableInk,"1");

        }
        else
        {
            if(itemTag=="Bath")
            {
                if(variableInk!="")
                {
                    if(_story.variablesState[variableInk].ToString() != "Null")
                    {
                        valorSituacion = _story.variablesState[variableInk].ToString();
                        item.GetComponent<waterPuzle>().valorVar = valorSituacion;
                    }
                }
            }
            if (itemTag == "Fridge")
            {
                if (variableInk != "")
                {
                    if (_story.variablesState[variableInk].ToString() != "Null")
                    {
                        valorSituacion = _story.variablesState[variableInk].ToString();
                        item.GetComponent<fridgePuzle>().valorVar = valorSituacion;
                    }
                }
            }

            if (itemTag == "Microwave")
            {
                item.GetComponent<fridgePuzle>().valorVar = "0"; //Pierde el objeto
            }

            if (itemTag == "CatFood")
            {
                item.GetComponent<fridgePuzle>().valorVar = "0"; //Pierde el objeto
            }
        }


    }
    private void DisplayChoices()
    {
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++)
        {
            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text);

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    Button CreateChoiceButton(string text)
    {
        var choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        var buttonText = choiceButton.GetComponentInChildren<Text>();
        buttonText.text = text;

        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        _story.Continue();
        RefreshChoiceView();
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