using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    public Text HistoryText;
    public Transform AnswersParent;
    public GameObject ButtonAnswerPrefab;
    public Stack<StoryNode> NodeHistory;

    private StoryNode currentNode;

    private void Start()
    {
        currentNode = StoryFiller.FillStory();

        NodeHistory = new Stack<StoryNode>();
        HistoryText.text = string.Empty;
        FillUi();
    }

    void FillUi()
    {
        // I decided to just clean up everything in the game history, but I want to add an option to go back to previous
        HistoryText.text += "\n\n" + currentNode.History;

        foreach (Transform child in AnswersParent.transform)
        {
            Destroy(child.gameObject);
        }

        var isLeft = true;
        var height = 50.0f;
        var index = 0;
        foreach (var answer in currentNode.Answers)
        {
            var buttonAnswerCopy = Instantiate(ButtonAnswerPrefab, AnswersParent, true);

            var x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.3f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft)
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3.0f;
            isLeft = !isLeft;

            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);

            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;
        }
        if (NodeHistory.Count > 0)
        {
            var buttonAnswerCopy = Instantiate(ButtonAnswerPrefab, AnswersParent, true);
            // if the height is not changed since there are odd numbers of answer -> add height
            if (index == 3 || index == 1)
            {
                height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 3.0f;
            }
            var x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 0.1f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition = new Vector3(x, height, 0);

            FillListener(buttonAnswerCopy.GetComponent<Button>(), -1);

            buttonAnswerCopy.GetComponentInChildren<Text>().text = "Get Back";
        }
    }

    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() => { AnswerSelected(index); });
    }

    private void AnswerSelected(int index)
    {
        if (index == -1) {
            HistoryText.text = "\nYou returned back";
            currentNode = NodeHistory.Pop();
            FillUi();
        }
        else{
            NodeHistory.Push(currentNode);

            HistoryText.text = "\n" + currentNode.Answers[index];

            if (!currentNode.IsFinal)
            {
                currentNode = currentNode.NextNode[index];

                currentNode.OnNodeVisited?.Invoke();

                FillUi();
            }
            else
            {
                HistoryText.text += "\n" + "PRESS ESC TO CONTINUE";
            }
        }
    }
}