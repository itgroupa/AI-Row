using AutoFixture;
using Itgroupa.Dto;

namespace Itgroupa.Tests
{
    public static class DataGenerator
    {
        public static Price GetPrice() => new Fixture().Create<Price>();
    }
}