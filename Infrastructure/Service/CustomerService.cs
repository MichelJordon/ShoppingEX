using Domain.Entities;
using Domain.Dtos;
using AutoMapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services;


public class CustomerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public CustomerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<GetCustomerDto>>> GetCustomers()
    {
        var customers = await _context.Customers.ToListAsync();
        var list = new List<GetCustomerDto>();
        
        foreach (var t in customers)
        {
            var customer = new GetCustomerDto()
            {
                CustomerId = t.CustomerId,
                Name = t.Name,
                Surname = t.Surname,
                PhoneNumber = t.PhoneNumber,
                Balance = t.Balance,
                Credits = GetCustomerCredits(t.CustomerId)
            };
            list.Add(customer);
        }
       return new Response<List<GetCustomerDto>>(list);
    }

    public async Task<Response<GetCustomerDto>> AddCustomer(AddCustomerDto customer)
    {
            var newCustomer = new Customer()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname,
                PhoneNumber = customer.PhoneNumber,
            };
        _context.Customers.Add(newCustomer);
         await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(newCustomer));
    }
    public async Task<Response<GetCustomerDto>> TopUpBalance(TopUpBalance ball)
    {
        var acc = _context.Customers.FirstOrDefault(x=>x.PhoneNumber == ball.PhoneNumber);
        if(acc == null) return new Response<GetCustomerDto>(HttpStatusCode.NotFound,"Customer not found");
        acc.Balance += ball.Balance;
        await _context.SaveChangesAsync();
        var get = new GetCustomerDto(){
            Name = acc.Name,
            Surname = acc.Surname,
            PhoneNumber = acc.PhoneNumber,
            Balance = acc.Balance,
            Credits = GetCustomerCredits(acc.CustomerId)
        };
        await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(acc));
    }
    public async Task<Response<GetCustomerDto>> Update(AddCustomerDto customer)
    {
        
        var find = await _context.Customers.FindAsync(customer.CustomerId);
        find.Name = customer.Name;
        find.Surname = customer.Surname;
        find.PhoneNumber = customer.PhoneNumber;
        await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(find));
    }
    
    public async Task<Customer> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
    public int GetCustomerCredits( int id )
    {
        int credits = 0;
        foreach (var t in _context.Credits)
        {
            if ( t.CustomerId == id )
                credits ++;
        }
        return credits;
    }
}