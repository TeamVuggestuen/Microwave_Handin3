using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using NUnit.Framework.Internal.Execution;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    public class IT6_UserInterface
    {
        private IUserInterface _uut;
        private ITimer _timer;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private IOutput _output;
        private ILight _light;
        private IButton _driverPowerButton;
        private IButton _driverTimeButton;
        private IButton _driverStartCancelButton;
        private IDoor _driverDoor;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _driverPowerButton = new Button();
            _driverTimeButton = new Button();
            _driverStartCancelButton = new Button();
            _driverDoor = new Door();
            _cookController = new CookController(_timer, _display, _powerTube, _uut);
            _uut = new UserInterface(_driverPowerButton, _driverTimeButton, _driverStartCancelButton, _driverDoor, _display, _light, _cookController);
            
        }

        [Test]
        public void OnDoorOpened_LightTurnedOn()
        {
            _driverDoor.Open();

            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void OnDoorClosed_LightTurnedOff()
        {
            _driverDoor.Open();
            _driverDoor.Close();

            _output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void OnPowerPressed_ShowPower()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            _driverPowerButton.Press();

            _output.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void OnPowerPressed_2Times_ShowPower()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            _driverPowerButton.Press();
            _driverPowerButton.Press();

            _output.Received().OutputLine($"Display shows: 100 W");
        }

        [Test]
        public void OnPowerPressed_15Times_ShowPower()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 15; i++)
            {
                _driverPowerButton.Press();
            }

            _output.Received(2).OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void OnTimePressed_ShowTime()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            _driverPowerButton.Press();
            _driverTimeButton.Press();

            _output.Received().OutputLine($"Display shows: 01:00");
        }

        [Test]
        public void OnTimePressed_2Times_ShowTime()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            _driverPowerButton.Press();
            _driverTimeButton.Press();
            _driverTimeButton.Press();

            _output.Received().OutputLine($"Display shows: 02:00");
        }

        [Test]
        public void OnStartCancelPressed_LightTurnOn()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            _driverPowerButton.Press();
            _driverTimeButton.Press();
            _driverStartCancelButton.Press();

            _output.Received().OutputLine($"Light is turned on");
        }

        [Test]
        public void OnStartCancelPressed_PowerTubeStarted()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverTimeButton.Press();
            _driverStartCancelButton.Press();

            _output.Received().OutputLine($"PowerTube works with 100 %");
        }

        [Test]
        public void OnStartCancelPressed_DontClear()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverTimeButton.Press();
            _driverStartCancelButton.Press();

            _output.DidNotReceive().OutputLine($"Display cleared");
        }

        [Test]
        public void OnStartCancelPressed_DuringSetUp_DisplayCleared()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverStartCancelButton.Press();

            _output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void OnDoorOpened_DuringSetup_DisplayBlanked()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverDoor.Open();

            _output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void OnDoorOpened_DuringSetup2_DisplayBlanked()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverTimeButton.Press();
            _driverDoor.Open();

            _output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void OnStartCancelPressed_DuringCooking_DisplayCleared()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverStartCancelButton.Press();
            _driverStartCancelButton.Press();

            _output.Received().OutputLine($"Display cleared");
        }

        [Test]
        public void OnDoorOpened_DuringCooking_DisplayCleared()
        {
            _driverDoor.Open();
            _driverDoor.Close();
            for (int i = 0; i < 14; i++)
            {
                _driverPowerButton.Press();
            }
            _driverStartCancelButton.Press();
            _driverDoor.Open();

            _output.Received().OutputLine($"Display cleared");
        }



    }
}
