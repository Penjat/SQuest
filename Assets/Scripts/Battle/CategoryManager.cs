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
    public void CheckCategories(IDictionary<MoveType,Move> usedParts){
        foreach(MoveCategory m in _categories){
            bool isLocked = usedParts.ContainsKey(m._type);
            m.SetLocked(isLocked);
            if(isLocked){
                m.SetMove(usedParts[m._type]);
            }
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
}

public interface CategoryManagerDelegate{
    void OpenCategory(MoveType moveType);
    void CancelMove(MoveType moveType);
    void ShowTargets(Move selectedMove);
    void HideTargets();
}
