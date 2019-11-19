using System.Collections;
using System.Collections.Generic;

namespace StringMethods{
    public static class StringExtensions
    {
        public static string ColorFor(this string str, Entity entity){
            //place the correct tags on the string
            string str1 = "";
            string str2 = "";
            switch(entity){
                case Entity.ENEMY:
                str1 = "<b><color=red>";
                str2 = "</color></b>";
                break;

                case Entity.MOVE:
                str1 = "<b><color=white>";
                str2 = "</color></b>";
                break;
            }
            return str1 + str + str2;
        }
    }
}
public enum Entity{
    ENEMY, MOVE
}
