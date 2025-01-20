namespace Core
{
    public class CalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly CalculatorModel _model;

        public CalculatorPresenter(ICalculatorView view, CalculatorModel model)
        {
            _view = view;
            _model = model;
        }

        public void OnResultButtonClicked(string expression)
        {
            if (!_model.TryCalculate(expression))
                _view.ShowError(_model.GetErrorMessage());

            _view.ClearInputField();
            _view.UpdateHistory(_model.GetHistory());
        }

        public void RestoreState()
        {
            // Восстанавливаем последнее введенное выражение
            _view.SetInputField(_model.GetLastExpression());

            // Восстанавливаем историю (если нужно)
            _view.UpdateHistory(_model.GetHistory());
        }

        public void OnInputFieldChanged(string text)
        {
            _model.SetLastExpression(text);
        }

        public void OnApplicationQuit()
        {
            _model.SaveHistory();
        }
    }
}