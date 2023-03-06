using RPNCalculator;
using System;

namespace RPNCalulator {
	public class RPN {
		private Stack<int> _operators;

		public int EvalRPN(string input) {
			_operators = new Stack<int>();

			var splitInput = input.Split(' ');
			foreach (var op in splitInput)
			{
				if (parseNumber(op, out int val))
                    _operators.Push(val);
                else if (Definitions._1paramOperations.ContainsKey(op))
                {
                    var num1 = _operators.Pop();
                    _operators.Push(Definitions._1paramOperations[op](num1));
                }
                else if (Definitions._2paramOperations.ContainsKey(op))
				{
					var num1 = _operators.Pop();
					var num2 = _operators.Pop();
					_operators.Push(Definitions._2paramOperations[op](num1, num2));
				}
				else
                    throw new InvalidOperationException();
            }

			var result = _operators.Pop();
			if (_operators.IsEmpty)
			{
				return result;
			}
			throw new InvalidOperationException();
		}

		private bool IsNumber(String input) => Int32.TryParse(input, out _);

        private bool parseNumber(String input, out int ret)
        {
			ret = 0;
			int _base;

            if (IsNumber(input))
            {
                _base = 10;
            }
            else if (Definitions.baseIndicator.ContainsKey(input[0]))
			{
				_base = Definitions.baseIndicator[input[0]];
				input = input.Substring(1);
			}
			else
				return false;

            ret = Convert.ToInt32(input, _base);
			return true;
        }

		private Func<int, int, int> Operation(String input) =>
			(x, y) =>
			(
				(input.Equals("+") ? x + y :
					(input.Equals("*") ? x * y : int.MinValue)
				)
			);
	}
}