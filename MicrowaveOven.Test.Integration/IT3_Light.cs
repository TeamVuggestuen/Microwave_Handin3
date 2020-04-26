using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    //---------------------NB-----------------------------
    //(1) Display set as IT2 as class/module is parallel to PowereTube in dependency tree and uses bottom most module Output
    //(2) Remember BLACKBOX perspective!

    [TestFixture]
    class IT3_Light
    {
        //System Under Test
        private IOutput myOutput;
        private bool isOn = false;

        [SetUp]
        public void Setup()
        {
            myOutput = new Output();
        }


        //[TestCase()]
        //{

        //}
    }
}
