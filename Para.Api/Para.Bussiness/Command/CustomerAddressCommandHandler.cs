using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command
{
    public class CustomerAddressCommandHandler :
        IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
        IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            mapped.InsertUser = "System";
            await _unitOfWork.CustomerAddressRepository.Insert(mapped);
            await _unitOfWork.Complete();

            var response = _mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            mapped.Id = request.CustomerAddressId;
            mapped.InsertUser = "System";
            _unitOfWork.CustomerAddressRepository.Update(mapped);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CustomerAddressRepository.Delete(request.CustomerAddressId);
            await _unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
