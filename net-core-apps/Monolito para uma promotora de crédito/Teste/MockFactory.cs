using Moq;

namespace Teste
{
    //TODO: Essa classe vai para a B.Testes
    public static class MockFactory
    {
        public static Mock<T> Create<T>() where T : class
            => new Mock<T>();
    }
}
