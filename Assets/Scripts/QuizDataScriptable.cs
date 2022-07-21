using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="QuestionData")]
public class QuizDataScriptable : ScriptableObject
{
    public List<Question> questions;
}
