using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Schema;
using Dapper;
using System.Data;

namespace Para.Bussiness.Query
{
    public class CustomerReportQueryHandler : IRequestHandler<GetCustomersReport, ApiResponse<List<CustomerReport>>>
    {
        private readonly IDbConnection _dbConnection;

        public CustomerReportQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ApiResponse<List<CustomerReport>>> Handle(GetCustomersReport request, CancellationToken cancellationToken)
        {
            var sql = @"
                    SELECT c.Id as CustomerId, c.*, cd.*, ca.*, cp.*
                    FROM Customers c
                    LEFT JOIN CustomerDetails cd ON c.Id = cd.CustomerId
                    LEFT JOIN CustomerAddresses ca ON c.Id = ca.CustomerId
                    LEFT JOIN CustomerPhones cp ON c.Id = cp.CustomerId;
                    ";

            var customerDictionary = new Dictionary<long, CustomerReport>();

            var customers = await _dbConnection.QueryAsync<Customer, CustomerDetailResponse, CustomerAddressResponse, CustomerPhoneResponse, CustomerReport>(
                sql,
                (customer, detail, address, phone) =>
                {
                    if (!customerDictionary.TryGetValue(customer.CustomerNumber, out var customerReport))
                    {
                        customerReport = new CustomerReport
                        {
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            IdentityNumber = customer.IdentityNumber,
                            Email = customer.Email,
                            CustomerNumber = customer.CustomerNumber,
                            DateOfBirth = customer.DateOfBirth,
                            CustomerDetail = detail,
                            CustomerAddresses = new List<CustomerAddressResponse>(),
                            CustomerPhones = new List<CustomerPhoneResponse>()
                        };

                        customerDictionary.Add(customer.CustomerNumber, customerReport);
                    }

                    if (address != null)
                    {
                        customerReport.CustomerAddresses.Add(address);
                    }

                    if (phone != null)
                    {
                        customerReport.CustomerPhones.Add(phone);
                    }

                    return customerReport;
                },
                splitOn: "CustomerId,Id,Id,Id"
            );

            var customersList = customerDictionary.Values.ToList();
            return new ApiResponse<List<CustomerReport>>(customersList);
        }
    }
}
