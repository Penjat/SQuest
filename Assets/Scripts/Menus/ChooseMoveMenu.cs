using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMoveMenu : MonoBehaviour, MoveButtonDelegate {

    ChooseMoveMenuDelegate _delegate;

    public RectTransform _buttonContainer;
    public GameObject _moveButtonPrefab;

    private List<MoveButton> _moveButtons;

    public Text _menuTitle;
    private IBodyPart _bodyPart;

    public void SetUp(ChooseMoveMenuDelegate menuDelegate){
        _delegate = menuDelegate;
    }

    public void Show(List<Move> moves, List<MoveType> partsAvailable, IBodyPart bodyPart){
        _menuTitle.text = GetTitle(bodyPart);
        gameObject.SetActive(true);
        _bodyPart = bodyPart;

        Clear();
        _moveButtons = new List<MoveButton>();
        foreach(Move move in moves){
            if(move.ShouldAppear(bodyPart)){
                bool isLocked = move.CheckLocked(partsAvailable);
                CreateButton(move, bodyPart, isLocked);
            }
        }
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
    public void Clear(){
        if(_moveButtons == null){
            return;
        }
        foreach(MoveButton move in _moveButtons){
            Destroy(move.gameObject);
        }
        _moveButtons.Clear();
    }

    public void CreateButton(Move move, IBodyPart bodyPart, bool isLocked){
        GameObject g = Instantiate(_moveButtonPrefab);
        g.transform.SetParent(_buttonContainer);

        //set button's text
        //TODO store in an array
        MoveButton button = g.GetComponent(typeof(MoveButton)) as MoveButton;
        button.SetUp(this, move, bodyPart);
        button.SetLocked(isLocked);

        _moveButtons.Add(button);
    }
    private string GetTitle(IBodyPart bodyPart){
        string menuText = "";
        //creck if creampied
        if(bodyPart.GetModifier() == BodyPartModifier.Cum){
            menuText += "Cum covered ";
        }
        switch(bodyPart.GetMoveType()){
            case MoveType.Hand:
            menuText += "Hand";
            break;

            case MoveType.Mouth:
            menuText += "Mouth";
            break;

            case MoveType.Breasts:
            menuText += "Breasts";
            break;

            case MoveType.Ass:
            menuText += "Ass";
            break;

            default:
            menuText += "Choose Move";
            break;
        }
        return menuText;
    }
    //--------------MoveButtonDelegate Methods---------------
    public void MoveButtonPressed(Move move){
        Debug.Log("a button was pressed");
        _delegate.MoveSelected(move, _bodyPart);
        Hide();
    }
}

public interface ChooseMoveMenuDelegate {
    void MoveSelected(Move move, IBodyPart bodyPart);
}
