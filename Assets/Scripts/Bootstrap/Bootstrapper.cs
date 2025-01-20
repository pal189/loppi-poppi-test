using Core;
using ErrorDialogs;
using UI;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CalculatorView _calculatorViewPrefab;
        [SerializeField] private ErrorDialog _errorDialogPrefab;
        [SerializeField] private Transform _uiRoot;

        private CalculatorPresenter _presenter;

        private void Start()
        {
            // Создаем экземпляр View
            CalculatorView view = Instantiate(_calculatorViewPrefab, _uiRoot);

            // Создаем экземпляр диалога ошибки
            ErrorDialog errorDialog = Instantiate(_errorDialogPrefab, _uiRoot);
            errorDialog.gameObject.SetActive(false);

            // Создаем Model
            CalculatorModel model = new CalculatorModel();

            // Создаем Presenter и связываем его с View и Model
            _presenter = new CalculatorPresenter(view, model);

            // Инициализируем View
            view.Initialize(_presenter, errorDialog);

            // Восстанавливаем состояние приложения
            _presenter.RestoreState();
        }

        private void OnApplicationQuit()
        {
          _presenter.OnApplicationQuit();
        }
    }
}