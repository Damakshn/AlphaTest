using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Infrastructure.Database;
using AutoMapper;

namespace AlphaTest.Application.UseCases.Common
{
    public abstract class UseCaseHandlerBase<TRequest> : IRequestHandler<TRequest> where TRequest : IUseCaseRequest
    {
        protected AlphaTestContext _db;
        protected IMapper _mapper;

        public UseCaseHandlerBase(AlphaTestContext db)
        {
            _db = db;
        }

        public UseCaseHandlerBase(AlphaTestContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public abstract Task<Unit> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class UseCaseHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
        protected AlphaTestContext _db;
        protected IMapper _mapper;

        public UseCaseHandlerBase(AlphaTestContext db)
        {
            _db = db;
        }

        public UseCaseHandlerBase(AlphaTestContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class UseCaseReportingHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IUseCaseRequest<TResponse>
    {
        protected AlphaTestContext _db;
        protected IMapper _mapper;

        public UseCaseReportingHandlerBase(AlphaTestContext db)
        {
            _db = db;
            _db.DisableTracking();
        }

        public UseCaseReportingHandlerBase(AlphaTestContext db, IMapper mapper)
        {
            _db = db;
            _db.DisableTracking();
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
