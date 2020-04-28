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

    //---------------------NB-----------------------------
    //(1) Display set as IT2 as class/module is parallel to PowereTube in dependency tree and uses bottom most module Output
    //(2) Remember BLACKBOX perspective!
    //(3) From whitebox system perspective: Top module(Display) neither Output has return values => testing solely on no throws and correct output
    //(4) Testing on output is partly whitebox testing since output cannot be known simply be PowerTube's perspective
    //    => adding private StringWriter consoleOut;
    //    Otherwise test would be restricted to testing for throws


    [TestFixture]
    class IT2_Display
    {
        //System Under Test
        private Display display_TM;  //Top module
        private IOutput output;
        private StringWriter consoleOut;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            display_TM = new Display(output);
            consoleOut = new StringWriter();
        }

        //------------------ Display & Output ---------------------
        //NB:
        //(1)Whitebox system under test perspective:
        //(2)Integrating Display & Output => cannot be substituted
        //(3)Therefore only possible to check methods by console output
        //(4)Not sure if output string is known in blackbox testing
        //(5)already covered in UT but only on output as substitute

        [TestCase(10,10)]
        [TestCase(0, 0)]
        public void ShowTime_MinutesSeconds_CorrectOutput(int min, int sec)
        {
            Console.SetOut(consoleOut);
            display_TM.ShowTime(min, sec);
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"Display shows{min:D2}:{sec:D2"));
        }


        [TestCase(100)]
        [TestCase(0)]
        public void ShowPower_Power_CorrectOutput(int power)
        {
            Console.SetOut(consoleOut);
            display_TM.ShowPower(power);
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"Display shows: {power}"));
        }

        [TestCase]
        public void Clear_CorrectOutput()
        {
            Console.SetOut(consoleOut);
            display_TM.Clear();
            Assert.That(consoleOut.ToString().Length > 0);
            //Equals($"Display cleared"));
        }


        //----------------------------ALTERNATIVE------------------------------------
        //------------------Testing for no throws on function calls (Blackbox)----------------------------
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


        [Test]
        //Testing for no throws on Clear()
        public void Clear__NoThrow()
        {
            Assert.That(() => display_TM.Clear(), Throws.Nothing);
        }
    }
}
