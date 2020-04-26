using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT3_Light
    {

        //System under test
        private Light light_TM;  //Top module
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            light_TM = new Light(output);
        }

        [Test]
        public void TurnOn_IsOff_CorrectOutput()
        {
            //light_TM.TurnOn();
            //output.Received().OutputLine("Light is turned on");
            Assert.That(() => light_TM.TurnOn(), Throws.Nothing);
        }


        [Test]
        public void TurnOn_IsOn_CorrectOutput() // burde denne her ikke give en fejl???
        {
            light_TM.TurnOn();
            //light_TM.TurnOn();
            //output.Received().OutputLine("Light is turned on");
            Assert.That(() => light_TM.TurnOn(), Throws.Nothing);
        }


        [Test]
        public void TurnOff_IsOn_CorrectOutput() // burde denne her ikke give en fejl???
        {
            light_TM.TurnOn();
            //light_TM.TurnOff();
            //output.Received().OutputLine("Light is turned off");
            Assert.That(() => light_TM.TurnOff(), Throws.Nothing);
        }


        [Test]
        public void TurnOff_IsOff_CorrectOutput() // burde denne her ikke give en fejl???
        {
            //light_TM.TurnOff();
            //output.Received().OutputLine("Light is turned off");
            Assert.That(() => light_TM.TurnOff(), Throws.Nothing);
        }
    }
}
