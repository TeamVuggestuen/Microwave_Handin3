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
    public class IT1_PowerTubeOutput
    {
        private IOutput output;
        private IPowerTube uut;

        [SetUp]
        public void SetUp()
        {
            output = new Output();
            uut = new PowerTube(output);
        }

        [TestCase(50)]
        [TestCase(700)]
        public void TurnOn_WasOff_CorrectOutput(int power)
        {
            uut.TurnOn(power);
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
        }
    }
}
