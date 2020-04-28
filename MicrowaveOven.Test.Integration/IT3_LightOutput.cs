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
    class IT3_LightOutput
    {
        private Light uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();

            uut = new Light(output);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            uut.TurnOn();
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            uut.TurnOff();
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            uut.TurnOn();
            uut.TurnOn();
        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            uut.TurnOff();
        }


    }
}
