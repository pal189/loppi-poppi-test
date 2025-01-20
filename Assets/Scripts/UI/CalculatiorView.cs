using System.Collections.Generic;
using Core;
using ErrorDialogs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _resultButton;

        private CalculatorPresenter _presenter;
        private ErrorDialog _errorDialog;

        public void Initialize(CalculatorPresenter presenter, ErrorDialog errorDialog)
        {
            _presenter = presenter;
            _errorDialog = errorDialog;

            // Назначаем обработчик для кнопки "Result"
            _resultButton.onClick.AddListener(OnResultButtonClicked);
            _inputField.onValueChanged.AddListener(OnInputFieldChanged);
            _inputField.text = "";
            _resultText.text = "";
        }

        private void OnResultButtonClicked()
        {
            if(_inputField.text == "")
                return;
            
            _presenter.OnResultButtonClicked(_inputField.text);
        }
        
        private void OnInputFieldChanged(string text)
        {
            _presenter.OnInputFieldChanged(text);
        }

        public void SetInputField(string expression)
        {
            _inputField.text = expression;
        }

        public void ClearInputField()
        {
            _inputField.text = "";
        }

        public void UpdateHistory(List<string> history)
        {
            _resultText.text = "";
            
            for (int i = history.Count - 1; i >= 0; i--)
            {
                _resultText.text += $"{history[i]}\n";
            }
            
        }

        public void ShowError(string errorMessage)
        {
            _errorDialog.Show(errorMessage);
        }
    }
}