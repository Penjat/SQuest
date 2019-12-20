using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectState{
Norm, Over, Targeted
};

//The card is the UI interface for each enemy
//the logic for the enemy is controlled by its class
public class Card : MonoBehaviour, ICard {

    private ICardDelegate _delegate;

    public HorizontalLayoutGroup _bodyTargetContainer;

    private const float FILL_RATE = 2.0f;

    public UIParticleSystem _explodeEffect;
    public Text _nameLabel;
    public StatusBar _climaxBar;
    public StatusBar _arousalBar;
    public Color _overColor;
    public Color _normColor;
    public Animator _animator;
    public Image _button;
    public DMGLabel _dmgLabel;

    public GameObject _bodyTargetPrefab;

    private List<DmgWithType> _dmgList = new List<DmgWithType>();

    void Start(){
        _button.color = _normColor;
    }
    public void SetUpBodyTargets(HashSet<BodyTarget> bodyTargets){
        foreach(BodyTarget bodyTarget in bodyTargets){
            CreateBodyTarget(bodyTarget);
        }
    }
    private void CreateBodyTarget(BodyTarget bodyTarget){
        //TODO add BodyTargetDisplay to array
        GameObject g = Instantiate(_bodyTargetPrefab);
        g.transform.SetParent(_bodyTargetContainer.transform);
        IBodyTargetDisplay targetDisplay = g.GetComponent<IBodyTargetDisplay>();
        bodyTarget.SetDisplay(targetDisplay);
    }

    public void SetName(string name){
        _nameLabel.text = name;
    }
    public void SetUpClimax(StatusBarDelegate statusBarDelegate, float max){
        _climaxBar.SetUp(statusBarDelegate, max, FILL_RATE);
    }
    public void SetClimax(float climax, bool animated=false){
        if(animated){
            _climaxBar.SetValueAnimated(climax);
            return;
        }
        _climaxBar.SetValue(climax);
    }
    public void SetUpArousal(StatusBarDelegate statusBarDelegate, float max){
        _arousalBar.SetUp(statusBarDelegate, max, FILL_RATE);
    }
    public void SetArousal(float arousal, bool animated=false){
        if(animated){
            _arousalBar.SetValueAnimated(arousal);
            return;
        }
        _arousalBar.SetValue(arousal);
    }

    public void Climax(){
        _explodeEffect.Play();
        _animator.Play("Climax");
    }
    public void ShowDmg(int dmg, DmgType dmgType=DmgType.Climax){
        //TODO pass in color or icons
        DmgWithType newDmg = new DmgWithType(dmg, dmgType);
        if(_dmgList.Count > 0){
            _dmgList.Add(newDmg);
            return;
        }
        _dmgList.Add(newDmg);
        _dmgLabel.ShowDmg(newDmg);
        StartCoroutine(CheckNextDmg());
    }
    private void NextDmg(){
        //remove the one we just showed
        _dmgList.RemoveAt(0);
        //check if still dmg left to show
        if(_dmgList.Count > 0){
            _dmgLabel.ShowDmg(_dmgList[0]);
            StartCoroutine(CheckNextDmg());
        }
    }
    private IEnumerator CheckNextDmg(){
        yield return new WaitForSeconds(2.0f);
        NextDmg();
    }
    public void SetTargeted(bool b){
        if(b){
            Debug.Log("should play targeted");
            _animator.Play("Targeted");
            return;
        }
        _animator.Play("Idle");
    }
    public void SetState(SelectState state){
        switch(state){

            case SelectState.Norm:
            _button.color = _normColor;
            SetTargeted(false);
            break;

            case SelectState.Targeted:
            _button.color = _normColor;
            SetTargeted(true);
            break;

            case SelectState.Over:
            _button.color = _overColor;
            SetTargeted(false);
            break;
        }
    }
    public void SetDelay(float delay){
        StartCoroutine(AppearIn(delay));
    }
    private IEnumerator AppearIn(float delay){
        _animator.speed = 0.0f;
        yield return new WaitForSeconds(delay);
        _animator.speed = 1.0f;
    }
    public void Attack(){
        _animator.Play("GenericAttack");
    }
    public void SetCardDelegate(ICardDelegate cardDelegate){
        _delegate = cardDelegate;
    }

    //---------------Input Methods----------------------
    public void MouseEnter(){
        _delegate.MouseEnter();
    }
    public void MouseExit(){
        _delegate.MouseExit();
    }
    public void MousePress(){
        _delegate.WasPressed();
    }
}

public interface ICard {
    void Climax();
    void SetName(string name);
    void SetUpClimax(StatusBarDelegate statusBarDelegate, float max);
    void SetClimax(float climax, bool animated=false);
    void SetUpArousal(StatusBarDelegate statusBarDelegate, float max);
    void SetArousal(float arousal, bool animated=false);
    void SetState(SelectState selectState);
    void Attack();
    void ShowDmg(int dmg, DmgType dmgType);
    void SetCardDelegate(ICardDelegate cardDelegate);
    void SetTargeted(bool b);
    void SetDelay(float delay);
    void SetUpBodyTargets(HashSet<BodyTarget> bodyTargets);
}
public interface ICardDelegate{
    void MouseEnter();
    void MouseExit();
    void WasPressed();
}
public struct DmgWithType{
    public int _dmg;
    public DmgType _dmgType;
    public DmgWithType(int dmg, DmgType dmgType){
        _dmg = dmg;
        _dmgType = dmgType;
    }
}
