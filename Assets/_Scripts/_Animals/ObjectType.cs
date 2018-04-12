using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Not useful for now, prolly will become useful when types get complicated
 */
public class ObjectType : MonoBehaviour {

    public static int PlayerHand = 0;
    public static int Food = 1;

    public static int GetType(GameObject obj) {
        string tag = obj.gameObject.tag;
        if (tag.Equals("Food")) {
            return 1;
        }

        if ( tag.Equals("Player") ) {
            return 0;
        }

        return -1;
    }
}
