using AspBackendTest.Application.Dtos.Info;
using AspBackendTest.Application.Dtos.Requests.Product;
using AspBackendTest.Application.IRepositories;
using AspBackendTest.Application.Mapper;
using AspBackendTest.Infrastructure.Context;
using AspBackendTest.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace AspBackendTest.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _authContext;

    public ProductRepository(ProductContext authContext)
    {
        _authContext = authContext;
    }


    public async Task<(List<ProductInfo>, int)> GetProducts(ProductQueryParameter parameter,
        CancellationToken cancellationToken = default)
    {
        var queryableProduct = _authContext.Products!.AsQueryable();

        if (!string.IsNullOrEmpty(parameter.Name))
            queryableProduct =
                queryableProduct.Where(product => product.Name.ToLower().Contains(parameter.Name.ToLower()));

        if (!string.IsNullOrEmpty(parameter.Color))
            queryableProduct =
                queryableProduct.Where(product => product.Color.ToLower().Contains(parameter.Color.ToLower()));

        if (parameter.Price != 0)
            queryableProduct = queryableProduct.Where(product =>
                product.TotalPrice == parameter.Price);

        if (parameter.Size != null)
            queryableProduct = queryableProduct.Where(product =>
                product.SizeType == parameter.Size);

        if (parameter.CurrencyId != null)
            queryableProduct = queryableProduct.Where(product =>
                product.CurrencyId == parameter.CurrencyId);

        if (parameter.PackingTypeId != null)
            queryableProduct = queryableProduct.Where(product =>
                product.PackingTypeId == parameter.PackingTypeId);

        var total = await queryableProduct.CountAsync(cancellationToken);

        queryableProduct = queryableProduct.OrderBy(type => type.UpdateTime);

        if (parameter.Skip != 0) queryableProduct = queryableProduct.Skip(parameter.Skip);
        if (parameter.Take != 0) queryableProduct = queryableProduct.Take(parameter.Take);

        return (
            await queryableProduct.Select(type => type.ToProductInfo())
                .ToListAsync(cancellationToken), total);
    }

    public async Task<ProductInfo> AddProduct(CreateProductRequest request, decimal totalPrice,
        CancellationToken cancellationToken = default)
    {
        var directory = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "ImageProduct"));

        var product = new Product
        {
            Color = request.Color,
            CurrencyId = request.CurrencyId,
            PackingTypeId = request.PackingTypeId,
            Name = request.Name,
            TotalPrice = totalPrice,
            SizeType = request.SizeType,
            SizeNumber = request.SizeNumber,
            PriceCurrency = request.Price
        };
        var path = Path.Combine(directory.ToString(), $"{product.Id}");
        await using var stream = new FileStream(path, FileMode.CreateNew);
        await request.File.CopyToAsync(stream, cancellationToken);
        product.FilePath = path;
        await _authContext.Products!.AddAsync(product, cancellationToken);
        await _authContext.SaveChangesAsync(cancellationToken);

        var result = await GetProduct(product.Id, cancellationToken);

        return result.ToProductInfo();
    }

    public async Task<ProductInfo> UpdateProduct(Guid id, UpdateProductRequest request, decimal totalPrice,
        CancellationToken cancellationToken = default)
    {
        var product = await _authContext.Products!.FirstAsync(product => product.Id == id, cancellationToken);

        product.Color = request.Color;
        product.CurrencyId = request.CurrencyId;
        product.PackingTypeId = request.PackingTypeId;
        product.Name = request.Name;
        product.TotalPrice = totalPrice;
        product.SizeType = request.SizeType;
        product.SizeNumber = request.SizeNumber;
        product.PriceCurrency = request.Price;

        await _authContext.SaveChangesAsync(cancellationToken);

        var result = await GetProduct(product.Id, cancellationToken);

        return result.ToProductInfo();
    }

    public async Task<Product> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        return await _authContext.Products!.FirstAsync(product => product.Id == id, cancellationToken);
    }

    public async Task RemoveProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _authContext.Products!.FirstAsync(product => product.Id == id, cancellationToken);

        product.IsDeleted = true;
        product.UpdateTime = DateTime.Now;

        await _authContext.SaveChangesAsync(cancellationToken);
    }
}