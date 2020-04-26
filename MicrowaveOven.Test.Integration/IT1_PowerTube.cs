using System;
using System.Collections.Generic;
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
        //(1) PowerTube set as IT1 as hardware dependency implies bottom up strategy and PowerTube relies on hardware/(power)
        //(2) Remember BLACKBOX perspective!
        //(3) Interval (EET) & exceptions/throws already tested in UT
        //(4) Top module(powertube) has no return values => testing solely on no throws and correct output
        //(5) Testing on output is partly whitebox testing since output cannot be known simply be PowerTube's perspective
        //    However it is included here to test class Output simultaneously as this is (understandably so) not included in UT
        //    Otherwise test would be restricted to testing for throws

        //System under test
        private PowerTube powertubeTM; //TM = Top module
        private IOutput output;


        [SetUp]
        //Setting up System Under Test
        public void Setup()
        {
            output = new Output();
            powertubeTM = new PowerTube(output);
        }


        [TestCase(50)]
        [TestCase(500)]
        //Legal interval betwen: 1-100
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            //String should only be output once (received(1)), since second value is out of legal interval
            powertubeTM.TurnOn(power);
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains($"{power}"))); 
        }


        [TestCase(70)]
        //Testing for no exceptions thrown with legal interval value
        public void TurnOn_WasOffCorrectpower_NoThrows(int power)
        {
            Assert.That(() => powertubeTM.TurnOn(power), Throws.Nothing);
        }


        [Test]
        //Testing for no throws on TurnOff()
        public void TurnOff_WasOff_NoOutput()
        {
            Assert.That(() => powertubeTM.TurnOff(), Throws.Nothing);
        }


        //Or (while also testing output):
        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            powertubeTM.TurnOff();
            output.Received(1).OutputLine(($"PowerTube turned off"));
        }
    }
}
