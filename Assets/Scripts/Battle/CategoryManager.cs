using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour, MoveCategoryDelegate {
    public CategoryManagerDelegate _delegate;
    private List<MoveCategory> _categories = new List<MoveCategory>();
    public GameObject _moveCategoryPrefab;
    public Transform _catagoryContainer;

    public Sprite[] _categoryButtonSprites;
    private int HAND = 0;
    private int MOUTH = 1;
    private int ASS = 2;
    private int BREASTS = 3;
    private int VAGINA = 4;
    private int FEET = 5;


    public void SetUp(CategoryManagerDelegate categoryManagerDelegate, List<IBodyPart> bodyParts){
        _delegate = categoryManagerDelegate;

        //create a button for every body part the player has
        foreach(IBodyPart bodyPart in bodyParts){
            GameObject g = Instantiate(_moveCategoryPrefab);
            g.transform.SetParent(_catagoryContainer);
            MoveCategory moveCategory = g.GetComponent<MoveCategory>();
            _categories.Add(moveCategory);
            Sprite sprite = GetSprite(bodyPart.GetMoveType());
            moveCategory.SetUp(this, bodyPart, sprite);
        }
    }
    public void CategoryPressed(IBodyPart bodyPart){
        _delegate.OpenCategory(bodyPart);
    }
    public void CheckCategories(IDictionary<MoveType,Move> usedParts){
        foreach(MoveCategory m in _categories){
            bool isLocked = usedParts.ContainsKey(m.GetMoveType());
            m.SetLocked(isLocked);
            if(isLocked){
                m.SetMove(usedParts[m.GetMoveType()]);
            }
        }
    }
    public void CheckAvailableCategories(HashSet<MoveType> avaibleMoveTypes){
        foreach(MoveCategory moveCategory in _categories){
            bool isAvailable =  avaibleMoveTypes.Contains(moveCategory.GetMoveType());
            moveCategory.SetAvailable(isAvailable);
        }
    }
    public void CancelMove(MoveType moveType){
        _delegate.CancelMove(moveType);
    }
    public void ShowTargets(Move selectedMove){
        _delegate.ShowTargets(selectedMove);
    }
    public void HideTargets(){
        _delegate.HideTargets();
    }
    private Sprite GetSprite(MoveType moveType){
        switch(moveType){
            case MoveType.Hand:
            return _categoryButtonSprites[HAND];
            case MoveType.Mouth:
            return _categoryButtonSprites[MOUTH];
            case MoveType.Ass:
            return _categoryButtonSprites[ASS];
            case MoveType.Breasts:
            return _categoryButtonSprites[BREASTS];
            case MoveType.Vagina:
            return _categoryButtonSprites[VAGINA];
            case MoveType.Feet:
            return _categoryButtonSprites[FEET];
        }
        return _categoryButtonSprites[HAND];
    }
}

public interface CategoryManagerDelegate{
    void OpenCategory(IBodyPart bodyPart);
    void CancelMove(MoveType moveType);
    void ShowTargets(Move selectedMove);
    void HideTargets();
}
