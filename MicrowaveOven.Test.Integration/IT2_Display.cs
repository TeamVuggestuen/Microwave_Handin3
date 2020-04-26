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
    
    //---------------------NB-----------------------------
    //(1) PowerTube set as IT1 as hardware dependency implies bottom up strategy and PowerTube relies on hardware/(power)
    //(2) Remember BLACKBOX perspective!
    //(3) Top module(powertube) has no return values => testing solely on no throws and correct output
    //(4) Testing on output is partly whitebox testing since output cannot be known simply be PowerTube's perspective
    //    However it is included here to test class Output simultaneously as this is (understandably so) not included in UT
    //    Otherwise test would be restricted to testing for throws


    [TestFixture]
    class IT2_Display
    {
        //System Under Test
        private Display display_TM;  //Top module
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            display_TM = new Display(output);
        }

        //-----------------Testing for correct output on function calls (Whitebox)------------------------------
        //already covered in UT but only on output as substitute
        [TestCase(10,10)]
        [TestCase(0, 0)]
        public void ShowTime_MinutesSeconds_CorrectOutput(int min, int sec)
        {
            //WHiTEBOX (known output)!
            //string should be output twice
            display_TM.ShowTime(min, sec);
            output.Received(2).OutputLine($"Display shows{min:D2}:{sec:D2}"); 
        }


        [TestCase(100)]
        [TestCase(0)]
        public void ShowPower_Power_CorrectOutput(int power)
        {
            //WHITEBOX (known output)!
            //string should be output twice
            display_TM.ShowPower(power);
            output.Received(2).OutputLine($"Display shows: {power}");
        }

        [TestCase]
        public void Clear_CorrectOutput()
        {
            //WHITEBOX (known output)!
            //string should be output once
            display_TM.Clear();
            output.Received(1).OutputLine($"Display cleared");
        }



        //------------------Testing for no throws on function calls (Blackbox)----------------------------
        //however upon system perspective we do know that class Display does not contain exceptions
        [TestCase(1,2)]
        //Testing for no throws on ShowTime()
        public void ShowTime__NoThrow(int min, int sec)
        {
            Assert.That(() => display_TM.ShowTime(min, sec), Throws.Nothing);
        }


        [TestCase(10)]
        //Testing for no throws on ShowPower()
        public void ShowPower__NoThrow(int power)
        {
            Assert.That(() => display_TM.ShowPower(power), Throws.Nothing);
        }


        [TestCase(10)]
        //Testing for no throws on Clear()
        public void Clear__NoThrow(int power)
        {
            Assert.That(() => display_TM.ShowPower(power), Throws.Nothing);
        }
    }
}
