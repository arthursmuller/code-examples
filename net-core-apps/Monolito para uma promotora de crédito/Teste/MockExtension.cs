using Moq;
using System;

namespace Teste
{
    //TODO: Essa classe vai para a B.Testes
    public static class MockExtension
    {
        public static void InjectBy<T>(this Mock<T> mock, ref Func<T> factory) where T : class
            => factory = () => mock.Object;
    }
}
