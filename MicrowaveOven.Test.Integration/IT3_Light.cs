using System;
using System.Collections.Generic;
using System.IO;
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
        private StringWriter consoleOut;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            light_TM = new Light(output);
            consoleOut = new StringWriter();
        }


        //------------------ PowerTube & Output ---------------------

        //NB:
        //(1) Both PowerTube & Output needs to be non substituted
        //(2) =>  whitebox system perspective: can only test by checking console output
        //(3) Testing as if string outputline is not known => using: private StringWriter consoleOut;


        [Test]
        public void TurnOn_IsOff_CorrectOutput()
        {
            Console.SetOut(consoleOut);
            light_TM.TurnOn();
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"Light is turned on"));
        }


        [Test]
        public void TurnOn_IsOn_CorrectOutput() 
        {
            light_TM.TurnOn();
            Console.SetOut(consoleOut);
            light_TM.TurnOn();
            Assert.That(consoleOut.ToString().Length == 0);
        }


        [Test]
        public void TurnOff_IsOn_CorrectOutput() 
        {
            light_TM.TurnOn();
            Console.SetOut(consoleOut);
            light_TM.TurnOff();
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"Light is turned off"));
        }


        [Test]
        public void TurnOff_IsOff_CorrectOutput() 
        {
            Console.SetOut(consoleOut);
            light_TM.TurnOff();
            Assert.That(consoleOut.ToString().Length == 0);
            //Assert.That(() => light_TM.TurnOff(), Throws.Nothing);
        }
    }
}
