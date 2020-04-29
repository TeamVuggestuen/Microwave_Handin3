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
        //System Under Test
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
        public void StartCooking_ValidParameters_Throw_exception()
        {
            
            Assert.That(() => cookController.StartCooking(350, 60), Throws.Nothing);
            
        }

        [Test]
        public void StartCooking_InvalidParameters_Throw_exception()
        {

            Assert.That(() => cookController.StartCooking(750, 60), Throws.Exception);

        }

        [Test]
        public void StartCooking_PowerTube_TurnOn_CorrectOutputDisplay()
        {
            cookController.StartCooking(50, 60);

            output.Received().OutputLine($"PowerTube works with 50");

        }


        [Test]
        public void StartCooking_PowerTube_TurnOff_TimerExpired_CorrectOutputDisplay()
        {
            cookController.StartCooking(50, 6);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            
            output.Received().OutputLine($"PowerTube turned off");
            
        }

        [Test]
        public void Stop_PowerTubeOff_CorrectOutputDisplay()
        {
            cookController.StartCooking(50, 60);
            cookController.Stop();

            output.Received().OutputLine($"PowerTube turned off");
        }

    }
}