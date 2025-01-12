// The MIT License (MIT)
// 
// Copyright (c) 2015-2021 Rasmus Mikkelsen
// Copyright (c) 2015-2021 eBay Software Foundation
// https://github.com/eventflow/EventFlow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Elastic.Clients.Elasticsearch.Mapping;
using EventFlow.Elasticsearch.ReadStores;
using EventFlow.Elasticsearch.ReadStores.Attributes;
using EventFlow.ReadStores;
using EventFlow.TestHelpers;
using FluentAssertions;
using NUnit.Framework;
 

namespace EventFlow.Elasticsearch.Tests.UnitTests
{
    [Category(Categories.Unit)]
    public class ReadModelDescriptionProviderTests : TestsFor<ReadModelDescriptionProvider>
    {
        // ReSharper disable once ClassNeverInstantiated.Local
        private class TestReadModelA : IReadModel
        {
            public void MappingDescriptor(TypeMappingDescriptor<TestReadModelA> mappingDescriptor)
            {
                
            }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        [ElasticsearchIndex("SomeThingFancy")]
        private class TestReadModelB : IReadModel
        {
          
        }

        [Test]
        public void ReadModelIndexIsCorrectWithoutAttribute()
        {
            // Act
            var readModelDescription = Sut.GetReadModelDescription<TestReadModelA>();

            // Assert
            readModelDescription.IndexName.Should().Be("eventflow-testreadmodela");
        }

        [Test]
        public void ReadModelIndexIsCorrectWithAttribute()
        {
            // Act
            var readModelDescription = Sut.GetReadModelDescription<TestReadModelB>();

            // Assert
            readModelDescription.IndexName.Should().Be("SomeThingFancy".ToLowerInvariant());
        }
    }
}