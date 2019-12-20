using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartIndicator : MonoBehaviour {
    public RectTransform _rect;
    public GameObject _iconPrefab;
    public void SetUp(HashSet<MoveType> partsUsed){

        //create an ico for each part used
        int i=0;
        foreach(MoveType m in partsUsed){
            CreateIcon(i);
            i++;
        }

    }
    public void CreateIcon(int index){
        GameObject g = Instantiate(_iconPrefab);
        g.transform.parent = _rect;
        RectTransform rectTransform = g.GetComponent<RectTransform>();

        float padding = 4.0f;
        float iconSize = 20.0f;

        float x1 = -(iconSize+padding)-((20.0f+padding)*index);
        float y1 = -(iconSize+padding);
        float x2 = x1+iconSize;
        float y2 = y1+iconSize;

        rectTransform.offsetMin = new Vector2(x1,y1);
        rectTransform.offsetMax = new Vector2(x2,y2);
    }
}
