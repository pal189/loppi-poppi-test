using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core
{
    public class CalculatorModel
    {
        private string _lastExpression = "";
        private List<string> _history = new List<string>();
        private string _historyFilePath;
        private string _lastExpressionFilePath;

        public CalculatorModel()
        {
            _historyFilePath = Path.Combine(Application.persistentDataPath, "calculator_history.txt");
            _lastExpressionFilePath = Path.Combine(Application.persistentDataPath, "calculator_last_expression.txt");
            LoadHistory();
            LoadLastExpression();
        }

        public bool TryCalculate(string expression)
        {
            _lastExpression = expression;

            if (!IsValidExpression(expression))
            {
                AddToHistory(expression, "ERROR");
                return false;
            }

            try
            {
                string[] parts = expression.Split('+');
                float a = float.Parse(parts[0]);
                float b = float.Parse(parts[1]);
                float result = a + b;
                AddToHistory(expression, result.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            int plusCount = expression.Split('+').Length - 1;
            if (plusCount != 1)
                return false;

            foreach (char c in expression)
            {
                if (!char.IsDigit(c) && c != '+')
                    return false;
            }

            return true;
        }

        private void AddToHistory(string expression, string result)
        {
            string entry = $"{expression} = {result}";
            _history.Add(entry);
            SaveHistory();
        }

        public List<string> GetHistory()
        {
            return _history;
        }

        public string GetLastExpression()
        {
            return _lastExpression;
        }

        public void SaveHistory()
        {
            File.WriteAllLines(_historyFilePath, _history);
        }

        private void LoadHistory()
        {
            if (File.Exists(_historyFilePath))
            {
                _history = new List<string>(File.ReadAllLines(_historyFilePath));
            }
        }

        private void LoadLastExpression()
        {
            if (File.Exists(_lastExpressionFilePath))
            {
                _lastExpression = File.ReadAllText(_lastExpressionFilePath);
            }
        }

        public void SetLastExpression(string text)
        {
            _lastExpression = text;
            SaveLastExpression();
        }

        private void SaveLastExpression()
        {
            File.WriteAllText(_lastExpressionFilePath, _lastExpression);
        }

        public string GetErrorMessage()
        {
            return "Please check the expression you just entered";
        }
    }
}