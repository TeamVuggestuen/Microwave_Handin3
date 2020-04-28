using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT2_DisplayOutput
    {
        private Display uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            uut = new Display(output);
        }

        [TestCase(0,0)]
        [TestCase(0,5)]
        [TestCase(5,0)]
        [TestCase(10,15)]
        public void ShowTime_CorrectOutput(int minute, int second)
        {
            uut.ShowTime(minute, second);
        }

        [TestCase(0)]
        [TestCase(150)]
        public void ShowPower_CorrectOutput(int power)
        {
            uut.ShowPower(power);
        }

        [Test]
        public void Clear_CorrectOutput()
        {
            uut.Clear();
        }

    }
}
