using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex a = new Complex()
            {
                Re = 10,
                Imaginary = 20
            };
            Complex b = new Complex()
            {
                Re = 1,
                Imaginary = 2
            };

            Complex actual = a.Add(b);
            Complex shouldBe = new Complex()
            {
                Re = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }
    }
}


