using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICandy
{
    void SelectedCandy();
    void DeselectCandy();
    void SwappingCandy(GameObject newCandy);
}
