using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Common
{
    public abstract class UseCaseHandlerBase<TRequest> : IRequestHandler<TRequest> where TRequest : IUseCaseRequest
    {
        protected IDbContext _db;
        protected IMapper _mapper;

        public UseCaseHandlerBase(IDbContext db)
        {
            _db = db;
        }

        public UseCaseHandlerBase(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public abstract Task<Unit> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class UseCaseHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
        protected IDbContext _db;
        protected IMapper _mapper;

        public UseCaseHandlerBase(IDbContext db)
        {
            _db = db;
        }

        public UseCaseHandlerBase(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class UseCaseReportingHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
        protected IDbReportingContext _db;
        protected IMapper _mapper;

        public UseCaseReportingHandlerBase(IDbReportingContext db)
        {
            _db = db;
        }

        public UseCaseReportingHandlerBase(IDbReportingContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
