using Moq;
using Api.ApplicationLogic.Services;
using Api.Core.Entities;
using Api.ApplicationLogic.Interface;
using AutoFixture;
using Api.Core.Commons;
using Api.Core;

namespace UnitTest
{
    public class BookServiceTests : SetupTest
    {
        private readonly IBookReadService _bookReadService;
        private readonly AppConfiguration _configuration = new AppConfiguration();
        public BookServiceTests()
        {
            _bookReadService = new BookReadService(_unitOfWorkMock.Object, _mapperConfig, _cacheServiceMock.Object, _configuration);
        }
        [Fact]
        public async Task GetBookPaginationAsync_ShouldReturnCorrectDataWhenDidNotPassTheParameters()
        {
            //arrange
            var mockData = new Pagination<Book>
            {
                Items = _fixture.Build<Book>().CreateMany(100).ToList(),
                PageIndex = 0,
                PageSize = 100,
                TotalItemsCount = 100
            };
            var expectedResult = _mapperConfig.Map<Pagination<Book>>(mockData);
            _unitOfWorkMock.Setup(x => x.BookRepository.ToPagination(0, 100)).ReturnsAsync(mockData);
            //act
            var result = await _bookReadService.Get(0, 100);
            //assert
            _unitOfWorkMock.Verify(x => x.BookRepository.ToPagination(0, 100), Times.Once());
        }
    }
}
