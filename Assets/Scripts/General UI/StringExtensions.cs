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

                case Entity.INSTRUCTIONS:
                str1 = "<size=18><color=grey>";
                str2 = "</color></size>";
                break;

                case Entity.PLAYER:
                str1 = "<color=#F00C93>";
                str2 = "</color>";
                break;

            }
            return str1 + str + str2;
        }
    }
}
public enum Entity{
    ENEMY, MOVE, INSTRUCTIONS, PLAYER
}
