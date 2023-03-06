using NUnit.Framework;
using RPNCalulator;
using System;

namespace RPNTest
{
	[TestFixture]
	public class RPNTest
	{
		private RPN _sut;
		[SetUp]
		public void Setup()
		{
			_sut = new RPN();
		}
		[Test]
		public void CheckIfTestWorks()
		{
			Assert.Pass();
		}

		[Test]
		public void CheckIfCanCreateSut()
		{
			Assert.That(_sut, Is.Not.Null);
		}

		[Test]
		public void SingleDigitOneInputOneReturn()
		{
			var result = _sut.EvalRPN("1");

			Assert.That(result, Is.EqualTo(1));

		}
		[Test]
		public void SingleDigitOtherThenOneInputNumberReturn()
		{
			var result = _sut.EvalRPN("2");

			Assert.That(result, Is.EqualTo(2));

		}
		[Test]
		public void TwoDigitsNumberInputNumberReturn()
		{
			var result = _sut.EvalRPN("12");

			Assert.That(result, Is.EqualTo(12));

		}
		[Test]
		public void TwoNumbersGivenWithoutOperator_ThrowsExcepton()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("1 2"));

		}
		[Test]
		public void OperatorPlus_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("1 2 +");

			Assert.That(result, Is.EqualTo(3));
		}
		[Test]
		public void OperatorTimes_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("2 2 *");

			Assert.That(result, Is.EqualTo(4));
		}
		[Test]
		public void OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("1 2 -");

			Assert.That(result, Is.EqualTo(-1));
		}
		[Test]
        public void OperatorDivide_DividingTwoNumbers_ReturnCorrectResult()
        {
            var result = _sut.EvalRPN("56 7 /");

            Assert.That(result, Is.EqualTo(8));
        }
        [Test]
        public void OperatorDivide_DividingByZero()
        {
            Assert.Throws<System.DivideByZeroException>(delegate { _sut.EvalRPN("56 0 /"); });
        }
        [Test]
		public void ComplexExpression()
		{
			var result = _sut.EvalRPN("15 7 1 1 + - / 3 * 2 1 1 + + -");
            Assert.That(result, Is.EqualTo(5));
		}
		[Test] 
		public void Binary()
		{
			var result = _sut.EvalRPN("b1001");
			Assert.That(result, Is.EqualTo(9));
		}
        [Test]
        public void BinaryFail()
        {
			Assert.Throws<System.FormatException>(delegate { _sut.EvalRPN("b1401"); });
        }
        [Test]
        public void Hex()
        {
            var result = _sut.EvalRPN("#a4");
            Assert.That(result, Is.EqualTo(164));
        }
        [Test]
        public void Octal()
        {
            var result = _sut.EvalRPN("o12");
            Assert.That(result, Is.EqualTo(10));
        }
        [Test]
        public void HexFail()
        {
            Assert.Throws<System.FormatException>(delegate { _sut.EvalRPN("#abh"); });
        }
		[Test]
		public void Factorial()
		{
            var result = _sut.EvalRPN("5 !");
			Assert.That(result, Is.EqualTo(120));
		}
        [Test]
        public void Absolute()
        {
            var result = _sut.EvalRPN("-5 ||");
            Assert.That(result, Is.EqualTo(5));
        }
        [Test]
        public void ComplexTest2()
        {
            var result = _sut.EvalRPN("#ba 4 ! + 200 3 * 2 / b1000110 + - ||");
            Assert.That(result, Is.EqualTo(160));
        }
        [Test]
        public void ComplexTest2Fail()
        {
            Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("#ba 4 ! + 200 3 * 2 / b1000110 + - | *"));
        }
    }
}
