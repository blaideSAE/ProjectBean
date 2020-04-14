using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Question",menuName = "BeanQuestion")]
public class BeanQuestion : ScriptableObject
{
    public new string name;
    public string body;
    public List<string> possibleAnswers;


}
