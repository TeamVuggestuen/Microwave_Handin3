using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    class IT4_CookController
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
            timer = Substitute.For<ITimer>();
            display = new Display(output);
            powerTube = new PowerTube(output);

            uut = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(350, 60);
            output.Received().OutputLine($"PowerTube works with {50} %");
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115000);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine($"Display shows: {1:D2}:{55:D2}");
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine($"PowerTube turned off");
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            output.Received().OutputLine($"PowerTube turned off");
        }

    }
}
