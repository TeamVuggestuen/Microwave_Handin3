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

namespace MicrowaveOven.Test.Integration
{
    class IT4_CookController
    {
        private CookController cookController;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powertubeTM;
        private IOutput output;
        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = new Display(output);
            powertubeTM = new PowerTube(output);

            cookController = new CookController(timer, display, powertubeTM, ui);
        }

        [Test]
        public void StartCooking_UnvalidParameters_Throw_exception()
        {
            
            Assert.That(() => cookController.StartCooking(350, 60), Throws.Exception);
            
        }


        [Test]
        public void Cooking_TimerTick_DisplayShow()
        {
            cookController.StartCooking(50, 60);

            timer.TimeRemaining.Returns(10);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine($"Display shows: {10 / 60:D2}:{10 % 60:D2}");
        }


        [Test]
        public void Cooking_TimerExpired_DisplayTurnedOff()
        {
            cookController.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine($"PowerTube turned off");
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            cookController.StartCooking(50, 60);
            cookController.Stop();

            output.Received().OutputLine($"PowerTube turned off");
        }

    }
}