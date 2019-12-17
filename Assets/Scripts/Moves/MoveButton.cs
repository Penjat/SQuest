using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour {

    MoveButtonDelegate _delegate;

    public Text _buttonLabel;
    public Button _button;
    //public PartIndicator _partIndicator;
    public GridLayoutGroup _partsUsedIcons;
    public GridLayoutGroup _partsTargetedIcons;

    public Text _climaxLabel;
    public Text _arousalLabel;

    private bool _isLocked;

    public GameObject _iconPrefab;

    //TODO put elsewhere
    public Sprite[] _partsImages;
    public Sprite[] _targetsImages;

    Move _move;

    public void SetUp(MoveButtonDelegate moveButtonDelegate, Move move){
        _delegate = moveButtonDelegate;
        _buttonLabel.text = move.GetName();
        _move = move;
        //_partIndicator.SetUp(_move.GetPartsUsed());
        _climaxLabel.text = move.GetDmg().GetClimax().ToString();
        _arousalLabel.text = move.GetDmg().GetClimax().ToString();
        ConfigurePartsUsed(move.GetPartsUsed());
        ConfigurePartsTargeted(move.GetPartsTargeted());
    }

    public void WasPressed(){

        _delegate.MoveButtonPressed(_move);
    }
    public void SetLocked(bool b){
        _isLocked = b;
        _button.enabled = !b;
        if(_isLocked){
            _button.image.color = Color.grey;
        }else{
            _button.image.color = Color.white;
        }
    }
    private void ConfigurePartsUsed(HashSet<MoveType> partsUsed){
        foreach(MoveType moveType in partsUsed){
            GameObject g = Instantiate(_iconPrefab);
            g.transform.SetParent(_partsUsedIcons.transform);
        }
    }
    private void ConfigurePartsTargeted(List<TargetType> partsTargeted){
        foreach(MoveType moveType in partsTargeted){
            GameObject g = Instantiate(_iconPrefab);
            g.transform.SetParent(_partsTargetedIcons.transform);
            Image image = g.GetComponent<Image>();
            image.sprite = _targetsImages[0];
        }
    }

}

public interface MoveButtonDelegate{
    void MoveButtonPressed(Move move);
}
