using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    bool isSelected { get;set;}
    void OnSelect();
    void OnDeselect();
}
