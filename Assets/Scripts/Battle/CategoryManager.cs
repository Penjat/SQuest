using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public void CheckCategories(IDictionary<IBodyPart,Move> usedParts){
        //called to see which categories should be locked/unlocked
        foreach(MoveCategory moveCategory in _categories){
            //for each category, check if the body part is in used parts
            bool isLocked = usedParts.ContainsKey(moveCategory.GetBodyPart());
            moveCategory.SetLocked(isLocked);
            if(isLocked){
                //if it is locked make sure the move is set for it
                moveCategory.SetMove(usedParts[moveCategory.GetBodyPart()]);
            }
        }
    }
    public void CheckAvailableCategories(HashSet<MoveType> avaibleMoveTypes){
        foreach(MoveCategory moveCategory in _categories){
            bool isAvailable =  avaibleMoveTypes.Contains(moveCategory.GetMoveType());
            moveCategory.SetAvailable(isAvailable);
        }
    }
    public void CancelMove(IBodyPart bodyPart){
        _delegate.CancelMove(bodyPart);
    }
    public void ShowTargets(IBodyPart bodyPart, Move selectedMove){
        _delegate.ShowTargets(bodyPart, selectedMove);
    }
    public void HideTargets(){
        _delegate.HideTargets();
    }
    public void ClimaxOn(IEnemy enemy){
        //TODO just random for now
        int random = Random.Range(0,_categories.Count);
        MoveType moveType = enemy.GetMoveTypeClimax();
        MoveCategory category = _categories.First(x => x.GetBodyPart().GetMoveType() == moveType);
        //just in case
        if(category == null){
            return;
        }
        category.ClimaxOn();
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
    void CancelMove(IBodyPart bodyPart);
    void ShowTargets(IBodyPart bodyPart, Move selectedMove);
    void HideTargets();
}
