using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryNode
{
    public string History;
    public string[] Answers;
    public bool IsFinal;
    public StoryNode[] NextNode;
    public Action OnNodeVisited;
}
