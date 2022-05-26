using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DelayToInvoke{
    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds) {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}