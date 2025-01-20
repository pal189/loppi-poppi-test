using System.Collections.Generic;

namespace Core
{
    public interface ICalculatorView
    {
        void SetInputField(string expression);
        void ClearInputField();
        void UpdateHistory(List<string> history);
        void ShowError(string errorMessage);
    }
}