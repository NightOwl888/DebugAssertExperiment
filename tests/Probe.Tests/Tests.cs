using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Probe.Tests
{
    public class Tests
    {
        [Test]
        public void Probe()
        {
            Console.WriteLine("==========");
            Console.WriteLine(RuntimeInformation.FrameworkDescription);
            Console.WriteLine(RuntimeInformation.OSDescription);
            Console.WriteLine(RuntimeInformation.ProcessArchitecture);
            Console.WriteLine(RuntimeInformation.OSArchitecture);

            AppDomain.CurrentDomain.FirstChanceException += (_, e) =>
            {
                var t = e.Exception.GetType();

                Console.WriteLine(
                    $"FIRST CHANCE: {t.FullName}");

                Console.WriteLine(
                    $"ASSEMBLY: {t.Assembly.FullName}");

                Console.WriteLine(
                    $"LOCATION: {t.Assembly.Location}");
            };

            Console.WriteLine("Before Assert");

            try
            {
                Debug.Assert(false, "Probe");

                Console.WriteLine("Returned Normally");

                Assert.Fail("Debug.Assert returned");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught");

                Console.WriteLine(ex.GetType().FullName);

                Console.WriteLine(ex.GetType().Assembly.FullName);

                throw;
            }
        }
    }
}
