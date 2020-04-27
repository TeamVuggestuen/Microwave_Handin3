using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    public class IT1_PowerTube
    {
        //NB
        //(1) PowerTube set as IT1 as hardware dependency implies bottom up strategy and PowerTube relies on hardware/(power) and uses bottom most module Output
        //(2) Remember BLACKBOX perspective!
        //(3) Interval (EET) & exceptions/throws already tested in UT
        //(4) Top module(powertube)neither Output has return values => testing solely on no throws and correct console output
        //    => adding private StringWriter consoleOut;

        //(5) Testing on output is partly whitebox testing since output cannot be known simply be PowerTube's perspective
        //    However it is included here to test class Output simultaneously as this is (understandably so) not included in UT
        //    Otherwise test would be restricted to testing for throws

        //System under test
        private PowerTube powertubeTM; //TM = Top module
        private IOutput output;
        private StringWriter consoleOut;



        [SetUp]
        //Setting up System Under Test
        public void Setup()
        {
            output = new Output();
            powertubeTM = new PowerTube(output);
            consoleOut = new StringWriter();
        }


        //------------------ PowerTube & Output ---------------------
        //NB:
        //(1)Whitebox system under test perspective:
        //(2)Integrating PowerTube & Output => cannot be substituted
        //(3)Therefore only possible to check methods by console output
        //(4)Not sure if output string is known in blackbox testing

        //Legal interval betwen: 1-100
        [TestCase(1)]
        [TestCase(100)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            Console.SetOut(consoleOut);
            powertubeTM.TurnOn(power);
            Assert.That(consoleOut.ToString().Length > 0);
                //Equals($"PowerTube works with {power}\r\n"));
        }

        
        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            powertubeTM.TurnOn(50); //unless powertube IsOn=true; no action/output on TurnOff() 
            
            Console.SetOut(consoleOut);
            powertubeTM.TurnOff();
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"PowerTube turned off"));
        }


        // ALTERNATIVE: test for no throws
        [Test]
        //Testing for no exceptions thrown with legal interval value
        public void TurnOn_WasOffCorrectpower_NoThrows()
        {
            Assert.That(() => powertubeTM.TurnOn(50), Throws.Nothing);
        }

        [Test]
        //Testing for no throws on TurnOff()
        public void TurnOff_WasOn_NoThrows()
        {
            powertubeTM.TurnOn(50); //unless powertube IsOn=true; no action/output on TurnOff() 

            Assert.That(() => powertubeTM.TurnOff(), Throws.Nothing);
        }


        //------------------ PowerTube & ArgumentOutOfRangeException ---------------------
        //Testing for PowerTube & ArgumentOutOfRangeException
        //Legal interval betwen: 1-100
        [TestCase(0)]
        [TestCase(200)]
        public void TurnOn_WasOffIncorrectPower_ThrowsException(int power)
        {
            powertubeTM.TurnOn(50);
            Assert.That(() => powertubeTM.TurnOn(power), Throws.Exception);
        }


        //------------------ PowerTube & ApplicationException ---------------------
        //Testing for PowerTube & ApplicationException
        //Legal interval betwen: 1-100
        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            powertubeTM.TurnOn(50);
            Assert.That(() => powertubeTM.TurnOn(75), Throws.Exception);
        }

    }
}
