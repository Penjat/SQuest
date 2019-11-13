using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour, MoveCategoryDelegate {
    public CategoryManagerDelegate _delegate;
    public MoveCategory[] _categories;

    public void SetUp(CategoryManagerDelegate categoryManagerDelegate){
        _delegate = categoryManagerDelegate;
        foreach(MoveCategory m in _categories){
            m.SetUp(this);
        }
    }
    public void CategoryPressed(MoveType moveType){
        _delegate.OpenCategory(moveType);
    }
    public void CheckCategories(HashSet<MoveType> usedParts){
        foreach(MoveCategory m in _categories){
            bool isLocked = usedParts.Contains(m._type);
            m.SetLocked(isLocked);
        }
    }
    public void CancelMove(MoveType moveType){

    }
}

public interface CategoryManagerDelegate{
    void OpenCategory(MoveType moveType);
}
