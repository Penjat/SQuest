using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    //increasing powers of 2
    public static int TITLE = 1;
    public static int BATTLE = 2;

    public Menu[] menus;

    public void NavigateTo(int index) {
        for (int i=0; i<menus.Length; i++) {
            int pos = 1 << i;
            bool isActive = (pos & index) != 0;
            menus[i].SetActive(isActive);
        }
    }
}
