using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrbActions
{
    void SetTargetPosition(Vector3 position);
    void MoveToTarget();
    void WaitForPassage();
}
