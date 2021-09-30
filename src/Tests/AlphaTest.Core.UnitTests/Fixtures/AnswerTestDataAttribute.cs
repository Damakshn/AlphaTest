using AutoFixture;
using AutoFixture.Xunit2;
using System;
using System.IO;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AnswerTestDataAttribute: AutoDataAttribute
    {
        public AnswerTestDataAttribute() 
            :base(() => new Fixture().Customize(new AnswerTestingCustomization()))
        {
            // WORKAROUND для включения трассировки работы AutoFixture в AutoDataAttribute
            // https://stackoverflow.com/questions/18602462/why-doesnt-tracing-behavior-appear-to-work-when-added-to-autofixture-fixture-be
            
            /*var tw = File.AppendText(Path.Combine(Environment.CurrentDirectory, "log.txt"));
            tw.AutoFlush = true;
            this.Fixture.Behaviors.Add(new TracingBehavior(tw));*/
            
        }
    }
}
