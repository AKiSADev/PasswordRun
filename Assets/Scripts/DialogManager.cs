using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	public Text dialogueText;
	public GameObject buttonBack;
	public Button next;

	private Queue<string> sentences = new Queue<string>();


	public void StartDialogue(Dialog dialogue)
	{


		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
		}
		else
		{
			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{

		buttonBack.SetActive(true);


		next.GetComponentInChildren<Text>().text = "Play >>";
		next.onClick.AddListener(delegate
		{
			nextScene();
		}
		
		);
		
	}

	public void GoBackHome()
	{
		SceneManager.LoadScene("MainMenu");
	}

void nextScene()
	{
		SceneManager.LoadScene("MainGame");
	}
}
