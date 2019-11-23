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

    public void SetUp(ChooseMoveMenuDelegate menuDelegate){
        _delegate = menuDelegate;
    }

    public void Show(List<Move> moves, IDictionary<MoveType,Move> partsUsed, MoveType moveType){
        _menuTitle.text = GetTitle(moveType);
        gameObject.SetActive(true);

        Clear();
        _moveButtons = new List<MoveButton>();
        int i = 0;
        foreach(Move move in moves){
            Debug.Log("move name: " + move.GetName());
            bool isLoked = move.CheckLocked(partsUsed);
            CreateButton(move, i, isLoked);
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

    public void CreateButton(Move move,int i, bool isLocked){
        GameObject g = Instantiate(_moveButtonPrefab);
        g.transform.SetParent(_buttonContainer);

        //set button's text
        //TODO store in an array
        MoveButton button = g.GetComponent(typeof(MoveButton)) as MoveButton;
        button.SetUp(this, move);

        //find the button's position
        float buttonWidth = _buttonContainer.rect.width;
        float buttonHeight = 80.0f;

        float x1 = 0.0f;
        float y1 = -_moveButtons.Count*buttonHeight -buttonHeight;
        float x2 = x1 + buttonWidth;
        float y2 = y1 + buttonHeight;

        //set the position
        RectTransform rectTransform = g.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(x1,y1);
        rectTransform.offsetMax = new Vector2(x2,y2);
        button.SetLocked(isLocked);

        _moveButtons.Add(button);
    }
    private string GetTitle(MoveType moveType){
        switch(moveType){
            case MoveType.Hand:
            return "Hand";
            case MoveType.Mouth:
            return "Mouth";
            case MoveType.Breasts:
            return "Breasts";
            case MoveType.Ass:
            return "Ass";
            default:
            return "Choose Move";
        }
    }
    //--------------MoveButtonDelegate Methods---------------
    public void MoveButtonPressed(Move move){
        Debug.Log("a button was pressed");
        _delegate.MoveSelected(move);
        Hide();
    }
}

public interface ChooseMoveMenuDelegate {
    void MoveSelected(Move move);
}
