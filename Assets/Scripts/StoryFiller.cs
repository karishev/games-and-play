using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryFiller
{
    public static StoryNode FillStory()
    {
        var root = CreateNode(
            "You find yourself in a room and you remember nothing. You want to get out",
            new [] {
            "Explore objects",
            "Explore the room"});

        var node1 = CreateNode(
            "There is a chair and a table with a plant to the left. " +
            "There are some bookselves to the right. " +
            "There are some boxes behind.",
            new [] {
            "Go right",
            "Go left",
            "Turn around",
            "Explore the room"});

        var node2 = CreateNode(
            "Nothing interesting here... " +
            "Thought there is a book that looks good..." +
            "Botanics for austranauts?",
            new [] {
            "Explore the rest of the room",
            "Find out more about the book."});

        var node3 = CreateNode(
           "It is about plants...what a surprise. " +
           "There is one called plantus correentis.",
           new [] {
            "Put the book down and explore the other objects in the room."});

        var node4 = CreateNode(
           "Nothing interesting in the boxes, they are filled with books... " +
           "They should be on the shelves.",
           new [] {
            "Go back to explore the rest of the objects in the room."});

        var node5 = CreateNode(
           "Humm, a chair. You have a headache and you sit down.",
           new [] {
            "I want to take a look at the table too."});

        var node6 = CreateNode(
            "Nothing special about the table, there is some soil from the plant. " +
            "The drawers look half open.",
            new [] {
            "Explore the drawers.",
            "Go back to explore the rest of the objects."});

        var node6Bis = CreateNode(
           "Nothing special about the table, there is some soil from the plant. " +
           "The label on the plant says plantus corrientis. " +
           "The drawers look half open.",
           new [] {
            "Explore the drawers",
            "Take a close look at the plant.",
            "Go back to exploring the drawers."});

        var node7 = CreateNode(
          "The drawers are empty, better go back to explore a different object.",
          new [] {
            "Go back to epxlore other objects."});

        var node8 = CreateNode(
           "Upon picking up the plant you find a key. What does this open?",
           new [] {
            "Explore the room."});

        var node9 = CreateNode(
          "The room has two windows and one door.",
          new [] {
            "Go to window #1",
            "Go to window #2",
            "Go to the door"});

        var node10 = CreateNode(
          "The window is covered it cannot open.",
          new [] {
            "Go to the other window.",
            "Go to the door."});

        var node11 = CreateNode(
          "The door is locked with a padlock.",
          new [] {
            "Explore the objects in the room."});

        var node11Bis = CreateNode(
          "The door is locked with a padlock.",
          new [] {
            "Explore the objects in the room.",
            "Use the key."});

        var node12 = CreateNode(
          "You got out of the room!",
          new [] {
            "Exit the game"});

        root.NextNode[0] = node1;
        root.NextNode[1] = node9;

        node1.NextNode[0] = node2;
        node1.NextNode[1] = node5;
        node1.NextNode[2] = node4;
        node1.NextNode[3] = node9;

        node2.NextNode[0] = node1;
        node2.NextNode[1] = node3;

        node3.NextNode[0] = node1;
        node3.OnNodeVisited = () =>
        {
            node5.NextNode[0] = node6Bis;
        };

        node4.NextNode[0] = node1;

        node5.NextNode[0] = node6;

        node6.NextNode[0] = node7;
        node6.NextNode[1] = node1;

        node6Bis.NextNode[0] = node7; 
        node6Bis.NextNode[1] = node8; 
        node6Bis.NextNode[2] = node1;
        
        node7.NextNode[0] = node1;

        node8.NextNode[0] = node9;
        node8.OnNodeVisited = () =>
        {
            node9.NextNode[2] = node10.NextNode[1] = node11Bis;
        };

        node9.NextNode[0] = node9.NextNode[1] = node10;
        node9.NextNode[2] = node11;

        node10.NextNode[0] = node10;
        node10.NextNode[1] = node11;

        node11.NextNode[0] = node1;

        node11Bis.NextNode[0] = node1;
        node11Bis.NextNode[1] = node12;

        node12.IsFinal = true;

        return root;
    }

    private static StoryNode CreateNode(string history, string[] options)
    {
        var node = new StoryNode
        {
            History = history,
            Answers = options,
            NextNode = new StoryNode[options.Length]
        };
        return node;
    
    }
}
