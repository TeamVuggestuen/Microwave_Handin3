using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT5_Timer
    {
        private ITimer timer_TM;
        private IDisplay display;
        private IPowerTube powerTube;
        private IOutput output;
        private IUserInterface ui;
        private CookController cookController;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            ui = Substitute.For<IUserInterface>(); // Skal vi lave en substitute, eller skal vi substitue alle UI parametre
            timer_TM = new Timer();
            display = new Display(output);
            powerTube = new PowerTube(output);

            cookController = new CookController(timer_TM, display, powerTube, ui);

        }
        //BlackBox
        //

        //Testing for no throws on ShowTime()
        [Test]
        public void Start__NoThrow()
        {
            Assert.That(() => timer_TM.Start(50), Throws.Nothing);
        }

        [Test]
        public void TimerExpired_displayOutput()
        {
            cookController.StartCooking(50, 3);

            Thread.Sleep(3050);
            output.Received().OutputLine($"PowerTube turned off");

        }

        [Test]
        public void Cooking_TimerExpired_DisplayCorrect()
        {
            cookController.StartCooking(50, 3);

            Thread.Sleep(3000);

            output.Received().OutputLine($"Display shows: {0 / 60:D2}:{0 % 60:D2}");
        }

        [Test]
        public void Cooking_TimerRunning_DisplayCorrect()
        {
            
            cookController.StartCooking(50, 10);

            Thread.Sleep(1100);

            output.Received().OutputLine($"Display shows: {9 / 60:D2}:{9 % 60:D2}");

        }

    }
}
