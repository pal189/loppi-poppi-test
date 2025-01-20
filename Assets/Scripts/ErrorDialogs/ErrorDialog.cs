using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorDialogs
{
    public class ErrorDialog : MonoBehaviour
    {
        [SerializeField] private TMP_Text _errorMessageText;
        [SerializeField] private Button _closeButton;

        public void Show(string message)
        {
            _errorMessageText.text = message;
            gameObject.SetActive(true);

            // Назначаем обработчик для кнопки закрытия
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}