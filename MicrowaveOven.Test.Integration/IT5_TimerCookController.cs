using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Test.Integration
{
    class IT5_TimerCookController
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            ui = Substitute.For<IUserInterface>();
            timer = new Timer();
            display = new Display(output);
            powerTube = new PowerTube(output);

            uut = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 3);

            Thread.Sleep(3100);
            output.Received().OutputLine($"PowerTube turned off");
        }

        [TestCase]
        public void Cooking_TimerRunning_DisplayCorrect()
        {
           uut.StartCooking(50, 60);

           Thread.Sleep(1100);
           
           output.Received().OutputLine($"Display shows: {59 / 60:D2}:{59 % 60:D2}");

        }

        [Test]
        public void Cooking_TimerExpired_DisplayCorrect()
        {
            uut.StartCooking(50, 3);

            Thread.Sleep(3100);

            output.Received().OutputLine($"Display shows: {0 / 60:D2}:{0 % 60:D2}");
        }

    }

}
