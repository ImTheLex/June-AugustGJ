using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shared
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private List<string> _dialogueLines;
        public UnityEvent m_onDialogueEnd;

        private int _currentLineIndex = 0;

        private void OnEnable()
        {
            _currentLineIndex = 0;
            ShowCurrentLine();
        }

        private void ShowCurrentLine()
        {
            if (_currentLineIndex < _dialogueLines.Count)
            {
                _dialogueText.text = _dialogueLines[_currentLineIndex];
            }
            else
            {
                EndDialogue();
            }
        }

        public void NextLine()
        {
            _currentLineIndex++;
            ShowCurrentLine();
        }

        private void EndDialogue()
        {
            _dialogueText.text = "";
            m_onDialogueEnd.Invoke();
            gameObject.SetActive(false);
        }
    }
}
