using Api.ApplicationLogic.Interface;
using Api.Infrastructure.Interface;
using Api.Infrastructure;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Api.Infrastructure.Persistence;
using Api.ApplicationLogic.Mapper;

namespace UnitTest
{
    public class SetupTest : IDisposable
    {
        protected readonly IMapper _mapperConfig;
        protected readonly Fixture _fixture;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<IBookReadService> _bookReadServiceMock;
        protected readonly Mock<ICurrentTime> _currentTimeMock;
        protected readonly Mock<IBookRepository> _bookRepositoryMock;
        protected readonly ApplicationDbContext _dbContext;
        public SetupTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookReadServiceMock = new Mock<IBookReadService>();
            _currentTimeMock = new Mock<ICurrentTime>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _currentTimeMock.Setup(x => x.GetCurrentTime()).Returns(DateTime.UtcNow);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
