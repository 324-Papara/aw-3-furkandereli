using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command
{
    public class CustomerDetailCommandHandler :
        IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
        IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerDetail>(request.Request);
            mapped.InsertUser = "System";
            await _unitOfWork.CustomerDetailRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerDetail>(request.Request);
            mapped.Id = request.CustomerDetailId;
            mapped.InsertUser = "System";
            _unitOfWork.CustomerDetailRepository.Update(mapped);
            await _unitOfWork.Complete();

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CustomerDetailRepository.Delete(request.CustomerDetailId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
