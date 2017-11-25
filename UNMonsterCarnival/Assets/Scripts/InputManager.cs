using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    GameObject PosessedObject;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_STANDALONE_OSX
    KeyCode Key_Attack;
    KeyCode Key_SingleSkill;
    KeyCode Key_MultipleSkill;
    KeyCode Key_SpeedSkill;

    KeyCode Key_Pause;

    private void Start()
    {
        Key_Attack = KeyCode.Space;
        Key_SingleSkill = KeyCode.A;
        Key_MultipleSkill = KeyCode.S;
        Key_SpeedSkill = KeyCode.D;

        Key_Pause = KeyCode.Escape;
    }
#endif

#if UNITY_ANDROID
#endif
}
