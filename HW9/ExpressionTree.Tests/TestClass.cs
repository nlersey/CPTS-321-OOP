
//using NUnit.Framework;
//using System.Collections;
//using System.Collections.Generic;
//using CPTS321;


//namespace CPTS321.Tests
//{
//    [TestFixture]
//    public class TestClass
//    {
//        [Test]
//        public void TestAddition()
//        {
//            ExpressionTree tree = new ExpressionTree("1+1");
//            Assert.That(tree.Evaluate() == 2);
//        }

//        public void TestSubtraction()
//        {
//            ExpressionTree tree = new ExpressionTree("1-1");
//            Assert.That(tree.Evaluate() == 0);
//        }

//        public void TestMutlipcation()
//        {
//            ExpressionTree tree = new ExpressionTree("2*2");
//            Assert.That(tree.Evaluate() == 4);
//        }

//        public void TestDivison()
//        {
//            ExpressionTree tree = new ExpressionTree("6/2");
//            Assert.That(tree.Evaluate() == 3);
//        }

//        public void TestMutliVariableMutliOperator()
//        {
//            ExpressionTree tree = new ExpressionTree("2+2*3");
//            Assert.That(tree.Evaluate() == 8);
//        }

//        public void TestMutliVariableMutliOperatorWithParnthesis()
//        {
//            ExpressionTree tree = new ExpressionTree("(2+2)*3");
//            Assert.That(tree.Evaluate() == 12);
//        }

//        public void TestMutliVariablewithLetters()
//        {
//            ExpressionTree tree = new ExpressionTree("A1+12+B2");
//            Assert.That(tree.Evaluate() == 12);
//        }
//    }
//}
